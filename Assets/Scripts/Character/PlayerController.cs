using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController {
	protected override void Update(){
        switch (status.state) {
        case CHARACTOR_STATE.Idle:
        case CHARACTOR_STATE.Move:
            float _axisX = Input.GetAxis("Horizontal");
            float _axisY = Input.GetAxis("Vertical");
            moveDirection = new Vector2(_axisX, _axisY);
            break;
        }


		if (Input.GetButtonDown("GodRealm")) {
			realm.isOn ^= true; // Invert, Same As [realm.isOn = !realm.isOn]
		}

        base.Update();
    }

    protected override void CheckState() {
        base.CheckState();

        if (Input.GetButtonDown("Atack")) {
		    switch (status.state) {
		    case CHARACTOR_STATE.Idle:
		    case CHARACTOR_STATE.Move:
                view.PlayAnimation("Atack");
                break;
		    }
        }
	}
}
