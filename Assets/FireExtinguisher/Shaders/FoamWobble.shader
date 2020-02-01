Shader "Custom/FoamWobble" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Amount("Amount", Range(0.01, 2.0)) = 0.01
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		Cull off

		CGPROGRAM
		#pragma surface surf Standard vertex:vert
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed _Amount;

		void vert(inout appdata_full v) {
			float x = v.normal.x * (sin(123.0 + _Time * 80) + 1) * 0.5;
			float y = v.normal.y * (sin(23.0  + _Time * 80) + 1) * 0.5;
			float z = v.normal.z * (sin(13.0  + _Time * 80) + 1) * 0.5;
			v.vertex.xyz += float3(x,y,z) * _Amount;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color * 2;
			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
