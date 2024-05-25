#version 330 core
out vec4 FragColor;

in VS_OUT {
    vec3 FragPos; // 世界空间的片段位置
    vec3 Normal;
    vec2 TexCoords;
    vec4 FragPosLightSpace;  // 光照视角下的裁剪空间的片段位置
} fs_in;

// 漫反射纹理贴图
uniform sampler2D diffuseTexture;
// 阴影深度贴图
uniform sampler2D shadowMap;
// 光源位置
uniform vec3 lightPos;
// 相机位置
uniform vec3 viewPos;

// 计算阴影值：片段在阴影中时为1.0，否则为0.0
float ShadowCalculation(vec4 fragPosLightSpace)
{
    /*
     顶点着色器输出一个裁切空间顶点位置到gl_Position时，OpenGL自动进行一个透视除法，将裁切空间坐标的范围-w到w转为-1到1，得到NDC。
     而从顶点着色器传递过来的fragPosLightSpace需要自己做透视除法
     
     当使用正交投影矩阵，顶点w元素仍保持不变，所以这一步实际上毫无意义。可是，当使用透视投影的时候就是必须的了，所以为了保证在两种投影矩阵下都有效就得留着这行。
     */
    // perform perspective divide
    vec3 projCoords = fragPosLightSpace.xyz / fragPosLightSpace.w;
    // 因为来自深度贴图的深度在0到1的范围，我们也打算使用projCoords从深度贴图中去采样，所以我们将NDC坐标-1到1变换为0到1的范围
    // transform to [0,1] range
    projCoords = projCoords * 0.5 + 0.5;
    // 从深度纹理贴图中采样，得到片段光空间的深度值
    // get closest depth value from light's perspective (using [0,1] range fragPosLight as coords)
    float closestDepth = texture(shadowMap, projCoords.xy).r;
    // 从矩阵变换结果，获取片段在相机空间的深度值
    // get depth of current fragment from light's perspective
    float currentDepth = projCoords.z;
    // 比较深度大小，判断是片段否在阴影中
    // check whether current frag pos is in shadow
    float shadow = currentDepth > closestDepth  ? 1.0 : 0.0;

    return shadow;
}

void main()
{
    // 1.正常进行光照模型计算
    vec3 color = texture(diffuseTexture, fs_in.TexCoords).rgb;
    vec3 normal = normalize(fs_in.Normal);
    vec3 lightColor = vec3(0.3);
    // ambient
    vec3 ambient = 0.3 * lightColor;
    // diffuse
    vec3 lightDir = normalize(lightPos - fs_in.FragPos);
    float diff = max(dot(lightDir, normal), 0.0);
    vec3 diffuse = diff * lightColor;
    // specular
    vec3 viewDir = normalize(viewPos - fs_in.FragPos);
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = 0.0;
    vec3 halfwayDir = normalize(lightDir + viewDir);  
    spec = pow(max(dot(normal, halfwayDir), 0.0), 64.0);
    vec3 specular = spec * lightColor;
    
    // 2.根据是否在阴影中，进行光照计算：片段在阴影中，则去掉漫反射、镜面反射，只留下环境光照
    // calculate shadow
    float shadow = ShadowCalculation(fs_in.FragPosLightSpace);                      
    vec3 lighting = (ambient + (1.0 - shadow) * (diffuse + specular)) * color;    
    
    FragColor = vec4(lighting, 1.0);
}
