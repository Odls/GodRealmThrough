using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour{
	static Camera mainCamera;

	public void Update() {
		if (mainCamera == null) { mainCamera = Camera.main; }
		Vector3 _localCmrPos;
		if (transform.parent != null) {
			_localCmrPos = transform.parent.InverseTransformPoint(mainCamera.transform.position);
		} else {
			_localCmrPos = mainCamera.transform.position;
		}
		Vector3 _ray = _localCmrPos - transform.localPosition;
		float _atan = Mathf.Atan2(_ray.z, _ray.x);
		transform.localRotation = Quaternion.Euler(0, -_atan * Mathf.Rad2Deg, 0);
	}
}
