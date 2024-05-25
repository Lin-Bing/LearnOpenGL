#version 330 core
layout (location = 0) in vec3 aPos;

uniform mat4 lightSpaceMatrix;
uniform mat4 model;

void main()
{
    // lightSpaceMatrix是以光源为视角的 投影矩阵*视觉矩阵，用于把顶点从世界空间变换到以光源为视角的裁剪空间
    gl_Position = lightSpaceMatrix * model * vec4(aPos, 1.0);
}
