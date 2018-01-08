// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShader/WarpShader"
{
	Properties
	{
		RenderedTex("Rendered Texture", 2D) = "white" {}
		MapTex("Map Texture", 2D) = "white"  {}
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

			sampler2D RenderedTex;
			sampler2D MapTex;


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

				float4 map = tex2D(MapTex, mapUV);
				float2 renderedTexUV = float2(map.x, map.y);
				if (!inside(renderedTexUV)) return BLACK;
					//		fixed4 col = map;
				fixed4 col = _alpha * pow(tex2D(RenderedTex, renderedTexUV), _power);
				return col;
			}
			ENDCG
		}
	}
}
