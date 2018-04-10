Shader "Test/SimpleUnlit"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_EffectTex("Texture Effect", 2D) = "white" {}
		_Bump("Bump", Range(0,2)) = 0.5
		_TimeFactor("TimeFactor", Range(0,2)) = 0.5
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _EffectTex;
			float4 _MainTex_ST;
			float _Bump;
			float _TimeFactor;
			
			v2f vert (appdata v)
			{
				v2f o;

				float4 texEffect = tex2Dlod(_EffectTex, float4(v.uv.xy + _Time.y*_TimeFactor, 0, 0));
				v.vertex.y += texEffect.r*_Bump;
				o.vertex = UnityObjectToClipPos(v.vertex);

				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
