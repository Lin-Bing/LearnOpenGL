#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 2) in vec2 aTexCoords;
// 实例化数组：岩石模型矩阵
layout (location = 3) in mat4 aInstanceMatrix;

out vec2 TexCoords;

uniform mat4 projection;
uniform mat4 view;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = projection * view * aInstanceMatrix * vec4(aPos, 1.0f); 
}
