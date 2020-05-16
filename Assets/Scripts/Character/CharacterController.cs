using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour{
	[SerializeField] protected CharacterStatus status;
	[SerializeField] protected CharacterData data;

	[SerializeField] protected Rigidbody2D rigibody;
	[SerializeField] protected CharacterView view;

	private bool mHasMoveDirection = false;
	/// <summary> 輸入方向不為零 </summary>
	protected bool hasMoveDirection { get => mHasMoveDirection; }

	Vector2 mMoveDirection = Vector2.zero;
	/// <summary> 輸入方向 </summary>
	protected Vector2 moveDirection {
		get => mMoveDirection;
		set {
			mMoveDirection = value;
			mHasMoveDirection = mMoveDirection.sqrMagnitude > 0;
		}
	}


	protected virtual void Update() {
		CheckState();

		// Rota When Has Move
		if (hasMoveDirection) {
			float _angle = Mathf.Atan2(moveDirection.y, moveDirection.x);
			view.forwardAngle = _angle;
		}

		rigibody.velocity = moveDirection * status.nowSpeed * data.walkSpeed * Time.deltaTime;
	}
	protected virtual void CheckState() {
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
	}
}
