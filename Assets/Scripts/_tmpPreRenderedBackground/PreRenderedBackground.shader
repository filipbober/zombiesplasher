

Shader "Custom/PreRenderedBackground" {
    Properties{
        _RGB("Base (RGB)", 2D) = "white" {}
    _DEPTH("Depth (RGB)", 2D) = "white" {}
    }
        SubShader{
        Pass{
        CGPROGRAM
//#pragma target 4.0
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

        sampler2D _RGB;
    sampler2D _DEPTH;

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
        o.color = tex2D(_RGB, i.uv);
		o.depth = tex2D(_DEPTH, i.uv);

		// Inverted ---

		//float2 mirrorTexCoords = {i.uv.x,1-i.uv.y };
		//o.color = tex2D(_RGB, mirrorTexCoords);
		//o.depth = tex2D(_DEPTH, mirrorTexCoords);

		// ---

        return o;
    }
    ENDCG
    }
    }
}