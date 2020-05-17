using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ControllerBase {
	protected override void Update() {
		CharacterStatus _status = target.status;

		#region Set Move Direction
		switch (_status.state) {
        case CHARACTOR_STATE.Idle:
        case CHARACTOR_STATE.Move:
            float _axisX = Input.GetAxis("Horizontal");
            float _axisY = Input.GetAxis("Vertical");
			target.moveDirection = new Vector2(_axisX, _axisY);
            break;
        }
		#endregion

		#region On Off Realm
		if (Input.GetButtonDown("GodRealm")) {
			target.realm.isOn ^= true; // Invert, Same As [realm.isOn = !realm.isOn]
		}
		#endregion

		#region Attack
		if (Input.GetButtonDown("Attack")) {
			switch (_status.state) {
			case CHARACTOR_STATE.Idle:
			case CHARACTOR_STATE.Move:
				target.Attack();
				break;
			}
		}
		#endregion

		base.Update();
	}
}
