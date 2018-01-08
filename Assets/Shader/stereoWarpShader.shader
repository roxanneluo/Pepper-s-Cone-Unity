// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShader/StereoWarpShader"
{
	Properties
	{
		_RenderedTexL("Rendered Texture for Left Eye", 2D) = "white" {}
		_RenderedTexR("Rendered Texture for right Eye", 2D) = "white" {}
		_MapTexL("Map Texture for left eye", 2D) = "white"  {}
		_MapTexR("Map Texture for right eye", 2D) = "white"  {}
	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Off

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	uniform float4	_TexRotationVec;	//serialization of inversed rotation matrix

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
	};

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		return o;
	}

	sampler2D _RenderedTexL, _RenderedTexR;
	sampler2D _MapTexL, _MapTexR;


	bool inside(float2 uv) {
		static const float EPS = 1e-3;
		return EPS <= uv.x && uv.x <= 1-EPS && EPS <= uv.y && uv.y <= 1-EPS;
	}

	uniform float _power;
	uniform float _alpha;
	float4 frag(v2f i) : SV_Target
	{
		const fixed4 BLACK = fixed4(0, 0, 0, 0);
		const float2 HALF = float2(0.5, 0.5);
		float2x2 rotMat = {_TexRotationVec.x, _TexRotationVec.y, _TexRotationVec.z, _TexRotationVec.w};
		//float2 mapUV = mul(_TextureRotation, i.uv-HALF) + HALF;
		float2 mapUV = mul(rotMat, i.uv-HALF) + HALF;
		if (!inside(mapUV)) return BLACK;

		float4 mapL = tex2D(_MapTexL, mapUV);
		float4 mapR = tex2D(_MapTexR, mapUV);
		float2 renderedTexUVL = float2(mapL.x, mapL.y);
		float2 renderedTexUVR = float2(mapR.x, mapR.y);
		float4 col = BLACK;
		// left eye: red, right eye: blue and green.
		if (inside(renderedTexUVL)) {
			float4 tex_color = tex2D(_RenderedTexL, renderedTexUVL);
			col.x = tex_color.x;col[3] = tex_color[3];

			//col.x = renderedTexUVL.x;col.y = renderedTexUVL.y;
		}
		if (inside(renderedTexUVR)) {
			float4 tex_color = tex2D(_RenderedTexR, renderedTexUVR);
			//col.z = renderedTexUVR.x; col[3] = renderedTexUVR.y;
			for (int c = 1; c < 4; ++c)
				col[c] = tex_color[c];
		}
		//if (!inside(renderedTexUV)) return BLACK;
//		fixed4 col = map;
		col = _alpha * pow(col, _power);
		return col;
	}
		ENDCG
	}
	}
}
