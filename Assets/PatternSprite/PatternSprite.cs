using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSprite : MonoBehaviour
{
	[SerializeField] SpriteRenderer[] spriteRenderers;
	[SerializeField] Sprite pattern;
	Material material;

    void Awake(){
		ApplyPattern();
	} 

	public void ApplyPattern(Sprite p_pattern) {
		pattern = p_pattern;
		ApplyPattern();
	}

	[ContextMenu("Apply Pattern")]
    public void ApplyPattern(){
		if (Application.isPlaying && (material==null)) {
			material = spriteRenderers[0].material;
			for(int f=1; f< spriteRenderers.Length; f++) {
				spriteRenderers[f].material = material;
			}
		} else {
			material = spriteRenderers[0].sharedMaterial;
		}
		
		if (pattern != null) {
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
			material.SetTexture("_PatternTex", null);
			material.SetVector("_PatternRect", Vector4.zero);
		}
	}
}
