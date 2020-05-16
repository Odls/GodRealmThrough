using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodController : CharacterController{
	WorldObject worldObject;
	public void Init(WorldObject p_worldObject) {
		worldObject = p_worldObject;
		view.pattern = worldObject.pattern;
		forwardAngle = (worldObject.isLeft ? Mathf.PI : 0);
	}
}
