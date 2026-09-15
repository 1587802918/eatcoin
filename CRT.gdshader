shader_type canvas_item;
uniform sampler2D screen_texture :hint_screen_texture;


uniform float curvature : hint_range(0.0, 10.0) = 0.50;       // 屏幕弧度
uniform float scanline_count : hint_range(0.0, 1080.0) = 200.0; // 扫描线密度
uniform float scanline_intensity : hint_range(0.0, 1.0) = 0.12;// 扫描线深浅
uniform float chromatic_aberration : hint_range(0.0, 0.02) = 0.001; // 色差强度
uniform float vignette_intensity : hint_range(0.0, 2.0) = 0.1;  // 暗角深浅
uniform float noise_intensity : hint_range(0.0, 0.2) = 0.01;    // 噪点强度


float random(vec2 uv)
{
    return fract(sin(dot(uv.xy, vec2(12.9898, 78.233))) * 43758.5453);
}

vec2 curve(vec2 uv){
	vec2 delta = uv - 0.5;
	float delta2 = dot(delta, delta);
    vec2 delta_offset = delta * (1.0+ delta2 * curvature * 0.01);
    return delta_offset + 0.5;
}

void fragment() {
    vec2 curved_uv = curve(SCREEN_UV);

    // 裁掉屏幕弯曲后露出的黑边
    if (curved_uv.x < 0.0 || curved_uv.x > 1.0 || curved_uv.y < 0.0 || curved_uv.y > 1.0) {
        COLOR = vec4(0.0, 0.0, 0.0, 1.0);
    } else {
        // 1. 色差效果 (RGB 分离)
        float r = texture(screen_texture, curved_uv + vec2(chromatic_aberration, 0.0)).r;
        float g = texture(screen_texture, curved_uv).g;
        float b = texture(screen_texture, curved_uv - vec2(chromatic_aberration, 0.0)).b;
        vec3 color = vec3(r, g, b);

        // 2. 扫描线
        float scanline = sin(curved_uv.y * scanline_count * 3.14159 * 2.0);
        scanline = (scanline + 1.0) * 0.5;
        color -= scanline * scanline_intensity;

        // 3. 动态噪点 (加入 TIME 让噪点跳动)
        float noise = (random(curved_uv + vec2(TIME * 15.0, TIME * 20.0)) - 0.5) * noise_intensity;
        color += noise;

        // 4. 四周暗角 (Vignette)
        vec2 vig_uv = curved_uv * (1.0 - curved_uv.yx);
        float vig = vig_uv.x * vig_uv.y * 100.0;
        vig = clamp(pow(vig, vignette_intensity), 0.0, 1.0);
        color *= vig;

        COLOR = vec4(color, 1.0);
    }
}