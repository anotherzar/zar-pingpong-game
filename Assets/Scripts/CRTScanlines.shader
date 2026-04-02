Shader "Custom/CRTScanlines"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _ScanlineIntensity ("Scanline Intensity", Range(0, 1)) = 0.5
        _ScanlineCount ("Scanline Count", Float) = 500
        _VignetteAmount ("Vignette Amount", Range(0, 2)) = 0.5
        _Curvature ("Curvature", Range(0, 1)) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

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
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ScanlineIntensity;
            float _ScanlineCount;
            float _VignetteAmount;
            float _Curvature;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float2 curve(float2 uv)
            {
                uv = uv * 2.0 - 1.0;
                float2 offset = abs(uv.yx) * _Curvature;
                uv = uv + uv * offset * offset;
                uv = uv * 0.5 + 0.5;
                return uv;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = curve(i.uv);
                
                // Bordes negros si se sale de la curvatura
                if (uv.x < 0.0 || uv.x > 1.0 || uv.y < 0.0 || uv.y > 1.0)
                    return fixed4(0,0,0,1);

                fixed4 col = tex2D(_MainTex, uv);
                
                // Scanlines
                float scanline = sin(uv.y * _ScanlineCount) * _ScanlineIntensity;
                col.rgb -= scanline * col.rgb;
                
                // Vignette Simple
                float vignette = uv.x * uv.y * (1.0 - uv.x) * (1.0 - uv.y);
                vignette = pow(vignette * 15.0, _VignetteAmount);
                col.rgb *= vignette;

                return col;
            }
            ENDCG
        }
    }
}
