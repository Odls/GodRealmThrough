using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterView : MonoBehaviour{
	[SerializeField] PatternSprite patternSprite;
	[SerializeField] Transform spriteTop;
	[SerializeField] SpriteRenderer frontSprite, backSprite;
	[SerializeField] Sprite[] patterns;
	[SerializeField] Transform colliderTop;


	void Awake() {
		patternId = Random.Range(0, patterns.Length - 1);
	}

	static Vector3 rightScale = new Vector3(-1, 1, 1);
	static Vector3 leftScale = new Vector3(1, 1, 1);
	/// <summary> 前方角度 (徑度) </summary>
	public float forwardAngle {
		set {
			bool _isForward = (value <= 0) || (value == Mathf.PI); // Angle <0 or =180
			bool _isRight = (Mathf.Abs(value) < Mathf.PI*0.5f); // Angle is -90~90

			// Set Sprite Active And Scale
			frontSprite?.gameObject.SetActive(_isForward);
			backSprite?.gameObject.SetActive(!_isForward);
			patternSprite.transform.localScale = (_isRight ? rightScale : leftScale);

			// Rota Collider
			colliderTop.localRotation = Quaternion.Euler(-90f, 0f, 90 - value*Mathf.Rad2Deg);
		}
	}

	int mPatternId = -1;
	/// <summary> 色板 ID </summary>
	public int patternId {
		set {
			if ((value != mPatternId) && (value >= 0) && (value <= (patterns.Length - 1))) {
				mPatternId = value;
				patternSprite.ApplyPattern(patterns[mPatternId]);
			}
		}
	}
	
	[SerializeField] Animator animator;
	public void PlayAnimation(string p_name) {
		animator.Play(p_name);
	}
}
