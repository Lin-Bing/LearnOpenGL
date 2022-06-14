#version 330 core
out vec4 FragColor;

in vec3 Normal;
in vec3 Position;

uniform vec3 cameraPos;
// 箱子片段颜色通过反射从天空盒子采样，因此需要天空盒子的纹理采样器
uniform samplerCube skybox;

void main()
{
    // 观察方向向量（顶点位置 - 相机位置）
    vec3 I = normalize(Position - cameraPos);
    // 以法向量为参考，计算反射向量
    vec3 R = reflect(I, normalize(Normal));
    // 使用反射向量做纹理采样
    FragColor = vec4(texture(skybox, R).rgb, 1.0);
}
