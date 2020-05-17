using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour{
	[SerializeField] protected CharacterStatus mStatus;
	public CharacterStatus status { get => mStatus; }

	[SerializeField] Realm mRealm;
	public Realm realm { get => mRealm; }

	[SerializeField] protected CharacterData mData;
	public CharacterData data { get => mData; }

	[SerializeField] protected Rigidbody2D rigibody;

	#region View
	[SerializeField] protected CharacterView view;
	public void SetPattern(Sprite p_pattern) {
		view.pattern = p_pattern;
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

	public void Attack() {
		view.PlayAnimation("Attack");
	}

	protected virtual void Update() {
		#region Idle Or Walk
		switch (status.state) {
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
		rigibody.velocity = moveDirection * status.nowSpeed * data.walkSpeed * Time.deltaTime;
		#endregion
	}
}
