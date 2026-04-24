


Shader "CustomMyShader" 
{
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float depth : TEXCOORD0; // 
            };

            uniform float4x4 _ModelMatrix;
            uniform float4x4 _ViewMatrix;
            uniform float4x4 _ProjectionMatrix;

            fixed4 _MaterialColor;

            // para neblina
            uniform float4 _FogColor;
            uniform float _FogDensity;

            v2f vert(appdata v)
            {
                v2f o;

                float4 worldPos = mul(_ModelMatrix, v.vertex);
                float4 viewPos = mul(_ViewMatrix, worldPos);

                o.vertex = mul(_ProjectionMatrix, viewPos);

                // guardamos distancia a cámara
                o.depth = -viewPos.z;

                o.color = v.color;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // factor de niebla
                float fogFactor = saturate(i.depth * _FogDensity);

                // mezcla de color
                fixed4 finalColor = lerp(_MaterialColor, _FogColor, fogFactor);

                return finalColor;
            }
            ENDCG
        }
    }
}
