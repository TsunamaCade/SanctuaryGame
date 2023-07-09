Shader "Custom/DiffuseLocalSpaceTextureBlend" {
    Properties {
        _Color ("Main Color", Color) = (1, 1, 1, 1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Scale ("Texture Scale", Float) = 1.0
        _BlendTex1 ("Texture 1", 2D) = "white" {}
        _BlendTex2 ("Texture 2", 2D) = "white" {}
        _BlendNoiseTex ("Noise Texture", 2D) = "white" {}
        _BlendAmount ("Blend Amount", Range(0, 1)) = 0.5
        _NoiseScale ("Noise Scale", Range(0, 1)) = 0.1
    }

    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _Color;
        float _Scale;
        sampler2D _BlendTex1;
        sampler2D _BlendTex2;
        sampler2D _BlendNoiseTex;
        float _BlendAmount;
        float _NoiseScale;

        struct Input {
            float3 localNormal;
            float3 localPos;
            float2 uv_BlendTex1;
            float2 uv_BlendTex2;
            float2 uv_BlendNoiseTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            float2 uv;
            fixed4 c;

            if (abs(IN.localNormal.x) > 0.5) {
                uv = IN.localPos.yz;
                c = tex2D(_BlendTex1, IN.uv_BlendTex1);
            }
            else if (abs(IN.localNormal.z) > 0.5) {
                uv = IN.localPos.xy;
                c = tex2D(_BlendTex2, IN.uv_BlendTex2);
            }
            else {
                uv = IN.localPos.xz;
                c = tex2D(_MainTex, uv * _Scale);
            }

            float noise = tex2D(_BlendNoiseTex, IN.uv_BlendNoiseTex * _NoiseScale).r;
            float blendFactor = smoothstep(0.5 - _BlendAmount, 0.5 + _BlendAmount, noise);

            fixed4 blendColor = lerp(c, tex2D(_MainTex, uv * _Scale), blendFactor);

            o.Albedo = blendColor.rgb * _Color.rgb;
            o.Alpha = blendColor.a * _Color.a;
        }
        ENDCG
    }
    FallBack "Legacy Shaders/VertexLit"
}