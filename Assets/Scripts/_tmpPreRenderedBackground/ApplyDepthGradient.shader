Shader "Custom/ApplyDepthGradient" {
    Properties{
        _MainTex("", 2D) = "white" {}
		_RGB("Base (RGB)", 2D) = "white" {}
		_DEPTH("Depth (RGB)", 2D) = "white" {}    

		// Debug purposes only
        _Gradient("Gradient [default 0]", Float) = 0
		_DynamicModelsColor("Dynamic models color", Vector) = (1, 0,0)
    }

        SubShader{
        Tags{ "RenderType" = "Opaque" }

        Pass{

        CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

    sampler2D _CameraDepthNormalsTexture;

    float _DepthView = 1; // 1 - texture colors; 0 - depth (for debugging purpose)

	sampler2D _RGB;
	sampler2D _DEPTH;

    struct v2f
    {
        float4 pos : SV_POSITION;
		half2 uv   : TEXCOORD0;
        float4 scrPos: TEXCOORD1;

		float4 color : COLOR;
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

    sampler2D _MainTex;
    float4 _DynamicModelsColor;
    uniform float _Gradient;
	fixed4 _Color;

	struct fragOut
	{
		half4 color : SV_Target;
	};

	fragOut frag(v2f i) : COLOR
    {

	    fragOut o;

        float3 normalValues;
        float depthValue;

        DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, i.scrPos.xy), depthValue, normalValues);    
	
	    depthValue = depthValue - ((1- i.scrPos.y) * _Gradient);		// -0.18

	    if (_DepthView == 1)
	    {
		    float2 mirrorTexCoords = { i.uv.x,1 - i.uv.y };

		    float envDepth = tex2D(_DEPTH, mirrorTexCoords);
		    float dynamicDepth = depthValue;

            bool isObjectOcculedByBackground = (envDepth < dynamicDepth);
            if (isObjectOcculedByBackground)
		    {			
			    o.color = tex2D(_RGB, mirrorTexCoords);		
			    return o;
		    }
		    else
		    {
			    o.color = tex2D(_MainTex, i.uv);				
		    }

		    return o;
        }
	    else
	    {		
			float2 mirrorTexCoords = { i.uv.x,1 - i.uv.y };

			if (depthValue >= tex2D(_DEPTH, mirrorTexCoords).x)
			{
                o.color = tex2D(_DEPTH, mirrorTexCoords);
			}
			else
			{				
                float4 depth = float4(depthValue, depthValue, depthValue, 1.0);

                depth.x = (_DynamicModelsColor.x == 0) ? depthValue : _DynamicModelsColor.x;
                depth.y = (_DynamicModelsColor.y == 0) ? depthValue : _DynamicModelsColor.y;
                depth.z = (_DynamicModelsColor.z == 0) ? depthValue : _DynamicModelsColor.z;

				o.color = depth;
			}

			return o;
	    }
    }
        ENDCG
    }
    }
        FallBack "Diffuse"
}