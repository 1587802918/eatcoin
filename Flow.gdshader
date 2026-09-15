shader_type canvas_item;

// 紫色系双色配置（可直接在检查器吸色微调）
uniform vec4 color_light : source_color = vec4(0.58, 0.28, 0.78, 1.0); // 亮紫
uniform vec4 color_dark  : source_color = vec4(0.32, 0.12, 0.48, 1.0); // 深紫

// 控制参数
uniform float speed : hint_range(0.01, 2.0,0.001) = 0.015;       // 流动速度（色块建议偏慢，更有熔岩/暗流感）
uniform float scale : hint_range(1.0, 20.0) = 4.5;        // 块状大小（越小块面越大）
uniform float threshold : hint_range(0.0, 1.0) = 0.5;     // 亮暗色块的面积占比
uniform float sharpness : hint_range(0.001, 0.2) = 0.02;  // 边缘硬度（越小边缘越硬，切出几何感）

// 基础网格噪点（产生带一定几何感的基底）
float hash(vec2 p) {
    p = fract(p * vec2(123.34, 456.21));
    p += dot(p, p + 45.32);
    return fract(p.x * p.y);
}

// 值噪点平滑插值
float noise(vec2 p) {
    vec2 i = floor(p);
    vec2 f = fract(p);
    // 五次平滑曲线，避免边缘过于规则圆滑
    vec2 u = f * f * f * (f * (f * 6.0 - 15.0) + 10.0);

    float a = hash(i + vec2(0.0, 0.0));
    float b = hash(i + vec2(1.0, 0.0));
    float c = hash(i + vec2(0.0, 1.0));
    float d = hash(i + vec2(1.0, 1.0));

    return mix(mix(a, b, u.x), mix(c, d, u.x), u.y);
}

// 域扭曲（产生不规则流动褶皱）
float fbm(vec2 p) {
    float v = 0.0;
    float a = 0.5;
    mat2 rot = mat2(vec2(cos(0.5), sin(0.5)), vec2(-sin(0.5), cos(0.5)));
    for (int i = 0; i < 4; i++) {
        v += a * noise(p);
        p = rot * p * 2.0;
        a *= 0.5;
    }
    return v;
}

void fragment() {
    vec2 uv = UV * scale;
    float t = 0.2 * TIME * speed;

    // 核心修改：让基础偏移向量在 X 和 Y 上同时保持正方向推进（产生从左上往右下的移动趋势）
    vec2 flow_dir = vec2(t, t);

    // 两层域扭曲均叠加上 flow_dir
    vec2 q = vec2(fbm(uv - flow_dir * 0.8 + vec2(0.0, 0.0)),
                  fbm(uv - flow_dir * 0.8 + vec2(5.2, 1.3)));

    vec2 r = vec2(fbm(uv + 2.0 * q - flow_dir * 0.5 + vec2(1.7, 9.2)),
                  fbm(uv + 2.0 * q - flow_dir * 0.5 + vec2(8.3, 2.8)));

    float f = fbm(uv + 1.5 * r - flow_dir * 0.3);

    // 硬切步进保持不变
    float pattern = smoothstep(threshold - sharpness, threshold + sharpness, f);

    // 混合两档紫色
    vec3 final_color = mix(color_dark.rgb, color_light.rgb, pattern);

    COLOR = vec4(final_color, 1.0);
}