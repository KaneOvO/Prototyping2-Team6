Shader "ScreenUV"
{
    Properties{
        _MainTex("Tex", 2D) = ""
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" } 
        LOD 200

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

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float2 screenUV : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                // 计算屏幕UV
                o.screenUV = float2(1,_ProjectionParams.x)*(o.vertex.xy/o.vertex.w+1)*0.5;
                return o;
            }

            sampler2D _MainTex;
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.screenUV);
                // just invert the colors
                // col = 1 - col;
                return col;
            }
            ENDCG
        }
    }
}