#version 330 core
// 声明从vs输入的图元类型：points，点
layout (points) in;
// 声明gs输出的图元类型：triangle，三角形，顶点数最多5个
layout (triangle_strip, max_vertices = 5) out;

/* cp 接口块，接收vs输出
 注：大多数的渲染图元包含多于1个的顶点，而几何着色器的输入是一个图元的所有顶点，因此是一个数组。不过此处是绘制图元是GL_POINTS只传入一个顶点
 */
in VS_OUT {
    vec3 color;
} gs_in[];

// 输出颜色值给fs，fs只需一个颜色，因此是单一向量，不是数组
out vec3 fColor;


void build_house(vec4 position)
{
    // 取顶点颜色，作为输出颜色
    fColor = gs_in[0].color; // gs_in[0] since there's only one input vertex
    
    /* cp 发色顶点，添加5个顶点到图元中，构成一个房子
    就实现了，本来是绘制一个顶点，通过几何着色器改变了图元类型，变成绘制多个三角形
     */
    gl_Position = position + vec4(-0.2, -0.2, 0.0, 0.0); // 1:bottom-left   
    EmitVertex();   
    gl_Position = position + vec4( 0.2, -0.2, 0.0, 0.0); // 2:bottom-right
    EmitVertex();
    gl_Position = position + vec4(-0.2,  0.2, 0.0, 0.0); // 3:top-left
    EmitVertex();
    gl_Position = position + vec4( 0.2,  0.2, 0.0, 0.0); // 4:top-right
    EmitVertex();
    gl_Position = position + vec4( 0.0,  0.4, 0.0, 0.0); // 5:top
    // 最后一个顶点颜色改为白色，通过插值，片段着色器就会产生白色过渡
    fColor = vec3(1.0, 1.0, 1.0);
    EmitVertex();
    
    // 所有发射出的(Emitted)顶点都会合成为指定的输出渲染图元
    EndPrimitive();
}

void main() {
    // 取顶点位置
    build_house(gl_in[0].gl_Position);
}
