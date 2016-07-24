Shader "Unlit/ShadowMap"
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
				float4 vertex : SV_POSITION;
				float4 litPos:TEXCOORD0;
			};

			uniform sampler2D _shadowMap;
			uniform float4x4 _shadowMapMatrix;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				float4 pos = mul(_Object2World, v.vertex);
				o.litPos = mul(_shadowMapMatrix,pos);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float2 shadowTexc = 0.5 * i.litPos.xy / i.litPos.w + float2(0.5,0.5);
				float shadowDepth = tex2D(_shadowMap,shadowTexc).r;
				shadowDepth = DecodeFloatRGBA(shadowDepth);

				float litZ = i.litPos.z / i.litPos.w;

				if (litZ <= shadowDepth)
				{
					return 1;
				}else
				{
					return 0.3;
				}
			}
			ENDCG
		}
	}
}
