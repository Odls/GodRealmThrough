using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour{

	[SerializeField] protected CharacterData mData;
	public CharacterData data { get => mData; }

	[SerializeField] protected Rigidbody2D rigibody;

	CharacterPart[] parts;
	void Awake() {
		mStatus.hp = data.hp;

		parts = GetComponentsInChildren<CharacterPart>(true);
		foreach (CharacterPart _part in parts) {
			_part.Init(this);
		}
	}

	#region Realm
	[SerializeField] Realm mRealm;
	public Realm realm { get => mRealm; }
	public void OpenRealm() { realm.isOn = true; }
	public void CloseRealm() { realm.isOn = false; }
	#endregion

	#region Status
	[SerializeField] protected CharacterStatus mStatus;
	public CharacterStatus status { get => mStatus; }

	public CHARACTOR_STATE state {
		get => mStatus.state;
		private set => mStatus.state = value;
	}

	public float hp {
		get => mStatus.hp;
		private set {
			mStatus.hp = value;
			if(mStatus.hp < 0) {
				Die();
			}
		}
	}

	float stateSpeed {
		get => mStatus.nowSpeed;
		set => mStatus.nowSpeed = value;
	}

	#endregion

	#region View
	[SerializeField] protected CharacterView view;
	public Sprite pattern {
		set => view.pattern = value;
		get => view.pattern;
	}
	#endregion

	#region Angle
	private bool mHasMoveDirection = false;
	/// <summary> 輸入方向不為零 </summary>
	protected bool hasMoveDirection { get => mHasMoveDirection; }

	Vector2 mMoveDirection = Vector2.zero;
	/// <summary> 輸入方向 </summary>
	public Vector2 moveDirection {
		get => mMoveDirection;
		set {
			mMoveDirection = value.normalized;
			mHasMoveDirection = mMoveDirection.sqrMagnitude > 0;
		}
	}

	/// <summary> 前方角 (徑度) </summary>
	public float forwardAngle { set => view.forwardAngle = value; }
	#endregion

	#region Fight
	void OnTriggerEnter2D(Collider2D p_collision) {
		ActionTrigger _trigger = p_collision.gameObject.GetComponent<ActionTrigger>();
		
		if(ObjectManager.TriggerIsValidInState(_trigger, state) && ObjectManager.EffectIsValidAtPos(transform.position, E_EFFECT_WORLD.God)) {
			if ((_trigger.type & E_TRIGGER_TYPE.Attack) != E_TRIGGER_TYPE.None) {
				// Is Attack
				Hit(_trigger);
				if (_trigger.hitEffect != null) {
					Instantiate(_trigger.hitEffect, p_collision.bounds.ClosestPoint(transform.position), transform.rotation);
				}
			}
		}
	}
	void Hit(ActionTrigger p_trigger) {
		view.PlayAnimation("Hit");
		OnHit?.Invoke(p_trigger);
		hp -= p_trigger.atk;
	}
	public void Action() {
		view.PlayAnimation("Action");
	}
	public void Attack(string p_name) {
		view.PlayAnimation(p_name);
	}
	public void Dash() {
		view.PlayAnimation("Dash");
	}
	[SerializeField] Gun gun;
	public void Shoot() {
		gun.Shoot();
	}
	public void Die() {
		mStatus.hp = 0;
		view.PlayAnimation("Die");
	}
	void DieEnd() {
		Destroy(gameObject);
		OnDie?.Invoke();
	}
	#endregion

	#region Event
	public Action<ActionTrigger> OnHit;
	public Action OnDie;
	#endregion
	protected virtual void Update() {
		#region Idle Or Walk
		switch (state) {
		case CHARACTOR_STATE.Idle:
			if (hasMoveDirection) {
				view.PlayAnimation("Walk");
			}
			break;
		case CHARACTOR_STATE.Move:
			if (!hasMoveDirection) {
				view.PlayAnimation("Idle");
			}
			break;
		}
		#endregion
		
		#region Rota When Has Move
		if (hasMoveDirection) {
			float _angle = Mathf.Atan2(moveDirection.y, moveDirection.x);
			forwardAngle = _angle;
		}
		#endregion

		#region Move
		rigibody.velocity = moveDirection * stateSpeed * data.walkSpeed * Time.deltaTime;
		#endregion
	}
}
