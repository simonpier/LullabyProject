Shader "CustomLights/PointArea" {
	SubShader{
		Tags { "Queue" = "Transparent-1" }

	CGINCLUDE
	#define POINT
	#include "UnityCG.cginc"
	#include "UnityPBSLighting.cginc"
	#include "UnityDeferredLibrary.cginc"

	half4 _CustomLightColor;

	float4 _CustomLightParams;
	#define _CustomLightInnerAttenuation _CustomLightParams.x
	#define _CustomLightBufferSize _CustomLightParams.y
	#define _CustomLightInvSqRadius _CustomLightParams.z
	#define _CustomLightSpotSize _CustomLightParams.w

	sampler2D _CameraGBufferTexture0;
	sampler2D _CameraGBufferTexture1;
	sampler2D _CameraGBufferTexture2;

	half3 CalcSphereLightToLight(float3 pos, float3 lightPos, float3 eyeVec, half3 normal, float sphereRad)
	{
		half3 viewDir = -eyeVec;
		half3 r = reflect(viewDir, normal);

		float3 L = lightPos - pos;
		float3 centerToRay = dot(L, r) * r - L;
		float3 closestPoint = L + centerToRay * saturate(sphereRad / length(centerToRay));
		return normalize(closestPoint);
	}

	void DeferredCalculateLightParams(
		unity_v2f_deferred i,
		out float3 outWorldPos,
		out float2 outUV,
		out half3 outLightDir,
		out float outAtten,
		out float outFadeDist)
	{
		i.ray = i.ray * (_ProjectionParams.z / i.ray.z);
		float2 uv = i.uv.xy / i.uv.w;

		float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv);
		depth = Linear01Depth(depth);
		float4 vpos = float4(i.ray * depth,1);
		float3 wpos = mul(unity_CameraToWorld, vpos).xyz;

		float3 lightPos = float3(unity_ObjectToWorld[0][3], unity_ObjectToWorld[1][3], unity_ObjectToWorld[2][3]);

		float3 tolight = wpos - lightPos;
		half3 lightDir = -normalize(tolight);

		float att = dot(tolight, tolight) * _CustomLightInvSqRadius;
		float atten = tex2D(_LightTextureB0, att.rr).UNITY_ATTEN_CHANNEL;
		float3 z = (0, 0, 1);
		float tes = distance(wpos.xy, lightPos.xy);

		if (tes < _CustomLightSpotSize) atten = max((_CustomLightSpotSize - tes) / _CustomLightSpotSize * _CustomLightInnerAttenuation + (1.0f - _CustomLightInnerAttenuation), atten*0.5f);
		else if (tes < _CustomLightSpotSize + _CustomLightBufferSize) atten = max((_CustomLightSpotSize + _CustomLightBufferSize - tes) / _CustomLightBufferSize * (1.0f - _CustomLightInnerAttenuation), atten*0.5f);
		else atten *= 0.5f;

		outWorldPos = wpos;
		outUV = uv;
		outLightDir = lightDir;
		outAtten = atten;
		outFadeDist = 0;
	}

	half4 CalculateLight(unity_v2f_deferred i)
	{
		float3 wpos;
		float2 uv;
		float atten, fadeDist;
		UnityLight light = (UnityLight)0;
		DeferredCalculateLightParams(i, wpos, uv, light.dir, atten, fadeDist);

		half4 gbuffer0 = tex2D(_CameraGBufferTexture0, uv);
		half4 gbuffer1 = tex2D(_CameraGBufferTexture1, uv);
		half4 gbuffer2 = tex2D(_CameraGBufferTexture2, uv);

		light.color = _CustomLightColor.rgb * atten;
		half3 baseColor = gbuffer0.rgb;
		half3 specColor = gbuffer1.rgb;
		half3 normalWorld = gbuffer2.rgb * 2 - 1;
		normalWorld = normalize(normalWorld);
		half oneMinusRoughness = gbuffer1.a;
		float3 eyeVec = normalize(wpos - _WorldSpaceCameraPos);

		float3 lightPos = float3(unity_ObjectToWorld[0][3], unity_ObjectToWorld[1][3], unity_ObjectToWorld[2][3]);
		float3 lightAxisX = normalize(float3(unity_ObjectToWorld[0][0], unity_ObjectToWorld[1][0], unity_ObjectToWorld[2][0]));

		light.dir = CalcSphereLightToLight(wpos, lightPos, eyeVec, normalWorld, _CustomLightBufferSize);

		half oneMinusReflectivity = 1 - SpecularStrength(specColor.rgb);
		light.ndotl = LambertTerm(normalWorld, light.dir);

		UnityIndirect ind;
		UNITY_INITIALIZE_OUTPUT(UnityIndirect, ind);
		ind.diffuse = 0;
		ind.specular = 0;

		half4 res = half4(baseColor * _CustomLightColor.rgb * atten, 1);
		return res;
	}
	ENDCG

	Pass {
		Fog { Mode Off }
		ZWrite Off
		ZTest Always
		Blend One One
		Cull Front

		CGPROGRAM
		#pragma target 3.0
		#pragma vertex vert
		#pragma fragment frag
		#pragma exclude_renderers nomrt

		unity_v2f_deferred vert(float4 vertex : POSITION)
		{
			unity_v2f_deferred o;
			o.pos = UnityObjectToClipPos(vertex);
			o.uv = ComputeScreenPos(o.pos);
			o.ray = UnityObjectToViewPos(vertex).xyz * float3(-1, -1, 1);
			return o;
		}

		half4 frag(unity_v2f_deferred i) : SV_Target
		{
			return CalculateLight(i);
		}

		ENDCG
	}

	}
		Fallback Off
}
