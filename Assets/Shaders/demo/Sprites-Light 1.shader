// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Sprites/Light1"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _ShadowTex ("Texture", 2D) = "white" {}
		[PerRendererData] _Color ("Color", Color) = (1,1,1,1)
		[PerRendererData] _LightPosition("LightPosition", Vector) = (0,0,1,0)
		[PerRendererData] _ShadowMapParams("ShadowMapParams", Vector) = (0,0,0,0)
		[PerRendererData] _Params2("Params2", Vector) = (0,0,0,0)
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Geometry" 
			"IgnoreProjector"="True" 
			"RenderType"="Opaque" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "ShadowMap1D.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color   : COLOR;
				float2 texcoord  : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				float2 texcoord  : TEXCOORD0;
				float4 modelPos : TEXCOORD1;
				float4 worldPos : TEXCOORD2;
				float4 color   : COLOR;
			};
			
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.modelPos = IN.vertex;
				OUT.worldPos = mul(unity_ObjectToWorld, IN.vertex);
				OUT.color = IN.color;
				OUT.texcoord = IN.texcoord;
				return OUT;
			}

			sampler2D 	_ShadowTex;
			float4 		_LightPosition;
			float4 		_ShadowMapParams;  // this is the row to write to in the shadow map. x is used to write, y to read.
			float4 		_Params2;
			fixed4 		_Color;
			sampler2D  		_MainTex;
			


			fixed4 SampleSpriteTexture(float2 uv)
			{

				fixed4 color = tex2D(_MainTex, uv);

				#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				if (_AlphaSplitEnabled)
					color.a = tex2D(_AlphaTex, uv).r;

				#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
				c.rgb *= c.a;

				float2 polar = ToPolar(IN.worldPos.xy,_LightPosition.xy);

				float shadow = SampleShadowTexturePCF(_ShadowTex,polar,_ShadowMapParams.x);
				if (shadow < 0.5f) {
					clip( -1.0 );
					return c;
				}


				return c;
			}
		ENDCG
		}
	}
}
