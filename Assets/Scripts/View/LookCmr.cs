using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCmr : MonoBehaviour{
	Camera mainCamera;
	void Awake() {
		mainCamera = Camera.main;
	}
	void Update(){
		Vector3 _localCmrPos = transform.parent.InverseTransformPoint(mainCamera.transform.position);
		Vector3 _ray = _localCmrPos - transform.localPosition;
		float _atan = Mathf.Atan2(_ray.z, _ray.x);
		transform.localRotation = Quaternion.Euler(0, -_atan * Mathf.Rad2Deg, 0);
    }
}
