Shader "Custom/BorderBlink"
{
    Properties
    {
        _Color ("Border Color", Color) = (1,0,0,1)
        _Thickness ("Border Thickness", Range(0, 0.2)) = 0.05
        _Fade ("Fade Strength", Range(0, 0.5)) = 0.1
        _Phase ("Phase", Range(0, 1)) = 0.0
    }
    SubShader
    {
        Tags {"Queue"="Overlay" "RenderType"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _Color;
            float _Thickness;
            float _Fade;
            float _Speed;
            float _Phase;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                float distToEdge = min(min(uv.x, 1.0 - uv.x), min(uv.y, 1.0 - uv.y));

                float border = smoothstep(_Thickness + _Fade, _Thickness, distToEdge);

                float fadeEffect = smoothstep(_Thickness + _Fade, _Thickness, distToEdge);

                float blinkEffect = (frac(_Time.y * _Speed) * 0.5 + 0.5);

                float alpha = border * fadeEffect * blinkEffect;

                if (_Phase < 0.5)
                {
                    alpha *= smoothstep(0, 0.5, _Phase);
                }
                else
                {
                    alpha *= smoothstep(1, 0.5, _Phase);
                }

                return fixed4(_Color.rgb, alpha);
            }
            ENDCG
        }
    }
}