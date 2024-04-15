Shader "Unlit/starshader"
{
    Properties
    {
        _DiffuseColor("DiffuseColor", Color) = (1,1,1,1)
        _LightDirection("LightDirection", Vector) = (0,-1,0,0)
        _SpecularColor("SpecularColor", Color) = (1,1,1,1)
        _Shininess("Shininess", Range(1, 140)) = 50
        _TriangleNormal("TriangleNormal", Vector) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma target 4.5
            #pragma vertex vert
            #pragma fragment frag
           
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float3 triangleNormal : TEXCOORD1;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 triangleNormal : TEXCOORD1;
            };

            float4 _DiffuseColor;
            float4 _LightDirection;
            float _Shininess;
            float4 _SpecularColor;
            float3 _TriangleNormal;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.triangleNormal = v.triangleNormal;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float ambientStrength = 0.2;
                float3 ambient = ambientStrength * float3(0.0f, 0.0f, 1.0f);
                float3 viewDir = normalize(_WorldSpaceCameraPos - mul(unity_ObjectToWorld, i.vertex));
                float3 lightDir = normalize(_LightDirection.xyz);
                float diff = max(dot(i.triangleNormal, lightDir), 0);
                float3 diffuse = diff * _DiffuseColor.rgb;
                float3 reflectDir = reflect(-lightDir, i.triangleNormal);
                float spec = pow(max(dot(viewDir, reflectDir), 0.0), _Shininess);
                float3 specular = _SpecularColor.rgb * spec;
                float3 result = (ambient + diffuse + specular);
                float threshold = 0.4;
                float3 banding = floor(result / threshold);
                float3 finalIntensity = banding * threshold;
                return float4(finalIntensity, 1.0);
             }
            ENDCG
        }
    }
}


