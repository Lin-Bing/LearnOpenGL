#version 330 core
layout (location = 0) in vec2 aPos;
layout (location = 1) in vec3 aColor;

// 接口块（Interface Block），用于着色器之间参数传递
out VS_OUT {
    vec3 color;
} vs_out;

void main()
{
    vs_out.color = aColor; // 传递颜色
    gl_Position = vec4(aPos.x, aPos.y, 0.0, 1.0); 
}
