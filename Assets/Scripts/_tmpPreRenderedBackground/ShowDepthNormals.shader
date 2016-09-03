Shader "Custom/ShowDepthNormals" {
    Properties{
        _MainTex("", 2D) = "white" {}

	_RGB("Base (RGB)", 2D) = "white" {}
	_DEPTH("Depth (RGB)", 2D) = "white" {}
	
    _HighlightDirection("Character color", Vector) = (1, 0,0)
        _DepthThreshold("Depth Threshold[0-1, def 0.05]", Float) = 0.05
        _DepthMult("Depth mult.[default 1]", Float) = 1
        _Gradient("Gradient [default 0]", Float) = 0
    }


		// Scr.pos vs uv.pos might cause texture inversion

		//http://www.alanzucconi.com/2015/07/08/screen-shaders-and-postprocessing-effects-in-unity3d/
        SubShader{
        Tags{ "RenderType" = "Opaque" }

        Pass{

		//Cull Front // first pass renders only back faces 
		//		   // (the "inside")
		//ZWrite Off // don't write to depth buffer 
		//		   // in order not to occlude other objects

        CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

        sampler2D _CameraDepthNormalsTexture;
    float _StartingTime;
    float _showNormalColors = 1; //when this is 1, show normal values as colors. when 0, show depth values as colors.
	//sampler2D _LastCameraDepthNormalsTexture;

	sampler2D _CameraDepthTexture;
	sampler2D _RGB;
	sampler2D _DEPTH;

	//struct appdata
	//{
	//	float4 vertex   : POSITION;  // The vertex position in model space.
	//	float3 normal   : NORMAL;    // The vertex normal in model space.
	//	float4 texcoord : TEXCOORD0; // The first UV coordinate.

	//	float4 color : COLOR;     // Per-vertex color
	//};

    struct v2f
    {
        float4 pos : SV_POSITION;
		half2 uv   : TEXCOORD0;
        float4 scrPos: TEXCOORD1;

		float4 color : COLOR;
    };

    //Our Vertex Shader
    v2f vert(appdata_base v)
	//v2f vert(appdata v)
    {
        v2f o;
        o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
        o.scrPos = ComputeScreenPos(o.pos);
        //o.scrPos.y = 1 - o.scrPos.y;
		o.uv = v.texcoord.xy;
		//o.color.w = 1.0;
		//o.color = v.color;
		//o.uv = v.texcoord;

		//o.color = v.color;
		//o.color.xyz = v.normal * 0.5 + 0.5;
		//o.color.w = 1.0;

        return o;
    }

    sampler2D _MainTex;
    float4 _HighlightDirection;
    uniform float _DepthThreshold;
    uniform float _DepthMult;
    uniform float _Gradient;
	fixed4 _Color;

	struct fragOut
	{
		//float4 color : COLOR;
		//float depth : DEPTH;
		half4 color : SV_Target;
		float depth : SV_Depth;
	};

    //Our Fragment Shader
    //half4 frag(v2f i) : COLOR{
	fragOut frag(v2f i) : COLOR{

		fragOut o;
		//o.color = tex2D(_RGB, i.uv);
		//o.depth = tex2D(_DEPTH, i.uv);

		//return tex2D(_RGB, i.uv);
		//return tex2D(_DEPTH, i.uv);


		//---
		float depthValue3 = Linear01Depth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos)).r);
	half4 depth3;

	depth3.r = depthValue3;
	depth3.g = depthValue3;
	depth3.b = depthValue3;

	depth3.a = 1;
	//return depth3;
		//---



        float3 normalValues;
    float depthValue;
    //extract depth value and normal values

	//i.scrPos.x = i.scrPos.x;
	//i.scrPos.y = i.scrPos.x;



    DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, i.scrPos.xy), depthValue, normalValues);
	//DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, scrCenter), depthValue, normalValues);
	



	depthValue = depthValue - ((1- i.scrPos.y) * _Gradient);
	//DecodeDepthNormal(tex2D(_LastCameraDepthNormalsTexture, i.scrPos.xy), depthValue, normalValues);	
    if (_showNormalColors == 1)
    {

		float4 c = tex2D(_MainTex, i.uv);
		//float4 c = tex2D(_RGB, i.uv);			// For some reason inverted!!!
		//float4 c = tex2D(_DEPTH, i.uv);		// For some reason inverted!!!
		o.color = c;
		//o.depth = depthValue;
		o.depth = c;






		float2 mirrorTexCoords = { i.uv.x,1 - i.uv.y };

		float envDepth = tex2D(_DEPTH, mirrorTexCoords);
		float dynamicDepth = depthValue;

		if (envDepth < dynamicDepth)
		{
			c = tex2D(_RGB, mirrorTexCoords);
		}
		else
		{
			c = tex2D(_MainTex, i.uv);
		}

		//c = tex2D(_DEPTH, mirrorTexCoords);

		//c = tex2D(_MainTex, i.uv);
		//c = _Color;
		//c.r = i.color;



		o.color = c;
		//o.depth = 0.0;
		//o.color = i.color;


		//return c;
		return o;

        //float4 normalColor = float4(normalValues, 1);
        //return normalColor;
    }
	else
	{
		if (depthValue < 1.0)
		{
			//float4 depth = float4(depthValue, depthValue, depthValue, 1.0);

			//float4 c = tex2D(_MainTex, i.uv);
			//float4 c = tex2D(_RGB, i.uv);

			//float4 depth = float4(depthValue, depthValue, depthValue, c.a);
			float4 depth = float4(depthValue, depthValue, depthValue, 1.0);

			depth.x = (_HighlightDirection.x == 0) ? depthValue : _HighlightDirection.x;
			depth.y = (_HighlightDirection.y == 0) ? depthValue : _HighlightDirection.y;
			depth.z = (_HighlightDirection.z == 0) ? depthValue : _HighlightDirection.z;

			//float4 depth = float4(depthValue2, depthValue2, depthValue2, 1.0);
			

			float2 mirrorTexCoords = { i.uv.x,1 - i.uv.y };
			float4 c = tex2D(_DEPTH, mirrorTexCoords);
			if (depthValue >= tex2D(_DEPTH, mirrorTexCoords).x)
			{
				//return c;

				o.color = c;
				//o.depth = depthValue;
				o.depth = c;
				return o;
			}
			else
			{
				 
				//return depth;
				o.color = depth;
				o.depth = depthValue;
				return o;
			}

			//return depth;
			o.color = depth;
			//o.depth = depthValue;
			o.depth = c;
			return o;
		}
		else
		{
			//float4 c = tex2D(_MainTex, i.uv);


			// ???? - why is this texture inverted???
			//float4 c = tex2D(_DEPTH, i.uv);
			float2 mirrorTexCoords = { i.uv.x,1 - i.uv.y };
			float4 c = tex2D(_DEPTH, mirrorTexCoords);

			//return tex2D(_DEPTH, i.uv);					// <--- inverted for unknown reason!!!

			//float2 mirrorTexCoords = {i.uv.x,1 - i.uv.y };
			//float4 c = tex2D(_DEPTH, mirrorTexCoords);
			//return c;

			o.color = c;
			//o.depth = depthValue;
			o.depth = c;
			return o;
		}
	}
  //  else
  //  {
  //      //float4 depth = float4(depthValue);    // no longer valid

  //      //float4 depth = float4(depthValue, 1, 1, 1);
  //      //float4 depth = float4(depthValue, 0, 0, 0);
  //      ////or
  //      //float4 depth = float4(depthValue, depthValue, depthValue, depthValue);
  //      //depthValue *= depthValue;
  //      depthValue = depthValue - (i.scrPos.y * _Gradient);

  //      //if (depthValue > 0.05)
  //      if (_DepthMult == 1.0)
  //      {
  //          if (depthValue > _DepthThreshold)
  //          {
  //              depthValue = 1;
  //          }
  //          else
  //          {
  //              depthValue = 0;
  //          }
  //      }
  //      else
  //      {

  //          depthValue *= _DepthMult;
  //      }


  //      //i.scrPos.y;
  //      //depthValue /= i.scrPos.y;

  //      /*depthValue = depthValue - (i.scrPos.y * _Gradient);*/







		////float depthValue2 = half4(tex2D(_CameraDepthTexture, i.uv));
		////depthValue2 = LinearEyeDepth(depthValue2);

		//if (depthValue < 1.0)
		//{
		//	//float4 depth = float4(depthValue, depthValue, depthValue, 1.0);

		//	float4 c = tex2D(_MainTex, i.uv);
		//	float4 depth = float4(depthValue, depthValue, depthValue, c.a);
		//	//float4 depth = float4(depthValue2, depthValue2, depthValue2, 1.0);

		//	return depth;			
		//}
		//else
		//{
		//	float4 c = tex2D(_MainTex, i.uv);
		//	return c;
		//}
	
		//
  //  }



    }
        ENDCG
    }
    }
        FallBack "Diffuse"
}







//
//sampler2D _CameraDepthTexture;
//v2f vert(appdata_img v)
//{
//	v2f o;
//	o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
//	o.uv = v.texcoord.xy;
//	return o;
//}
//
//half4 fragThin(v2f i) : COLOR
//{
//	half4 depth = half4(Linear01Depth(tex2D(_CameraDepthTexture, i.uv).r);
//
//	return depth;
//}
