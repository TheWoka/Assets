Shader "Custom/TileLighting"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        [PerRendererData] _LightMap ("Light Map", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _WorldBounds ("World Bounds (minX, minY, width, height)", Vector) = (0,0,1,1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "PreviewType"="Plane" }
        Cull Off Lighting Off ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t { float4 vertex:POSITION; float4 color:COLOR; float2 texcoord:TEXCOORD0; };
            struct v2f { float4 vertex:SV_POSITION; fixed4 color:COLOR; float2 texcoord:TEXCOORD0; float3 worldPos:TEXCOORD1; };

            sampler2D _MainTex; sampler2D _LightMap; fixed4 _Color; float4 _WorldBounds;

            v2f vert(appdata_t IN) {
                v2f o;
                o.vertex = UnityObjectToClipPos(IN.vertex);
                o.texcoord = IN.texcoord;
                o.color = IN.color * _Color;
                o.worldPos = mul(unity_ObjectToWorld, IN.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f IN) : SV_Target {
                fixed4 col = tex2D(_MainTex, IN.texcoord) * IN.color;
                float2 lightUV = (IN.worldPos.xy - _WorldBounds.xy) / _WorldBounds.zw;
                lightUV = clamp(lightUV, 0.0, 1.0);
                fixed4 light = tex2D(_LightMap, lightUV);
                col.rgb *= max(light.r, 0.15);
                return col;
            }
            ENDCG
        }
    }
}