Shader "Shader Scripts/Wireframe" {
    Properties {
        _COLOR("Color", Color) = (1, 1, 1, 1)
        _THICKNESS("Wireframe thickness", Range(0.0, 0.005)) = 0.0025
        _TRANSPARENCY("Transparency", Range(0.0, 1)) = 0.5
    }

    SubShader {
        Tags { 
            "Queue" = "Transparent" 
            "RenderType" = "Transparent" 
        }

        LOD 200

        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Back 

            CGPROGRAM

            #pragma vertex vertexFunction
            #pragma fragment fragmentFunction
            #pragma geometry geometryFunction
            #include "UnityCG.cginc" 

            struct v2g {
                float4 position: SV_POSITION;
            };

            struct g2f {
                float4 position: SV_POSITION;
                float3 bary: TEXCOORD0;
            };

            v2g vertexFunction(appdata_base IN)
            {
                v2g OUT;
                OUT.position = UnityObjectToClipPos(IN.vertex);

                return OUT;
            }

            [maxvertexcount(3)]
            void geometryFunction(triangle v2g IN[3], inout TriangleStream<g2f> triStream)
            {
                g2f OUT;
                OUT.position = IN[0].position;
                OUT.bary = float3(1, 0, 0);
                triStream.Append(OUT);
                OUT.position = IN[1].position;
                OUT.bary = float3(0, 0, 1);
                triStream.Append(OUT);
                OUT.position = IN[2].position;
                OUT.bary = float3(0, 1, 0);
                triStream.Append(OUT);
            }

            float _THICKNESS;
            fixed4 _COLOR;
            float _TRANSPARENCY;
            fixed4 fragmentFunction(g2f IN) : SV_Target
            {
                float value = min(IN.bary.x, (min(IN.bary.y, IN.bary.z)));
                value = exp2(-1 / _THICKNESS * value * value);
                fixed4 color = _COLOR;
                color.a = _TRANSPARENCY;

                return color * value;
            }

            ENDCG
        }

    }
}