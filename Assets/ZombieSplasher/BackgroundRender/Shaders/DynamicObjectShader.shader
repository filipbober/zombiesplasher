Shader "Custom/DynamicObjectShader" {
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 200

            Pass {
            CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            //#pragma surface surf Standard fullforwardshadows

            // Use shader model 3.0 target, to get nicer looking lighting
            //#pragma target 3.0

            #pragma vertex vert
            #pragma fragment frag
    #include "UnityCG.cginc"

            sampler2D _MainTex;

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv   : TEXCOORD0;
                float4 scrPos: TEXCOORD1;
                float4 posInObjectCoords : TEXCOORD2;

                float3 worldPos : TEXCOORD3;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.scrPos = ComputeScreenPos(o.pos);
                //o.scrPos.y = 1 - o.scrPos.y;
                o.uv = v.texcoord.xy;
                o.posInObjectCoords = v.vertex;

                // Camera world pos...
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                //o.worldPos = mul(_Object2World, float4(0, 0, 0, 1)).xyz;
                //float vz = mul(UNITY_MATRIX_MV, v.vertex).z;
                //float depth = _offset + abs((1 - clamp(-vz / _farDepth, 0, 2)) * _depthScale);

                //needs float _offset, _farDepth, _depthScale


                //o.worldPos.x = normalize(-v.vertex).z;

                return o;
            }

            struct fragOut
            {
                //half4 color : SV_Target;
                half4 color : COLOR;
                //float depth : DEPTH;
            };

            fragOut frag(v2f i) //: COLOR
            {

                fragOut o;
                //float2 mirrorTexCoords = { i.uv.x,1 - i.uv.y };

                // wysokosc od -1 do 7

                //float depthValue = 1 - (i.worldPos.y / 10.0);
                float depthValue = (i.worldPos.y / 8.0) + 1/8.0;
                //depthValue = pow(depthValue, 2);
                //if (i.worldPos.y < 1)
                //    depthValue = 1;

                //depthValue = 0.99;
                //if (i.worldPos.y < 0.01)
                //    depthValue = 0;


                //if (i.worldPos.y < -1 || i.worldPos.y > 7)
                //    depthValue = 0.0f;

                

                o.color = float4(depthValue, depthValue, depthValue, 1);

                //o.color = float4(0.5, 0.5, 0.5, 1);
                return o;
            }


            //struct Input {
            //	float2 uv_MainTex;
            //};

            //half _Glossiness;
            //half _Metallic;
            //fixed4 _Color;

            //void surf (Input IN, inout SurfaceOutputStandard o) {
            //	// Albedo comes from a texture tinted by color
            //	fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
      //          c.r = 0;
      //          c.b = 0;
            //	o.Albedo = c.rgb;
            //	// Metallic and smoothness come from slider variables
            //	o.Metallic = _Metallic;
            //	o.Smoothness = _Glossiness;
            //	o.Alpha = c.a;
            //}

        ENDCG
        }
        }
            FallBack "Diffuse"
}
