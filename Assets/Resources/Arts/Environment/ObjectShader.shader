Shader "Custom/ObjectShader"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			Blend One OneMinusSrcAlpha

			CGPROGRAM
			#pragma surface surf CustomLambert vertex:vert nofog nolightmap nodynlightmap keepalpha noinstancing
			#pragma multi_compile_local _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnitySprites.cginc"

			struct Input
			{
				float2 uv_MainTex;
				fixed4 color;
			};



			void vert(inout appdata_full v, out Input o)
			{
				v.vertex = UnityFlipSprite(v.vertex, _Flip);

				#if defined(PIXELSNAP_ON)
				v.vertex = UnityPixelSnap(v.vertex);
				#endif

				UNITY_INITIALIZE_OUTPUT(Input, o);
				o.color = v.color * _Color * _RendererColor;
			}

			void surf(Input IN, inout SurfaceOutput o)
			{
				fixed4 c = SampleSpriteTexture(IN.uv_MainTex) * IN.color;
				o.Albedo = c.rgb * c.a;
				o.Alpha = c.a;
			}

			half4 LightingCustomLambert(SurfaceOutput s, half3 lightDir, half atten) {
				half NdotL = dot(s.Normal, lightDir);
				half diff = NdotL * 0.5 + 0.5;
				half4 c;
				half3 white = float3(1, 1, 1);
				//It seems to fall in performance, so I'll fix it
				half3 refLight = lerp(_LightColor0.rgb, white, NdotL * NdotL * NdotL * NdotL);
				c.rgb = s.Albedo * refLight * (atten * 2);
				c.a = s.Alpha;
				return c;
			}

			ENDCG
		}

			Fallback "Transparent/VertexLit"
}
