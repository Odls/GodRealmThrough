using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectManager{
	public static bool TriggerIsValidInState(ActionTrigger p_trigger, CHARACTOR_STATE state) {
		if (p_trigger == null) { return false; }

		E_TRIGGER_TYPE _canTriggerMask;
		switch (state) {
		case CHARACTOR_STATE.Jump:  // Only SkyAttack
			_canTriggerMask = E_TRIGGER_TYPE.SkyAttack;
			break;
		case CHARACTOR_STATE.Die:   // Nothing
			_canTriggerMask = E_TRIGGER_TYPE.None;
			break;
		default:                    // Not SkyAttack
			_canTriggerMask = ~E_TRIGGER_TYPE.SkyAttack;
			break;
		}

		if ((p_trigger.type & _canTriggerMask) == E_TRIGGER_TYPE.None) {
			return false; // Can't Trigger
		}

		return true;
	}

	public static bool EffectIsValidAtPos(Vector3 p_pos, E_EFFECT_WORLD p_effectWorld) {
		switch (p_effectWorld) {
		case E_EFFECT_WORLD.Base:   // In God World Can't Trigger
			if (RealmManager.IsInRealm(p_pos)) {
				return false;
			}
			break;
		case E_EFFECT_WORLD.God:    // In Base World Can't Trigger
			if (!RealmManager.IsInRealm(p_pos)) {
				return false;
			}
			break;
		case E_EFFECT_WORLD.Both:   // Always Trigger
			break;
		default:                    // Not Trigger
			return false;
			break;
		}

		return true;
	}
}
