shader_type canvas_item;

uniform float tilingScale : hint_range(0.1, 10.0) = 1;
uniform float movementX : hint_range(-1.0, 1.0) = 0.0;
uniform float movementY : hint_range(-1.0, 1.0) = -1.0;
uniform float movementSpeed : hint_range(0, 5.0) = 1.0;

void fragment() 
{
	vec2 timeScale = vec2(movementX, movementY) * movementSpeed;
    vec2 scaledUV = FRAGCOORD.xy * (tilingScale * 0.01);
	vec2 displacement = TIME * timeScale;
	vec2 newUvs = scaledUV + displacement;
	vec2 clampedUvs = fract(newUvs);
	
	vec4 inputColor = texture(TEXTURE, clampedUvs);
	COLOR = inputColor;
}
