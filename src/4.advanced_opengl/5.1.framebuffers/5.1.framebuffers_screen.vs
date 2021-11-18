#version 330 core
layout (location = 0) in vec2 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = aTexCoords;
    // 由于离屏帧缓冲的读取产物的顶点属性已经是标准坐标，不需要任何矩阵变换
    gl_Position = vec4(aPos.x, aPos.y, 0.0, 1.0); 
}  
