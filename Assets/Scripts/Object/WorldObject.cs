using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour{
	[SerializeField] ObjectData data;
	[SerializeField] Transform spriteTop;
	GodController god;

	private void Awake() {
		// Set Pattern
		pattern = data.GetRamdomPattern();

		// Random Left or Right
		isLeft = (Random.value < 0.5f);

		// Create God
		god = Instantiate(data.godPrefab, transform.position, transform.rotation, transform.parent);
		god.Init(this);
    }

	#region Angle
	bool mIsLeft;
	// 面相左側
	public bool isLeft {
		get => mIsLeft;
		set {
			mIsLeft = value;
			Vector3 _scale = spriteTop.transform.localScale;
			_scale.x *= (mIsLeft ? 1 : -1);
			spriteTop.transform.localScale = _scale;
		}
	}
	#endregion


	#region pattern
	[SerializeField] PatternSprite patternSprite;
	/// <summary> 色板 </summary>
	public Sprite pattern {
		get => patternSprite.pattern;
		set => patternSprite.pattern = value;
	}
	#endregion
}
