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
        _MaxHeight("_MaxHeight [default 0.1]", Float) = 0.1
        _IsCosOn("Is Cos On [default off = 0]", Float) = 0
    }

	SubShader
	{
		Tags{ "RenderType" = "Opaque" }

		Pass
		{

			CGPROGRAM
// Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct v2f members worldPos)
//#pragma exclude_renderers d3d11 xbox360
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _CameraDepthNormalsTexture;
			float _DepthView = 1; // 1 - texture colors; 0 - depth (for debugging purpose)
			sampler2D _BgColor;
			sampler2D _BgDepth;
            sampler2D _BgShaderDepth;
            sampler2D _3dDepth;
			sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
			float4 _DynamicModelsColor;
			uniform float _Gradient;
            uniform float _MaxHeight;
            uniform float _IsCosOn; 

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
                float2 mirrorTexCoords = { i.uv.x,1 - i.uv.y };
                //float2 mirrorTexCoords = { i.uv.x, i.uv.y };

                float4 dynamicColor = tex2D(_MainTex, i.uv);
                float4 backgroundColor = tex2D(_BgColor, mirrorTexCoords);

                float dynamicDepth;
                float3 dynamicNormals;
                DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, i.scrPos.xy), dynamicDepth, dynamicNormals);
                //float backgroundDepth = tex2D(_BgDepth, mirrorTexCoords);   
                float backgroundDepth = tex2D(_BgDepth, mirrorTexCoords);

                // ---
                //float height = i.posInObjectCoords.z;
                //float height = 1 - ((1 - i.pos.z) / _MaxHeight);
                //float height = i.pos.z;
                //height = _MaxHeight;
                //dynamicDepth = 1 - height;

                //float height = 10 - (i.worldPos.z / _MaxHeight);
                //float height =  1 - (i.worldPos.y - _MaxHeight);
                float height = 1 - (i.worldPos.z - _MaxHeight);
                //dynamicDepth += height;

                //height = 1 - (i.worldPos.x + _MaxHeight);
                //height = i.posInObjectCoords.x + _MaxHeight;
                //height = i.worldPos.x + _MaxHeight;
                //dynamicDepth = height;
                dynamicDepth = tex2D(_3dDepth, mirrorTexCoords);
                //dynamicDepth = pow(dynamicDepth, 3);
                dynamicDepth = 1 - dynamicDepth;

                // 10 - max height
                //dynamicDepth = 1 - (height / _MaxHeight);
                //dynamicDepth = 1 - (height / _MaxHeight);
                //dynamicDepth = 1 - (_MaxHeight / height);

                //half4 depth = half4(Linear01Depth(tex2D(_CameraDepthTexture, i.uv).r);
                //half4 depth = half4(Linear01Depth(tex2D(_CameraDepthTexture, i.uv).r);
                //dynamicDepth = depth;

                //if (i.posInObjectCoords.y > 0.0)
                //{
                //    //discard;
                //    o.color = float4(0, 0, 0, 1);
                //    //o.color = return float4(0.0, 1.0, 0.0, 1.0);
                //    return o;
                //}
                // ---

                //http://answers.unity3d.com/questions/187455/shader-get-vertex-height-relative-to-model.html
                bool isBackgroundCloser = false;
                if (backgroundDepth < dynamicDepth)
                    isBackgroundCloser = true;

                //if (dynamicDepth > 0.1)
                //    isBackgroundCloser = true;

                //if (backgroundDepth > 0.95)
                //    isBackgroundCloser = false;

                //if (height <= 0.01)
                //    isBackgroundCloser = true;

                // ---
                //if (dynamicDepth > 0 && dynamicDepth < 1.0)
                //    isBackgroundCloser = false;
                //else
                //    isBackgroundCloser = true;
                // ---

                if (_DepthView > 0)
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
    //                envDepth = envDepth - ((1 - i.scrPos.y) * _MaxHeight);

    //                // ---
    //                if (_IsCosOn > 0)
    //                {
    //                    float angle = radians(90 - 57.8);
    //                    envDepth = envDepth + sin(angle) * i.scrPos.y * _MaxHeight;
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
    //                envDepth = envDepth - ((1 - i.scrPos.y) * _MaxHeight);

    //                // ---
    //                if (_IsCosOn > 0)
    //                {
    //                    float angle = radians(90 - 57.8);
    //                    envDepth = envDepth + sin(angle) * i.scrPos.y * _MaxHeight;
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











// http://answers.unity3d.com/questions/300106/shader-height-lines.html   
//Shader "Custom/Height"
//     {
//     Properties
//     {
//         _Color("Color", Color) = (0.5, 0.5, 0.5, 0.5)
//         _Step("Step", Float) = 50.0
//     }
//
//         SubShader
//     {
//         Pass
//     {
//         CGPROGRAM
//         // Upgrade NOTE: excluded shader from Xbox360; has structs without semantics (struct v2f members worldPos)
//#pragma exclude_renderers xbox360
//#pragma vertex vert
//#pragma fragment frag
//#include "UnityCG.cginc"
//
//         fixed4 _Color;
//     float _Step;
//
//     struct v2f
//     {
//         float4 pos : SV_POSITION;
//         float3 worldPos;
//     };
//
//     v2f vert(appdata_base v)
//     {
//         v2f o;
//         o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
//         o.worldPos = mul(_Object2World, v.vertex).xyz;
//         return o;
//     }
//
//     half4 frag(v2f i) : COLOR
//     {
//         return _Color * fixed4(
//             fixed3(
//                 1.0 - pow(
//                 (float)((int)i.worldPos.y % (int)_Step) / _Step,
//                     2)
//             ),
//             1);
//     }
//         ENDCG
//     }
//     }
//     }
