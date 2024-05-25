#version 330 core
out vec4 FragColor;

in vec3 ourColor;
in vec2 TexCoord;

// texture samplers
uniform sampler2D texture1;
uniform sampler2D texture2;

void main()
{
	// linearly interpolate between both textures (80% container, 20% awesomeface)
	// FragColor = mix(texture(texture1, TexCoord), texture(texture2, TexCoord), 0.2);
    
    /* cp 练习1：修改片段着色器，仅让笑脸图案朝另一个方向看
     */
    FragColor = mix(texture(texture1, TexCoord), texture(texture2, vec2(1.0-TexCoord.x, TexCoord.y)), 0.2);
}
