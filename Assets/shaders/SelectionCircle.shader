Shader "Custom/SelectionCircle"
{
	Properties
	{
		_EdgeColor ("Edge Color", Color) = (1, 1, 1, 1)
		_FadeColor ("Fade Color", color) = (1, 1, 1, 1)
		_Thickness ("Thickness", Range(0, 0.5)) = 0.2
		_FadePower ("Fade Power", Range(0, 10)) = 1
	}
	SubShader
	{
		Tags {
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"
			
			struct VertexInput {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
			};

			uniform float4 _EdgeColor;
			uniform float4 _FadeColor;
			uniform float _Thickness;
			uniform float _FadePower;
			
			v2f vert (VertexInput v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target {
				float2 centeredUV = i.uv - float2(0.5, 0.5);
				float dist = sqrt(dot(centeredUV, centeredUV));
				float fade = pow((dist - 0.5 + _Thickness) / _Thickness, _FadePower);
				half4 col = _EdgeColor * fade + _FadeColor * (1 - fade);
				col.a *= step(dist, 0.5);
				col.a *= fade;
				col.a = clamp(col.a, 0, 1);

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}

			ENDCG
		}
	}
}
