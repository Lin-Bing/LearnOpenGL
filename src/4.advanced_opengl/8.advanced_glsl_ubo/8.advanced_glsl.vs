#version 330 core
layout (location = 0) in vec3 aPos;

// 定义一个std140布局的Uniform块 Matrices，包含：投影矩阵、视觉矩阵
layout (std140) uniform Matrices
{
    mat4 projection;
    mat4 view;
};
//不同的物体模型矩阵不一样，因此不放在Uniform块中
uniform mat4 model;

void main()
{
    // 矩阵转换
    gl_Position = projection * view * model * vec4(aPos, 1.0);
}  
