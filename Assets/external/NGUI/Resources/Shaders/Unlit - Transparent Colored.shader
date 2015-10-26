Shader "Unlit/Transparent Colored"{
 Properties{
     _MainTex ("Base (RGB), Alpha (angel)", 2D) = "white" {}     
  _Alpha ("Alpha", Float) = 6
 }
 
 Subshader {
     Tags {"Queue"="Transparent"}
     ZWrite Off
     Blend SrcAlpha OneMinusSrcAlpha
     Pass {
         CGPROGRAM
         #pragma vertex vert
         #pragma fragment frag
   
  struct appdata_t
  {
   float4 vertex : POSITION;
   half4 color : COLOR;
   float2 texcoord : TEXCOORD0;
  };

         struct v2f {
             float4 position : SV_POSITION;
             float2 uv_mainTex : TEXCOORD;
    half4 color : COLOR;
         };
 
         uniform float4 _MainTex_ST;
   float _Alpha;
   
   v2f vert (appdata_t v) {
             v2f o;
             o.position = mul(UNITY_MATRIX_MVP, v.vertex);
             o.uv_mainTex = v.texcoord;
             o.color = v.color;
             return o;
         }
 
         uniform sampler2D _MainTex;

         fixed4 frag (v2f IN) : COLOR {
             fixed4 mainTex = tex2D(_MainTex, IN.uv_mainTex);
             fixed4 fragColor;
    
    half grayColor = (mainTex.r + mainTex.g + mainTex.b) * 0.33;
    half inColorSum = IN.color.r + IN.color.g + IN.color.b;
    half4 fullColor = mainTex * IN.color;
    
    half blackAndWhiteOrColor = step(0.01, inColorSum);
    half invBlackAndWhiteOrColor = 1 - blackAndWhiteOrColor;

    fragColor.rgb = invBlackAndWhiteOrColor * grayColor + blackAndWhiteOrColor * fullColor.rgb;

             fragColor.a = mainTex.a * IN.color.a;
             return fragColor;
         }
         ENDCG   
     }
 }
}