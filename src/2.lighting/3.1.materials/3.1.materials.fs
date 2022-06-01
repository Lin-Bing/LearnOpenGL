#version 330 core
out vec4 FragColor;

// 物体材质：原本三个分量都是一个颜色，现在使用三个颜色来描述
struct Material {
    // ambient、diffuse一般是物体颜色
    vec3 ambient;
    vec3 diffuse;
    // specular表示镜面高光颜色，一般是接近白色
    vec3 specular;
    // 镜面反射指数
    float shininess;
}; 

// 光的属性
struct Light {
    // 光源位置
    vec3 position;

    // 每个颜色分量的强度系数：通常环境光照系数最低、漫反射中等、镜面反射由于要产生高光效果所以强度一般是最大值1.0
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

in vec3 FragPos;  
in vec3 Normal;  
  
uniform vec3 viewPos;
uniform Material material;
uniform Light light;

void main()
{
    // ambient
    vec3 ambient = light.ambient * material.ambient;
  	
    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * (diff * material.diffuse);
    
    // specular
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * (spec * material.specular);  
        
    vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);
} 
