#version 330 core
layout (location = 0) in vec2 aPos;
layout (location = 1) in vec3 aColor;
// 偏移向量，在vs中看不出差别，但cpu中可配置为实例化数组
layout (location = 2) in vec2 aOffset;

out vec3 fColor;

void main()
{
    fColor = aColor;
    
    // gl_Position = vec4(aPos + aOffset, 0.0, 1.0);
    
    /*
     gl_InstanceID
     GLSL在顶点着色器内建变量，在使用实例化渲染调用时，gl_InstanceID会从0开始，在每个实例被渲染时递增1
     此处根据实例ID，依次缩放至原尺寸的 gl_InstanceID / 100.0
     */
    vec2 pos = aPos * (gl_InstanceID / 100.0);
    gl_Position = vec4(pos + aOffset, 0.0, 1.0);
}
