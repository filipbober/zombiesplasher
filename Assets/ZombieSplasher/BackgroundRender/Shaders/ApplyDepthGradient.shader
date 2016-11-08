// Copyright (C) 2016 Filip Cyrus Bober

Shader "Custom/ApplyDepthGradient" 
{
    Properties
	{
        _MainTex("", 2D) = "white" {}		
        
        // Set from script
		_Rgb("Base (RGB)", 2D) = "white" {}
		_Depth("Depth (RGB)", 2D) = "white" {} 

		_Gradient("3D Gradient [default 0]", Float) = 0
        _EnvGradient("Env Gradient [default 0]", Float) = 0
        _IsCosOn("Is Cos On [default off = 0]", Float) = 0
    }

	SubShader
	{
		Tags{ "RenderType" = "Opaque" }

		Pass
		{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _CameraDepthNormalsTexture;
			float _DepthView = 1; // 1 - texture colors; 0 - depth (for debugging purpose)
			sampler2D _BgColor;
			sampler2D _BgDepth;
            sampler2D _BgShaderDepth;
			sampler2D _MainTex;
			float4 _DynamicModelsColor;
			uniform float _Gradient;
            uniform float _EnvGradient;
            uniform float _IsCosOn; 

			struct v2f
			{
				float4 pos : SV_POSITION;
				half2 uv   : TEXCOORD0;
				float4 scrPos: TEXCOORD1;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.scrPos = ComputeScreenPos(o.pos);
				//o.scrPos.y = 1 - o.scrPos.y;
				o.uv = v.texcoord.xy;

				return o;
			}			

			struct fragOut
			{
				half4 color : SV_Target;
			};

			fragOut frag(v2f i) //: COLOR
			{

                fragOut o;
                float2 mirrorTexCoords = { i.uv.x,1 - i.uv.y };

                float4 dynamicColor = tex2D(_MainTex, i.uv);
                float4 backgroundColor = tex2D(_BgColor, mirrorTexCoords);

                float dynamicDepth;
                float3 dynamicNormals;
                DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, i.scrPos.xy), dynamicDepth, dynamicNormals);
                float backgroundDepth = tex2D(_BgDepth, mirrorTexCoords);

                bool isBackgroundCloser = false;
                if (backgroundDepth < dynamicDepth)
                    isBackgroundCloser = true;


                if (_DepthView == 1)
                {
                    if (isBackgroundCloser)
                        o.color = backgroundDepth;
                    else
                        o.color = dynamicDepth;
                }
                else
                {
                    if (isBackgroundCloser)
                        o.color = backgroundColor;
                    else
                        o.color = dynamicColor;
                }

                return o;

				//fragOut o;

				//float3 normalValues;
				//float depthValue;

    //            float3 shaderDepthValues;

				//DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, i.scrPos.xy), depthValue, normalValues);    
	
				////depthValue = depthValue - ((1- i.scrPos.y) * _Gradient);		// -0.18
				//if (_DepthView == 1)
				//{
    //                float2 mirrorTexCoords = { i.uv.x,1 - i.uv.y };

    //                float envDepth = tex2D(_BgDepth, mirrorTexCoords);
    //                //float envDepth = tex2D(_BgShaderDepth, mirrorTexCoords);
    //                float dynamicDepth = depthValue;

    //                dynamicDepth = depthValue - ((1 - i.scrPos.y) * _Gradient);
    //                envDepth = envDepth - ((1 - i.scrPos.y) * _EnvGradient);

    //                // ---
    //                if (_IsCosOn > 0)
    //                {
    //                    float angle = radians(90 - 57.8);
    //                    envDepth = envDepth + sin(angle) * i.scrPos.y * _EnvGradient;
    //                }
    //                // ---

    //                bool isObjectOcculedByBackground = (envDepth < dynamicDepth);

    //                //float4 dynamicObjectColor = dynamicDepth;
    //                //float4 backgroundColor = envDepth;

    //                if (isObjectOcculedByBackground)
    //                {
    //                    float4 color = float4(envDepth, envDepth, envDepth, 1.0);

    //                    //o.color.x = backgroundColor + _DynamicModelsColor.z;
    //                    o.color = color;
    //                }
    //                else
    //                {
    //                    //float4 color = float4(depthValue, depthValue, depthValue, 1.0);
    //                    float4 color = float4(depthValue, depthValue, depthValue, 1.0);

    //                    //color.x = (_DynamicModelsColor.x == 0) ? depthValue : _DynamicModelsColor.x;
    //                    //color.y = (_DynamicModelsColor.y == 0) ? depthValue : _DynamicModelsColor.y;
    //                    //color.z = (_DynamicModelsColor.z == 0) ? depthValue : _DynamicModelsColor.z;

    //                    o.color = color;
    //                }

    //                return o;
				//}
				//else
				//{		
    //                float2 mirrorTexCoords = { i.uv.x,1 - i.uv.y };

    //                float envDepth = tex2D(_BgDepth, mirrorTexCoords);
    //                //float envDepth = tex2D(_BgShaderDepth, mirrorTexCoords);
    //                float dynamicDepth = depthValue;

    //                dynamicDepth = depthValue - ((1 - i.scrPos.y) * _Gradient);
    //                envDepth = envDepth - ((1 - i.scrPos.y) * _EnvGradient);

    //                // ---
    //                if (_IsCosOn > 0)
    //                {
    //                    float angle = radians(90 - 57.8);
    //                    envDepth = envDepth + sin(angle) * i.scrPos.y * _EnvGradient;
    //                }
    //                // ---

    //                bool isObjectOcculedByBackground = (envDepth < dynamicDepth);

    //                float4 dynamicObjectColor = tex2D(_MainTex, i.uv);
    //                float4 backgroundColor = tex2D(_BgColor, mirrorTexCoords);

    //                if (isObjectOcculedByBackground)
    //                {
    //                    o.color = backgroundColor;
    //                }
    //                else
    //                {

    //                    o.color = dynamicObjectColor;
    //                }

    //                return o;					
				//}
			}

			ENDCG
		}
    }
    
	FallBack "Diffuse"
}