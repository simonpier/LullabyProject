Shader "Custom/PlayerShader"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[HDR]_OutlineColor("OutlineColor", Color) = (2,2,2,1)
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
		ZWrite On
		Blend One OneMinusSrcAlpha
		ZTest LEqual

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert nofog nolightmap nodynlightmap keepalpha noinstancing
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
			if (o.Normal.z > 0.0f) {
				o.Normal *= -1.0f;
			}
			o.Alpha = c.a;
		}
		ENDCG

		//Mask Pass
		ZTest Greater

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert nofog nolightmap nodynlightmap keepalpha noinstancing
		#pragma multi_compile_local _ PIXELSNAP_ON
		#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
		#include "UnitySprites.cginc"

		fixed4 _OutlineColor;
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
			bool out_flag = false;
			out_flag = out_flag || (SampleSpriteTexture(IN.uv_MainTex + float2(0.02f, 0))).a < 0.2f;
			out_flag = out_flag || (SampleSpriteTexture(IN.uv_MainTex + float2(-0.02f, 0))).a < 0.2f;
			out_flag = out_flag || (SampleSpriteTexture(IN.uv_MainTex + float2(0, 0.02f))).a < 0.2f;
			out_flag = out_flag || (SampleSpriteTexture(IN.uv_MainTex + float2(0, -0.02f))).a < 0.2f;
		
			if(out_flag){
				o.Albedo = _OutlineColor * c.a;
			}
			else {
				o.Albedo = c.rgb * c.a * 0.01f;
			}
			//o.Albedo = float3(1, 1, 1);
			o.Alpha = c.a;
		
		}
		ENDCG
	}
	Fallback "Transparent/VertexLit"
}