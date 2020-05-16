using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSprite : MonoBehaviour {
	[SerializeField] SpriteRenderer[] spriteRenderers;
	Sprite mPattern;
	Material material;

	public Sprite pattern{
		get => mPattern;
		set {
			mPattern = value;
			if (Application.isPlaying && (material == null)) {
				// Create Material Instace
				material = spriteRenderers[0].material;
				for (int f = 1; f < spriteRenderers.Length; f++) {
					spriteRenderers[f].material = material;
				}
			} else {
				// Use Assets Material
				material = spriteRenderers[0].sharedMaterial;
			}

			if (pattern != null) {
				// Apply Pattern
				material.SetTexture("_PatternTex", pattern.texture);
				Vector2 _size = new Vector2(pattern.texture.width, pattern.texture.height);
				Rect _rect = pattern.rect;
				material.SetVector(
					"_PatternRect",
					new Vector4(
						(_rect.x + 0.5f) / _size.x,
						(_rect.y + 0.5f) / _size.y,
						_rect.width / _size.x,
						_rect.height / _size.y
					)
				);
			} else {
				// Clear Pattern
				material.SetTexture("_PatternTex", null);
				material.SetVector("_PatternRect", Vector4.zero);
			}
		}
	}
}
