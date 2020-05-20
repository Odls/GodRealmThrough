using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour{
	[SerializeField] DropItemView dropItemPrefab;
	[SerializeField] ObjectData data;
	[SerializeField] Transform spriteTop;
	[SerializeField] GodController god;
	Character character;

	private void Awake() {
		// Set Pattern
		pattern = data.GetRamdomPattern();

		// Random Left or Right
		isLeft = (Random.value < 0.5f);

		hp = data.hp;

		// Create God Character
		character = Instantiate(data.characterPrefab, transform.position, transform.rotation, transform.parent);
		god.Init(character, this);
    }
	void OnTriggerEnter2D(Collider2D p_collision) {
		ActionTrigger _trigger = p_collision.gameObject.GetComponent<ActionTrigger>();

		if ( ObjectManager.EffectIsValidAtPos(transform.position, E_EFFECT_WORLD.Base)) {
			if ((_trigger.type & E_TRIGGER_TYPE.Touch) != E_TRIGGER_TYPE.None) {
				// Is Touch
				Hit(_trigger);
			}
		}
	}

	[SerializeField] float mHp = 100;
	public float hp {
		get => mHp;
		private set {
			mHp = value;
			if (mHp < 0) {
				Break();
			}
		}
	}

	void Hit(ActionTrigger p_trigger) {
		hp -= p_trigger.atk;
	}
	void Break() {
		mHp = 0;
		character.Die();

		foreach(DropItem _dropItem in data.drops) {
			for(int f=0; f< _dropItem.count; f++) {
				DropItemView _dropItemView = Instantiate(dropItemPrefab, transform.position, transform.rotation, transform.parent);
				_dropItemView.Pop(
					_dropItem.data.icon,
					PlayerController.playerCharacter.transform,
					() => ItemManager.AddItem(_dropItem.data)
				);
			}
		}
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
