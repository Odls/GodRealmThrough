using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodController : ControllerBase {
	WorldObject worldObject;
	public void Init(WorldObject p_worldObject) {
		worldObject = p_worldObject;
		target.SetPattern(worldObject.pattern);
		target.forwardAngle = (worldObject.isLeft ? Mathf.PI : 0) + Random.Range(Mathf.PI * -0.49f, Mathf.PI * 0.49f);
	}
}
