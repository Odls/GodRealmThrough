using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour{
	[SerializeField] PatternSprite patternSprite;
	[SerializeField] SpriteRenderer frontSprite, backSprite;
	[SerializeField] Sprite[] patterns;


	void Awake() {
		patternId = Random.Range(0, patterns.Length - 1);
	}

	/// <summary> 是否顯示為正面 </summary>
	public bool isFront {
		set {
			frontSprite?.gameObject.SetActive(value);
			backSprite?.gameObject.SetActive(!value);
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
	//[SerializeField] Animator mAnimator;
	[SerializeField] public Animator animator {	get;}
}
