#version 330 core
in vec2 TexCoords;
out vec4 color;

// 字体纹理
uniform sampler2D text;
// 字体颜色
uniform vec3 textColor;

void main()
{
    // 使用纹理坐标，对纹理颜色进行采样。此处的颜色仅仅是一个8位的单颜色通道作为alpha通道（实现：字体颜色不透明，字体背景透明）
    vec4 sampled = vec4(1.0, 1.0, 1.0, texture(text, TexCoords).r);
    // 字体颜色 = 颜色向量与采样颜色向量作分量相乘，得到最终颜色
    color = vec4(textColor, 1.0) * sampled;
}
