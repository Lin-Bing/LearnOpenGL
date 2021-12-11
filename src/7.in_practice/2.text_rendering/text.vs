#version 330 core

// 顶点属性：使用一个vec4，包含顶点位置坐标（绘制到2D平面上，不需要z值）、纹理坐标
layout (location = 0) in vec4 vertex; // <vec2 pos, vec2 tex>
out vec2 TexCoords;

// 投影矩阵：由于是在2D平面，只需使用正射投影矩阵投影到屏幕坐标
uniform mat4 projection;

void main()
{
    gl_Position = projection * vec4(vertex.xy, 0.0, 1.0);
    TexCoords = vertex.zw;
}
