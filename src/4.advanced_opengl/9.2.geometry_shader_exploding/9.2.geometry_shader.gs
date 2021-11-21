#version 330 core

// gs输入输出都是三角形图元，只是顶点变换过
layout (triangles) in;
layout (triangle_strip, max_vertices = 3) out;

// 接收vs输入的接口块
in VS_OUT {
    vec2 texCoords; // 纹理坐标
} gs_in[];

// 向fs输出纹理坐标
out vec2 TexCoords; 

// uniform变量作为时间戳，用于变换
uniform float time;

// 计算法向量
vec3 GetNormal()
{
    // 注意：图元是三角形，因此gl_in数组大小为3，即三角形的三个顶点
    // 顶点向量相减，获取两个平行于三角形表面的向量a和b
    vec3 a = vec3(gl_in[0].gl_Position) - vec3(gl_in[1].gl_Position);
    vec3 b = vec3(gl_in[2].gl_Position) - vec3(gl_in[1].gl_Position);
    // 叉乘获取垂直于两个向量的一个向量，标准化即得到法向量
    return normalize(cross(a, b));
}

// 顶点计算爆炸效果：顶点随着时间沿法向量方向移动
vec4 explode(vec4 position, vec3 normal)
{
    // 位置沿法向量方向移动（0,2），sin取值范围(-1,1),(sin(time) + 1.0) / 2.0 取值范围即(0,1)
    float magnitude = 2.0;
    vec3 direction = normal * ((sin(time) + 1.0) / 2.0) * magnitude; 
    return position + vec4(direction, 0.0); // 原始位置值 + 法向量方向移动值，产生爆炸效果
}


void main() {
    // 法向量
    vec3 normal = GetNormal();
    
    // 发射3个变换后的顶点
    
    // 爆炸效果变换
    gl_Position = explode(gl_in[0].gl_Position, normal);
    // 顶点发射前输出纹理坐标
    TexCoords = gs_in[0].texCoords;
    EmitVertex();
    gl_Position = explode(gl_in[1].gl_Position, normal);
    TexCoords = gs_in[1].texCoords;
    EmitVertex();
    gl_Position = explode(gl_in[2].gl_Position, normal);
    TexCoords = gs_in[2].texCoords;
    EmitVertex();
    
    EndPrimitive();
}
