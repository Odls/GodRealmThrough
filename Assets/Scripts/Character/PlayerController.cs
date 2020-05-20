using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ControllerBase {
	public static Character playerCharacter { get; private set; } = null;
	void Awake() {
		playerCharacter = target;
	}
	protected override void Update() {

		#region Set Move Direction
		switch (target.state) {
        case CHARACTOR_STATE.Idle:
        case CHARACTOR_STATE.Move:
            float _axisX = Input.GetAxis("Horizontal");
            float _axisY = Input.GetAxis("Vertical");
			target.moveDirection = new Vector2(_axisX, _axisY);
            break;
		default:
			target.moveDirection = Vector2.zero;
			break;
        }
		#endregion

		#region On Off Realm
		if (Input.GetButtonDown("GodRealm")) {
			target.realm.isOn ^= true; // Invert, Same As [realm.isOn = !realm.isOn]
		}
		#endregion

		#region Attack
		switch (target.state) {
		case CHARACTOR_STATE.Idle:
		case CHARACTOR_STATE.Move:
			if (Input.GetButtonDown("Attack")) {
				target.Attack("BaseAttack");
			}
			if (Input.GetButtonDown("Action")) {
				target.Action();
			}
			if (Input.GetButtonDown("Dash")) {
				target.Dash();
			}
			break;
		}
		#endregion

		base.Update();
	}
}
