Shader "Custom/Test/Shader2"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline"}
        LOD 200

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 color : COLOR;
                float3 lightVec : TEXCOORD1;
                float3 worldNormal : TEXCOORD2;
            };

            v2f vert(appdata_full i)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(i.vertex);
                o.uv = TRANSFORM_TEX(i.texcoord, _MainTex);
                o.worldNormal = UnityObjectToWorldDir(i.normal);
                o.color = ShadeVertexLights(i.vertex, i.normal);
                return o;            
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv)*_Color;+float4(i.color.x, i.color.y, i.color.z, 1);
                return col;
            }
            ENDHLSL
        }
    }

    FallBack "Diffuse"
}
