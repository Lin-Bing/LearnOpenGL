#version 330 core
out vec4 FragColor;

in vec3 Normal;  
in vec3 FragPos;  
  
uniform vec3 lightPos; 
uniform vec3 viewPos;  // 观察点
uniform vec3 lightColor;  // 光线颜色
uniform vec3 objectColor;


// 思路：根据物体颜色是反射光线的原理，最终颜色 = 三个分量的光线颜色之和 ✖️ 物体颜色

void main()
{
    // ambient 环境光照：模拟非直接接收光照的全局照明，保证不被照射的部分不是纯黑：系数 * 光照颜色
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;
  	
    // diffuse 漫反射：模拟光对物体的方向性影响：cos(光线与法线夹角) * 光照颜色
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;
    
    // specular 镜面反射：cos(观察方向与反射方向夹角)^指数运算 * 反射强度 * 光照颜色
    float specularStrength = 0.5;
    // 观察方向
    vec3 viewDir = normalize(viewPos - FragPos);
    // 反射方向
    vec3 reflectDir = reflect(-lightDir, norm);
    // 夹角余弦， 指数32缩小高光范围
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
    vec3 specular = specularStrength * spec * lightColor;  
        
    vec3 result = (ambient + diffuse + specular) * objectColor;
    FragColor = vec4(result, 1.0);
} 
