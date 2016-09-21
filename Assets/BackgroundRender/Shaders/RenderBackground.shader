// Copyright (C) 2016 Filip Cyrus Bober

Shader "Custom/RenderBackground" 
{
    Properties
	{
        // Set from script
        _Color("Color camera view (RGB)", 2D) = "white" {}
		_Depth("Depth camera view (RGB)", 2D) = "white" {}
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

			sampler2D _Color;
			sampler2D _Depth;

			struct v2f
			{
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct fragOut
			{
				half4 color : COLOR;
				float depth : DEPTH;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				o.position = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord;

				return o;
			}

			fragOut frag(in v2f i)
			{
				fragOut o;
				o.color = tex2D(_Color, i.uv);
				o.depth = tex2D(_Depth, i.uv);

				return o;
			}

			ENDCG
		}
    }
}