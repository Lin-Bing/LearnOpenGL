#version 330 core

void main()
{
    // 由于我们没有颜色缓冲，最后的片段不需要任何处理，所以我们可以简单地使用一个空片段着色器。底层会默认去设置深度缓冲
    
    // gl_FragDepth = gl_FragCoord.z;
}
