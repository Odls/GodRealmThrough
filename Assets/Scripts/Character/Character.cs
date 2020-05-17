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
		if (_trigger == null) { return; }

		#region Check Type
		E_TRIGGER_TYPE _canTriggerMask;
		switch (state) {
		case CHARACTOR_STATE.Jump:	// Only SkyAttack
			_canTriggerMask = E_TRIGGER_TYPE.SkyAttack;
			break;
		case CHARACTOR_STATE.Die:   // Nothing
			_canTriggerMask = E_TRIGGER_TYPE.None;
			break;
		default:					// Not SkyAttack
			_canTriggerMask = ~E_TRIGGER_TYPE.SkyAttack;
			break;
		}

		if((_trigger.type & _canTriggerMask) == E_TRIGGER_TYPE.None) {
			return; // Can't Trigger
		}
		#endregion

		#region Check World
		switch (_trigger.effectWorld) {
		case E_EFFECT_WORLD.Base:	// In God World Can't Trigger
			if (RealmManager.IsInRealm(transform.position)) {
				return;
			}
			break;
		case E_EFFECT_WORLD.God:	// In Base World Can't Trigger
			if (!RealmManager.IsInRealm(transform.position)) {
				return;
			}
			break;
		case E_EFFECT_WORLD.Both:	// Always Trigger
			break;
		default:					// Not Trigger
			return;
			break;
		}
		#endregion

		#region Do Action
		if ((_trigger.type & (E_TRIGGER_TYPE.GroundAttack | E_TRIGGER_TYPE.SkyAttack)) != E_TRIGGER_TYPE.None) {
			Hit(_trigger);
		}
		#endregion
	}
	void Hit(ActionTrigger p_trigger) {
		view.PlayAnimation("Hit");
		OnHit?.Invoke(p_trigger);
		hp -= p_trigger.atk;
	}
	public void Attack(string p_name) {
		view.PlayAnimation(p_name);
	}
	[SerializeField] Gun gun;
	public void Shoot() {
		gun.Shoot();
	}
	void Die() {
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
