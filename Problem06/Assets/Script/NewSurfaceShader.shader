Shader "Unlit/YellowShader"
{
    Properties
    {
        _DiffuseColor("DiffuseColor", Color) = (1,1,1,1)
        _LightDirection("LightDirection", Vector) = (1,-1,-1,0)
        _SpecularColor("SpecularColor", Color) = (1,1,1,1)
        _Shininess("Shininess", Range(1, 140)) = 32
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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
            };

            float4 _DiffuseColor;
            float4 _LightDirection;
            float _Shininess;
            float4 _SpecularColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //fixed4 col = float4(1.0f,1.0f,0.0,1.0f);
                float ambientStrength = 0.2;
                float3 ambient = ambientStrength * float3(1.0f,0.0f,0.0f);//주변광
                float3 viewDir = normalize(_WorldSpaceCameraPos - mul(unity_ObjectToWorld, i.vertex));
                float lightDir = normalize(_LightDirection);
                float diff = max(dot(i.normal,lightDir),0);
                float3 diffuse = diff * float3(1.0f,1.0f,1.0f);//확산광
                float3 reflectDir = reflect(-lightDir,i.normal); //반사광
                float spec = pow(max(dot(viewDir, reflectDir), 0.0), _Shininess);
                float3 specular = float(0.5) * spec;
                float3 result = (ambient + diffuse + specular);
                float threshold = 0.4;
				float3 banding = floor(result / threshold);
				float3 finalIntensity = banding * threshold;
				
				float4 result2 = float4(finalIntensity.x, finalIntensity.y, finalIntensity.z, 1.0);
                return fixed4(result2);
                
             }
            ENDCG
        }
    }
}

