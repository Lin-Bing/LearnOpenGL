#version 330 core
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture1;

void main()
{
    // 片段着色器中获取了纹理的全部4个颜色分量，而不仅仅是RGB分量
    vec4 texColor = texture(texture1, TexCoords);
    // alpha值低于0.1时，丢弃片段
    if(texColor.a < 0.1)
        discard;
    FragColor = texColor;
}
