#version 330 core
out vec4 FragColor;

in vec3 Normal;

in vec3 FragPos; // 片段位置

uniform vec3 lightPos;  // 光源位置
uniform vec3 lightColor;
uniform vec3 objectColor;

void main()
{
    // ambient 环境光照，模拟非直接接收光照的全局照明，保证不被照射的部分不是纯黑
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;
  	
    // diffuse 漫反射
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos); // 光线方向
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;
            
    vec3 result = (ambient + diffuse) * objectColor;
    FragColor = vec4(result, 1.0);
} 
