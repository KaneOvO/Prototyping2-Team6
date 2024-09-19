}Shader "Universal Render Pipeline/Custom/ScreenSpaceUV"
{
    Properties
    {
        [MainColor] _BaseColor("BaseColor", Color) = (1,1,1,1)
        [MainTexture] _BaseMap("BaseMap", 2D) = "white" {}
    }
 
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalRenderPipeline"}
 
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
 
        CBUFFER_START(UnityPerMaterial)
        float4 _BaseMap_ST;
        half4 _BaseColor;
        CBUFFER_END
        ENDHLSL
 
        Pass
        {
            Tags { "LightMode"="UniversalForward" }
 
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _SHADOWS_SOFT
 
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
                      
            struct Attributes
            {
                float4 positionOS   : POSITION;
            };
 
            struct Varyings
            {
                float3 positionWS   : TEXCOORD4;
                float4 positionHCS  : SV_POSITION;
            };
 
            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);
 
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
 
                VertexPositionInputs positionInputs = GetVertexPositionInputs(IN.positionOS.xyz);
 
                OUT.positionHCS = positionInputs.positionCS;
                OUT.positionWS = positionInputs.positionWS;
                return OUT;
            }
 
            half4 frag(Varyings IN) : SV_Target
            {
                float4 shadowCoord = TransformWorldToShadowCoord(IN.positionWS);
                
                Light mainLight = GetMainLight(shadowCoord);
 
                float2 uv = (IN.positionHCS / _ScreenParams.xy) * _BaseMap_ST.xy + _BaseMap_ST.zw;
                half3 baseColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv) * _BaseColor;
                half3 finalColor = baseColor * mainLight.shadowAttenuation;
                return half4(finalColor, 1.0);
            }
            ENDHLSL
        }
 
        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
    }
}
