#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;

// 传递顶点世界空间的法向量、位置值给fs，用于采样
out vec3 Normal;
out vec3 Position;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    // 使用法线矩阵，把法向量变换到世界空间（光照计算在世界空间进行）
    Normal = mat3(transpose(inverse(model))) * aNormal;
    // 位置转换为世界空间
    Position = vec3(model * vec4(aPos, 1.0));
    
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}
