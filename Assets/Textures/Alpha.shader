// Upgrade NOTE: commented out 'float3 _WorldSpaceCameraPos', a built-in variable

Shader "ScreenUVWithFixedAlphaAndBillboard"
{
    Properties{
        _MainTex("Main Texture", 2D) = "white" {}
        _AlphaTex("Alpha Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" } 
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha // 启用透明混合

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION; // 顶点位置
                float2 uv : TEXCOORD0;    // 物体本地UV坐标
            };

            struct v2f
            {
                float2 uv : TEXCOORD0; // 保持物体UV用于alpha采样
                float4 vertex : SV_POSITION;
                float2 screenUV : TEXCOORD1; // 用于屏幕UV采样
            };

            // 摄像机的世界空间位置
            // uniform float3 _WorldSpaceCameraPos;

            v2f vert (appdata v)
            {
                v2f o;

                // 获取物体在世界空间中的位置
                float3 objectWorldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                // 计算摄像机与物体的方向
                float3 toCamera = normalize(_WorldSpaceCameraPos - objectWorldPos);

                // 计算摄像机的右向量（水平旋转）
                float3 right = cross(float3(0,1,0), toCamera);

                // 计算新的顶点位置，使物体朝向摄像机
                objectWorldPos += (right * v.vertex.x + float3(0,1,0) * v.vertex.y);

                // 将物体位置转换为裁剪空间
                o.vertex = UnityObjectToClipPos(float4(objectWorldPos, 1.0));

                o.uv = v.uv; // 保持对象的UV用于alpha贴图
                // 计算屏幕UV
                o.screenUV = float2(1,_ProjectionParams.x)*(o.vertex.xy/o.vertex.w+1)*0.5;

                return o;
            }

            sampler2D _MainTex;
            sampler2D _AlphaTex;

            fixed4 frag (v2f i) : SV_Target
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
}