Shader "Sprites/Pattern"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_PatternTex("Pattern Texture", 2D) = "white" {}
		_PatternRect("Pattern Rect", Vector) = (0,0,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex SpriteVert
			#pragma fragment PatternSpriteFrag
			#pragma target 2.0
			#pragma multi_compile_instancing
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnitySprites.cginc"
			
			sampler2D _PatternTex;
			fixed4 _PatternRect;

			fixed4 PatternSpriteFrag(v2f IN) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, IN.texcoord);
				fixed4 p = tex2D(
					_PatternTex,
					float2(
						_PatternRect.x + c.r * _PatternRect.z,
						_PatternRect.y + c.g * _PatternRect.w
					)
				);
				
				c.rgb = p.rgb;

			#if ETC1_EXTERNAL_ALPHA
				fixed4 alpha = tex2D(_AlphaTex, IN.texcoord);
				c.a = lerp(c.a, alpha.r, _EnableExternalAlpha);
			#endif

				c = c * IN.color;
				c.rgb *= c.a;
				return c;

				//return tex2D(
				//	_PatternTex,
				//	float2(
				//		_PatternRect.x + IN.texcoord.x * _PatternRect.z,
				//		_PatternRect.y + IN.texcoord.y * _PatternRect.w
				//	)
				//);
			}
		ENDCG
		}
	}
}
