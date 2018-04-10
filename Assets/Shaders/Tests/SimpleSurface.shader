Shader "Test/SimpleSurface" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_SecondTex("Albedo (RGB)", 2D) = "white" {}
		_EffectTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Test ("Test", Range(0,1)) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _SecondTex;
		sampler2D _EffectTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		half _Test;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float2 uv = IN.uv_MainTex;

			float force = (cos(_Time.y)*0.5f + 0.5f);

			fixed4 cEffect = tex2D(_EffectTex, uv + _Time.x*0.2f);


			if (cEffect.r <= force)
			{
				fixed4 c = tex2D(_MainTex, uv);
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}
			else if (cEffect.r > 0.1f && cEffect.r <= force + 0.1f)
			{
				fixed4 c = tex2D(_MainTex, uv + _Test * cEffect.r * (1-force));
				o.Albedo = lerp(c, _Color, cEffect.r);
				o.Alpha = c.a;
			}
			else
			{
				fixed4 c = tex2D(_MainTex, uv + _Test * cEffect.r * (1-force));
				fixed4 c2 = tex2D(_SecondTex, uv);
				o.Albedo = lerp(c, c2, cEffect.r);
				o.Alpha = c.a;
			}

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
