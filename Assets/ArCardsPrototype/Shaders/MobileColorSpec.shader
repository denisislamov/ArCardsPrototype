// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Mobile/ColorSpec" {
	Properties{
		_Shininess("Shininess", Range(0.03, 1)) = 0.078125
		_Color("Color", Color) = (1, 1, 1, 1)
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 150
		Cull Off

		CGPROGRAM
#pragma surface surf MobileBlinnPhong exclude_path:prepass nolightmap noforwardadd halfasview interpolateview

		inline fixed4 LightingMobileBlinnPhong(SurfaceOutput s, fixed3 lightDir, fixed3 halfDir, fixed atten)
	{
		fixed diff = max(0, dot(s.Normal, lightDir));
		fixed nh = max(0, dot(s.Normal, halfDir));
		fixed spec = pow(nh, s.Specular * 128) * s.Gloss;

		fixed4 c;
		c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * atten;
		UNITY_OPAQUE_ALPHA(c.a);
		return c;
		}

		uniform fixed4 _Color;
		half _Shininess;

	struct Input {
		float2 uv_MainTex;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		o.Specular = _Shininess;
		o.Albedo = _Color.rgb;
		o.Alpha = _Color.a;
	}
	ENDCG
	}

		Fallback "Mobile/VertexLit"
}
