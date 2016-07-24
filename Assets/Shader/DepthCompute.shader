Shader "Unlit/DepthCompute"
{
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float2 depth : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.depth.xy = o.vertex.zw;
				return o;
			}
			
			float4 frag (v2f i) : SV_Target
			{
				float d = i.depth.x / i.depth.y;
				float4 col = EncodeFloatRGBA(d);
				return col;
			}
			ENDCG
		}
	}
}
