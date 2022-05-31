#version 330 core
out vec4 FragColor;
  
uniform vec3 objectColor;
uniform vec3 lightColor;

void main()
{
    // 物体反射颜色：物体本身颜色 * 光线颜色
    FragColor = vec4(lightColor * objectColor, 1.0);
}
