using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum E_TRIGGER_TYPE {
	None			= 0,
	Touch			= 1 << 1,
	GroundAttack	= 1 << 2,
	SkyAttack		= 1 << 3,
	All				= unchecked((int)0xFFFFFFFF),
}
[System.Flags]
public enum E_EFFECT_WORLD {
	None	= 0,
	Base	= 1 << 1,
	God		= 1 << 2,
	Both	= Base | God,
}

public class ActionTrigger : MonoBehaviour{
	[SerializeField, BitMask(typeof(E_TRIGGER_TYPE))]
	E_TRIGGER_TYPE mType = E_TRIGGER_TYPE.GroundAttack;
	/// <summary> 觸發類型 </summary>
	public E_TRIGGER_TYPE type { get => mType; }

	[SerializeField, BitMask(typeof(E_EFFECT_WORLD))]
	E_EFFECT_WORLD mEffectWorld = E_EFFECT_WORLD.God;
	/// <summary> 觸發世界 </summary>
	public E_EFFECT_WORLD effectWorld { get => mEffectWorld; }

	[SerializeField] float mPowerRate = 1;
	/// <summary> 強度 </summary>
	public float powerRate { get => mPowerRate; }

	//Collider2D collider;
	//private void Awake() {
	//	collider = GetComponent<Collider2D>();
	//}
	//private void OnEnable() {
	//	collider.enabled = false;
	//	collider.enabled = true;
	//}
}
