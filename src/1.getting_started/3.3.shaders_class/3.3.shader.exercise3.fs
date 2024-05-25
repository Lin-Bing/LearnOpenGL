#version 330 core
out vec4 FragColor;

//in vec3 ourColor;
in vec3 ourPosition;

void main()
{
    //FragColor = vec4(ourColor, 1.0f);
    FragColor = vec4(ourPosition, 1.0f);
}

/*
 
 为什么在三角形的左下角是黑的?
 
 1.片段颜色的输出等于（插值）坐标
 2.三角形左下角的坐标是多少？ 这是（-0.5f，-0.5f，0.0f）。 当xy值为负数，它们被限制为 0.0f 值，0.0f 的值当然是黑色的。 这种情况一直发生到中心两侧三角形，因为从该点开始，值将再次进行正插值。因此中心点左下角是纯黑色
 */
