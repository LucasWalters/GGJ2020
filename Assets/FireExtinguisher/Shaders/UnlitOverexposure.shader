Shader "Custom/UnlitOverexposure" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Overexposure("Overexposure", Range(1.0, 4.0)) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Transparent" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Unlit alpha:auto
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;
		fixed  _Overexposure;

		half4 LightingUnlit(SurfaceOutput s, half3 lightDir, half atten)
			{ return half4(s.Albedo.rgb, s.Alpha); }

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color * _Overexposure;
			o.Albedo = c.rgb;
			o.Alpha  = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
