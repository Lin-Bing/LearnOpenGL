#version 330 core

// gs输入三角形图元，输出线段图元
layout (triangles) in;
layout (line_strip, max_vertices = 6) out;

// 接收vs处理后的裁剪空间法向量
in VS_OUT {
    vec3 normal;
} gs_in[];

const float MAGNITUDE = 0.2;

// ??? 为何需要投影矩阵
uniform mat4 projection;

// 接收位置顶点、法向量，在每个位置向量处绘制一个法线向量
void GenerateLine(int index)
{
    // 发射位置顶点
    gl_Position = projection * gl_in[index].gl_Position;
    EmitVertex();
    // 发射法向量顶点
    gl_Position = projection * (gl_in[index].gl_Position + vec4(gs_in[index].normal, 0.0) * MAGNITUDE);
    EmitVertex();
    
    EndPrimitive();
}

void main()
{
    // 绘制三角形图元的三个顶点的法向量
    GenerateLine(0); // first vertex normal
    GenerateLine(1); // second vertex normal
    GenerateLine(2); // third vertex normal
}
