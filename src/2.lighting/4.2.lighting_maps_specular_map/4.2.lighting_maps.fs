#version 330 core
out vec4 FragColor;

/*
 物体材质：描述物体本身的特性，颜色，反光指数。只是从固定颜色变成纹理，才能反应现实物体
    漫反射、环境光照：用同一个纹理贴图，因为都是反应物体颜色
    镜面反射：用灰度图（rgb值相同），因为表示反光属性。越白（值越大）表示越反光（乘积越大）
 */
struct Material {
    // 漫反射贴图纹理采样器
    sampler2D diffuse;
    // 镜面光贴图纹理采样器
    sampler2D specular;    
    // 镜面反射指数
    float shininess;
}; 

// 光强度系数分量
struct Light {
    vec3 position;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

in vec3 FragPos;  
in vec3 Normal;  
in vec2 TexCoords;
  
uniform vec3 viewPos;
uniform Material material;
uniform Light light;

// 环境光照、漫反射、镜面光照都从纹理贴图上获取颜色
void main()
{
    // ambient
    vec3 ambient = light.ambient * texture(material.diffuse, TexCoords).rgb;
  	
    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * diff * texture(material.diffuse, TexCoords).rgb;  
    
    // specular
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * spec * texture(material.specular, TexCoords).rgb;  
        
    vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);
} 
