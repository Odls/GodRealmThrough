using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodController : ControllerBase {
	bool isAngry = false;
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
		ObjectManager.angryShout += OnHearShout;
		#endregion
	}
	private void OnDestroy() {
		target.OnHit -= OnHit;
		target.OnDie -= OnDie;
		ObjectManager.angryShout -= OnHearShout;
	}

	/// <summary> 旋轉速度 (角度) </summary>
	[SerializeField] float rotaSpeed = 60;
	/// <summary> 移動時停止的機率 </summary>
	[SerializeField] float stopRate = 0.01f;
	/// <summary> 停止時移動的機率 </summary>
	[SerializeField] float moveRate = 0.01f;
	/// <summary> 攻擊的機率 </summary>
	[SerializeField] float attackRate = 0.02f;
	/// <summary> 怒吼的機率 </summary>
	[SerializeField] float shoutRate = 0.02f;
	/// <summary> 聽到怒吼的距離 </summary>
	[SerializeField] float hearShoutRadius = 3f;
	protected override void Update() {
		base.Update();
		if (isAngry && (target!= null)) {
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

				#region Angry Shout
				if (Random.value <= shoutRate) {
					ObjectManager.angryShout?.Invoke(target.transform.position);
				}
				#endregion
				break;
			}
		}		
	}

	void LateUpdate() {
		target.forwardAngle = forwardAngle;
	}

	void Angry() {
		target.OpenRealm();
		isAngry = true;
	}

	#region Event
	void OnHit(ActionTrigger p_trigger) {
		Angry();
	}
	void OnDie() {
		if(worldObject!= null) {
			worldObject.Break();
		}
	}
	void OnHearShout(Vector2 p_pos) {
		Vector2 _ray = p_pos - (Vector2)transform.position;
		if(_ray.sqrMagnitude <= (hearShoutRadius* hearShoutRadius)) {
			Angry();
		}
	}
	#endregion
}
