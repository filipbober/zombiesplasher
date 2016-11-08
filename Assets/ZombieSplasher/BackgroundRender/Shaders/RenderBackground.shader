// Copyright (C) 2016 Filip Cyrus Bober

Shader "Custom/RenderBackground" 
{
    Properties
	{
        // Set from script
        _Color("Color camera view (RGB)", 2D) = "white" {}
		_Depth("Depth camera view (RGB)", 2D) = "white" {}

        _IsOn("IsOn [default On = 1]", Float) = 1
    }

    SubShader
	{
		Pass
		{
			CGPROGRAM
			//#pragma target 4.0
		    #pragma vertex vert
		    #pragma fragment frag
		    #include "UnityCG.cginc"

            sampler2D _CameraDepthNormalsTexture;
        sampler2D _CameraDepthTexture;

			sampler2D _Color;
			sampler2D _Depth;

            uniform float _IsOn;

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
                float4 scrPos: TEXCOORD1;
			};

			struct fragOut
			{
				half4 color : COLOR;
				float depth : DEPTH;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.scrPos = ComputeScreenPos(o.pos);
				o.uv = v.texcoord.xy;

				return o;
			}

			fragOut frag(in v2f i)
			{
                //fragOut oo;
                //oo.depth = 1;
                //oo.color = float4(oo.depth, oo.depth, oo.depth, 1.0);
                //return oo;

                half4 depth = half4(Linear01Depth(tex2D(_CameraDepthTexture, i.uv).r);                
                return depth;
                // ---

                if (_IsOn > 0)
                {
                    fragOut o;
                    o.color = tex2D(_Color, i.uv);
                    o.depth = tex2D(_Depth, i.uv);
                    
                    return o;
                }
                else
                {
                    fragOut o;

                    float3 normalValues;
                    float depthValue;
                    float2 mirrorTexCoords = { i.uv.x,1 - i.uv.y };
                    //float2 mirrorTexCoords = { i.uv.x, i.uv.y };

                    DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, i.scrPos.xy), depthValue, normalValues);

                    o.depth = depthValue;
                    //o.color = tex2D(_Color, i.uv);
                    o.color = tex2D(_Color, mirrorTexCoords);
                    o.color = float4(1.0, 0, 0, 1);
                    return o;
                }
			}

			ENDCG
		}
    }
}