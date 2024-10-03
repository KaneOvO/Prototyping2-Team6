Shader "ScreenUVWithAlphaTest"
{
    Properties{
        _MainTex("Main Texture", 2D) = "white" {}
        _AlphaTex("Alpha Texture", 2D) = "white" {}
    }
        SubShader
    {
        Tags { "RenderType" = "Transparent" }
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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                // 计算屏幕UV
                o.screenUV = float2(1,_ProjectionParams.x) * (o.vertex.xy / o.vertex.w + 1) * 0.5;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _AlphaTex;

            fixed4 frag(v2f i) : SV_Target
            {
                // 使用屏幕UV采样纹理贴图
                fixed4 col = tex2D(_MainTex, i.screenUV);
            // 使用物体UV采样Alpha贴图
            fixed alpha = tex2D(_AlphaTex, i.uv).r; // 使用Alpha贴图的红色通道作为Alpha值
            col.a = alpha; // 将Alpha值赋予最终颜色
            return col;
        }
        ENDCG
    }
    }
        FallBack "Diffuse"
}