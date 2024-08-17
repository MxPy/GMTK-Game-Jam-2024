Shader "Custom/VHSEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        // skanlinie (CRT)
        _ScanlineIntensity ("Scanline Intensity", Range(0, 1)) = 0.2
        _ScanlineThickness ("Scanline Thickness", Range(0, 1)) = 0.2
        _ScanlineSpeed ("Scanline Speed", Range(0, 10)) = 1
        _VerticalOffset ("Vertical Offset", Range(0, 1)) = 0.1

        // zniekształcenia
        _DistortionStrength ("Distortion Strength", Range(0, 1)) = 0.5
        _DistortionOffset ("Distortion Offset", Float) = 0.0

        // ustawienie intensywności zakłóceń
        _GlitchIntensity ("Glitch Intensity", Range(0, 1)) = 0.5

        // linie statyczne
        _StaticLinesIntensity ("Static Lines Intensity", Range(0, 1)) = 0.205
        _StaticLinesSize ("Static Lines Size", Range(0, 0.1)) = 0.002

        // color shift
        _ColorShift ("Color Shift Amount", Range(0, 1)) = 0.05

        // efekt jitteru
        _TapeJitter ("Tape Jitter Amount", Range(0, 1)) = 0.02
        _JitterEnabled ("Enable Jitter", Float) = 0.0

        // efekt falowania
        _WaveFrequency ("Wave Frequency", Float) = 10.0
        _WaveAmplitude ("Wave Amplitude", Float) = 0.05
        _WaveSpeed ("Wave Speed", Float) = 1.0
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
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _ScanlineIntensity;
            float _ScanlineThickness;
            float _ScanlineSpeed;
            float _VerticalOffset;
            float _DistortionStrength;
            float _DistortionOffset;
            float _GlitchIntensity;
            float _StaticLinesIntensity;
            float _StaticLinesSize;
            float _ColorShift;
            float _TapeJitter;
            float _JitterEnabled;
            float _WaveFrequency;
            float _WaveAmplitude;
            float _WaveSpeed;

            // przetwarzanie danych wierzchołków
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // obliczenie przesunięcia jittera, jeśli włączone
                float jitter = _JitterEnabled > 0.5 ? sin(_Time.y * 10) * _TapeJitter : 0.0;
                o.texcoord = v.texcoord + float2(jitter, 0);
                return o;
            }

            // funkcja do przetwarzania pikseli
            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.texcoord;

                // skanlinie (CRT)
                float scanline = sin(uv.y * _ScanlineThickness + _Time.y * _ScanlineSpeed) * _ScanlineIntensity;
                uv.y += scanline - _VerticalOffset * 0.1;

                // zniekształcenia
                float distortion = frac(sin((uv.x + _DistortionOffset) * 10 + uv.y * 20) * 43758.5453) * _DistortionStrength * 0.1;
                uv += distortion;

                // glitchowanie obrazu
                float glitchOffset = _GlitchIntensity * (sin(_Time.y * 20.0) + sin(uv.y * 40.0)) * 0.5;
                uv.x += glitchOffset;
                // dodanie dodatkowych zakłóceń pionowych
                if (frac(sin(dot(uv.xy, float2(12.9898, 78.233))) * 43758.5453) < _GlitchIntensity * 0.5)
                {
                    uv.y += _GlitchIntensity * 0.1;
                }

                // efekt linii statycznych
                float staticLine = sin(uv.y * _StaticLinesSize * 100) * _StaticLinesIntensity;
                uv.y += staticLine;

                // VHS - kolor, szum i linie
                float noise = frac(sin(dot(uv * _Time.y, float2(12.9898, 78.233))) * 43758.5453);
                float lines = step(0.5, frac(uv.y * 30.0 + _Time.y * 5.0)) * 0.1;
                // przesunięcie kolorów RGB
                float2 redUV = uv + float2(_ColorShift, 0);
                float2 greenUV = uv;
                float2 blueUV = uv - float2(_ColorShift, 0);

                fixed4 redCol = tex2D(_MainTex, redUV);
                fixed4 greenCol = tex2D(_MainTex, greenUV);
                fixed4 blueCol = tex2D(_MainTex, blueUV);
                // lączenie kolorów w jeden kolor VHS
                fixed4 vhsCol = fixed4(redCol.r, greenCol.g, blueCol.b, 1.0);
                // dodanie szumu i linii do koloru
                vhsCol.rgb += noise * 0.1;
                vhsCol.rgb *= 1.0 - lines;

                // ustawienie falowania
                uv.y += sin(uv.x * _WaveFrequency + _Time.y * _WaveSpeed) * _WaveAmplitude;

                // pobieranie koloru z tekstury
                fixed4 col = tex2D(_MainTex, uv);
                col.rgb = lerp(col.rgb, vhsCol.rgb, 0.5);

                return col;
            }
            ENDCG
        }
    }
}
