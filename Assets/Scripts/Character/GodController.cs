using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodController : ControllerBase {
	bool angry = false;
	float forwardAngle;
	WorldObject worldObject;
	public void Init(Character p_character, WorldObject p_worldObject) {
		target = p_character;
		worldObject = p_worldObject;

		#region View
		target.pattern = worldObject.pattern;
		forwardAngle = (worldObject.isLeft ? Mathf.PI : 0) + Random.Range(Mathf.PI * -0.49f, Mathf.PI * 0.49f);
		target.forwardAngle = forwardAngle;
		#endregion

		#region register Event
		target.OnHit += OnHit;
		target.OnDie += OnDie;
		#endregion
	}

	/// <summary> 旋轉速度 (角度) </summary>
	[SerializeField] float rotaSpeed = 60;
	/// <summary> 移動時停止的機率 </summary>
	[SerializeField] float stopRate = 0.01f;
	/// <summary> 停止時移動的機率 </summary>
	[SerializeField] float moveRate = 0.01f;
	/// <summary> 攻擊的機率 </summary>
	[SerializeField] float attackRate = 0.2f;
	protected override void Update() {
		base.Update();
		if (angry) {
			Vector2 _targetRay = PlayerController.playerCharacter.transform.position - target.transform.position;
			float _targetAngle = Mathf.Atan2(_targetRay.y, _targetRay.x);
			float _deltaAngle = _targetAngle - forwardAngle;
			float _sqrMagnitude = _targetRay.sqrMagnitude;

			switch (target.state) {
			case CHARACTOR_STATE.Idle:
			case CHARACTOR_STATE.Move:
				#region Rota
				if (_deltaAngle > Mathf.PI) {
					_targetAngle -= Mathf.PI * 2;
					_deltaAngle = _targetAngle - forwardAngle;
				} else if (_deltaAngle < -Mathf.PI) {
					_targetAngle += Mathf.PI * 2;
					_deltaAngle = _targetAngle - forwardAngle;
				}				
				forwardAngle = Mathf.MoveTowards(forwardAngle, _targetAngle, rotaSpeed * Mathf.Deg2Rad * Time.deltaTime);
				#endregion


				#region Move Or Idle
				switch (target.state) {
				case CHARACTOR_STATE.Idle:
					if (Random.value <= moveRate) {
						target.moveDirection = new Vector2(Mathf.Cos(forwardAngle), Mathf.Sin(forwardAngle));
					} else {
						target.moveDirection = Vector2.zero;
					}
					break;
				case CHARACTOR_STATE.Move:
					if ((Random.value <= moveRate) || (_sqrMagnitude < 1)) {
						target.moveDirection = Vector2.zero;
					} else {
						target.moveDirection = new Vector2(Mathf.Cos(forwardAngle), Mathf.Sin(forwardAngle));
					}
					break;
				}
				#endregion

				#region Attack
				if (Random.value <= attackRate) {
					List<AttackData> _attackList = target.data.GetValidAttack(_deltaAngle, _sqrMagnitude);
					int _count = _attackList.Count;
					if (_count > 0) {
						AttackData _doAttack = _attackList[Random.Range(0, _count - 1)];
						target.Attack(_doAttack.name);
					}
				}
				#endregion
				break;
			}
		}		
	}

	void LateUpdate() {
		target.forwardAngle = forwardAngle;
	}
	#region Event
	void OnHit(ActionTrigger p_trigger) {
		target.OpenRealm();
		angry = true;
	}
	void OnDie() {
		Destroy(worldObject.gameObject);
	}
	#endregion
}
