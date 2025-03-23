Shader "Custom/Scroll Texture"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Animate("Animate scroll", Vector) = (0,0,0,0)
		_Color("Color", Color) = (1,1,1,1)

	}
	SubShader
	{
		Tags { "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv: TEXCOORD0;
				float4 vertex: SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Animate;
			float4 _Color;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv += _Animate.xy * _Time.yy;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float2 uvs = i.uv;

				fixed4 texColor = tex2D(_MainTex, uvs);
				return texColor * _Color;
			}
			
			ENDCG
		}
	}
}