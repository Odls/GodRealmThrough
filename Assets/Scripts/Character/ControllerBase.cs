using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBase : MonoBehaviour{
	[SerializeField] protected Character target;

	public virtual void Init(Character p_character) {
		target = p_character;
		target.OnHit += OnHit;
		target.OnDie += OnDie;
	}

	protected virtual void OnDestroy() {
		target.OnHit -= OnHit;
		target.OnDie -= OnDie;
	}

	protected virtual void Update() {}
	protected virtual void OnHit(ActionTrigger p_trigger) {}
	protected virtual void OnDie() {}
}
