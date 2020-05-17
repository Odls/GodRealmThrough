using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : CharacterPart {
	[SerializeField] PatternSprite patternSprite;
	[SerializeField] ActionTrigger[] triggers;
	[SerializeField] Realm realm;

	internal override void Init(Character p_character) {
		base.Init(p_character);
		patternSprite.pattern = p_character.pattern;
		foreach(ActionTrigger _trigger in triggers) {
			_trigger.Init(p_character);
		}
		realm.isOn = true;
	}

	void BulletEnd() {
		Destroy(gameObject);
	}
}
