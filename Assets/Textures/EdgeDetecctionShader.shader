Shader "Custom/EdgeDetectionPostProcess"
{
    Properties
    {
        _EdgeColor ("Edge Color", Color) = (0,0,0,1)
        _Threshold ("Threshold", Range(0.01, 1)) = 0.2
    }
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            float4 _EdgeColor;
            float _Threshold;
            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float3 col = tex2D(_MainTex, i.uv).rgb;
                
                // Sobel filter to detect edges
                float dx = dFdx(col.r);
                float dy = dFdy(col.r);
                float edge = sqrt(dx * dx + dy * dy);

                if (edge > _Threshold)
                    return _EdgeColor; // Render edge color
                else
                    return float4(col, 1.0); // Render original color
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}