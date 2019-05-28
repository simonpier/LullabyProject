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
			"Queue" = "AlphaTest"
			"IgnoreProjector" = "True"
			"RenderType" = "Opaque"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Blend One Zero
		//ZWrite Off
		ZTest Always
		//Offset 0, -1
		//Lighting On

		CGPROGRAM
		#pragma surface surf Standard keepalpha
		#pragma vertex vert
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

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = SampleSpriteTexture(IN.uv_MainTex) * IN.color;
			o.Albedo = c.rgb * c.a;
			if (o.Normal.z > 0.0f) {
				o.Normal *= -1.0f;
			}
			//o.Albedo = o.Normal;
			o.Alpha = c.a;
		}
		ENDCG
	}
}