using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterView : MonoBehaviour{
	[SerializeField] Transform spriteTop;
	[SerializeField] SpriteRenderer[] frontSprites, backSprites;
	[SerializeField] Transform colliderTop;

	#region Angle
	static Vector3 rightScale = new Vector3(-1, 1, 1);
	static Vector3 leftScale = new Vector3(1, 1, 1);
	/// <summary> 前方角 (徑度) </summary>
	public float forwardAngle {
		set {
			bool _isForward = (value <= 0) || (value == Mathf.PI); // Angle <0 or =180
			bool _isRight = (Mathf.Abs(value) < Mathf.PI*0.5f); // Angle is -90~90

			// Set Sprite Active And Scale
			foreach (SpriteRenderer _renderer in frontSprites) {
				_renderer.gameObject.SetActive(_isForward);
			}
			foreach (SpriteRenderer _renderer in backSprites) {
				_renderer.gameObject.SetActive(!_isForward);
			}
			spriteTop.transform.localScale = (_isRight ? rightScale : leftScale);

			// Rota Collider
			colliderTop.localRotation = Quaternion.Euler(-90f, 0f, 90 - value*Mathf.Rad2Deg);
		}
	}
	#endregion

	#region pattern
	[SerializeField] PatternSprite patternSprite;
	/// <summary> 色板 </summary>
	public Sprite pattern {
		set => patternSprite.pattern = value;
		get => patternSprite.pattern;
	}
	#endregion

	#region Animation
	[SerializeField] Animator animator;
	public void PlayAnimation(string p_name) {
		animator.Play(p_name);
	}
	#endregion
}
