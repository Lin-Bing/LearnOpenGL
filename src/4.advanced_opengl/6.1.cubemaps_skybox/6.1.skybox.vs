#version 330 core
layout (location = 0) in vec3 aPos;

out vec3 TexCoords;

uniform mat4 projection;
uniform mat4 view;

void main()
{
    // 输入位置向量，输出为片段着色器的纹理坐标
    TexCoords = aPos;
    // 不用模型矩阵，因为顶点坐标就是标准坐标
    vec4 pos = projection * view * vec4(aPos, 1.0);
    /* cp 性能优化
     天空在最后面，当前面有东西时我们希望深度测试失败。将输出位置的z分量等于它的w分量，透视除法会让z分量永远等于1.0，即最大的深度值。结果就是天空盒只会在没有可见物体的地方渲染了，因为其他物体深度，在前面
     */
    gl_Position = pos.xyww;
}  
