// Copyright (C) 2016 Filip Cyrus Bober

Shader "Custom/ApplyDepthGradient" 
{
    Properties
	{
        _MainTex("", 2D) = "white" {}		
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

			//sampler2D _CameraDepthNormalsTexture;
			float _DepthView = 1;

			sampler2D _bgColor;
			sampler2D _bgDepth;

            sampler2D _MainTex;
            sampler2D _actorsDepth;

            sampler2D _envColor;        // transparent view color
            sampler2D _envDepth;		// transparent view depth

            //sampler2D _2dBgColor;
            //sampler2D _2dBgDepth;

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv   : TEXCOORD0;
				float4 scrPos: TEXCOORD1;
			};

            v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.scrPos = ComputeScreenPos(o.pos);
				o.uv = v.texcoord.xy;

				return o;
			}			

			struct fragOut
			{
                half4 color : COLOR;
			};

			fragOut frag(v2f i)
			{

                fragOut o;
                float2 mirrorTexCoords = { i.uv.x,1 - i.uv.y };

                float4 envColor2d = tex2D(_bgColor, mirrorTexCoords);
                float envDepth2d = tex2D(_bgDepth, mirrorTexCoords);

                float4 dynamicColor = tex2D(_MainTex, i.uv);
                float4 transparentColor = tex2D(_envColor, mirrorTexCoords);
                float transparentDepth;

                float dynamicDepth;
                float3 dynamicNormals;

                //float4 bgColor2d = tex2D(_2dBgColor, mirrorTexCoords);
                //float bgDepth2d = tex2D(_2dBgDepth, mirrorTexCoords);

                dynamicDepth = tex2D(_actorsDepth, mirrorTexCoords);
                dynamicDepth = 1 - dynamicDepth;

                transparentDepth = tex2D(_envDepth, mirrorTexCoords);
                transparentDepth = 1 - transparentDepth;              

                bool transparentTop = false;
                bool backgroundTop = false;
                if (transparentDepth < dynamicDepth)
                    transparentTop = true; 

                //bool displayTransparent = false;
                //if (transparentDepth < 1.0)
                //{
                //    //if (envDepth2d < dynamicColorDepth)
                //    //    displayTransparent = true;
                //    displayTransparent = true;
                //}

                if (transparentTop)
                {
                    if (envDepth2d < transparentDepth)                    
                        backgroundTop = true;                    
                }
                else if (envDepth2d < dynamicDepth)
                    backgroundTop = true;

                if (_DepthView > 0)
                {        
                    // Debug Depth View mode
                    if (backgroundTop)
                        o.color = envDepth2d;
                    else if (transparentTop)
                        o.color = transparentDepth;
                    else
                        o.color = dynamicDepth;   
                }
                else
                {
                    //// Color mode
                    //if (backgroundTop)
                    //    o.color = envColor2d;
                    //else if (transparentTop)
                    //{
                    //    if (transparentColor.a < 1.0)
                    //        o.color = transparentColor * transparentColor.a + (envColor2d * (1 - transparentColor.a));
                    //    else
                    //        o.color = transparentColor;
                    //}
                    //else
                    //    o.color = dynamicColor;    

                    // Alpha
                    if (dynamicColor.a < 1.0)
                        dynamicColor = dynamicColor * dynamicColor.a + (envColor2d * (1 - dynamicColor.a));

                    if (backgroundTop)
                    {
                        o.color = envColor2d;
                    }
                    else if (transparentTop)
                    {
                        if (dynamicDepth < 1.0)
                            o.color = dynamicColor;
                        else
                            o.color = envColor2d;
                    }
                    else
                        o.color = dynamicColor;
                }

                // Transparency 
                //if ((backgroundTop) && transparentDepth < 1.0)
                if (transparentDepth < 1.0 && ((abs(transparentDepth - dynamicDepth) > 0.001 || backgroundTop)))
                {
                    // Alpha
                    if (transparentColor.a < 1.0)
                        transparentColor = transparentColor * transparentColor.a + (envColor2d * (1 - transparentColor.a));

                    o.color = transparentColor;
                }
                
                // Transparency
                //if (dynamicDepth < 1.0 && (transparentTop || backgroundTop))
                //{
                //    o.color = dynamicColor;
                //    o.color.r = 0.4 * envColor2d.r + 0.6 * 1.0; //dynamicColor.r;
                //    o.color.g = 0.4 * envColor2d.g + 0.6 * 0.0; //dynamicColor.g;
                //    o.color.b = 0.4 * envColor2d.b + 0.6 * 0.0; //dynamicColor.b;
                //}

                return o;
			}

			ENDCG
		}
    }
}
