Shader "Custom/EmissionAnimation" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Emission ("Emission (RGB)", Color) = (1,1,1,1)
		_EmissionTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows

		sampler2D _MainTex;
		sampler2D _EmissionTex;
		
        fixed4 _Emission;
       
		struct Input {
			float2 uv_MainTex;
			float2 uv_EmissionTex;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 e = tex2D (_EmissionTex, IN.uv_EmissionTex);
			o.Albedo = c.rgb;
			
			if (IN.uv_EmissionTex.x > _SinTime.y)
			{
			    o.Emission = _Emission * e;
			}
			else 
			{
			    o.Emission = fixed3(0.0, 0.0, 0.0);
			}
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
