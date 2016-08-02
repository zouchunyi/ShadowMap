Shader "Unlit/Projector"
{
   Properties {
      _ShadowTex ("Projected Image", 2D) = "white" {}
   }
   SubShader {
      Pass {
         Blend One One
         ZWrite Off
         Offset -1, -1
 
         CGPROGRAM
 
         #pragma vertex vert
         #pragma fragment frag
 
         uniform sampler2D _ShadowTex;
         uniform float4x4 _Projector;
 
          struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 posProj : TEXCOORD0;
         };
 
         vertexOutput vert(vertexInput input)
         {
            vertexOutput output;

            output.posProj = mul(_Projector, input.vertex);
            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
         	float4 col = tex2D(_ShadowTex,input.posProj.xy / input.posProj.w) * 0.1;

//         	if (col.r == 1)
//         	{
//         		col.a = 0;
//         	}else
//         	{
//         		col = float4(0,0,0,1);
//         	}
         	return col;
         }
 
         ENDCG
      }
   }
}
