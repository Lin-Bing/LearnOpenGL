#version 330 core
out vec4 FragColor;

in vec3 TexCoords;

// 立方体贴图纹理采样器， vec3而不是vec2
uniform samplerCube skybox;

void main()
{    
    FragColor = texture(skybox, TexCoords);
}
