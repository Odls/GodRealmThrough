using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Realm : MonoBehaviour {
	[SerializeField] Animator animator;

	[SerializeField] float mRadius;
	/// <summary> 半徑 </summary>
	public float radius { get => mRadius; }

	Camera mainCamera;
	void Awake() {
		mainCamera = Camera.main;
		animator.Play("Off", -1, 1);
	}
	void Update() {
		#region Look Camera
		Vector3 _localCmrPos = transform.parent.InverseTransformPoint(mainCamera.transform.position);
		Vector3 _ray = _localCmrPos - transform.localPosition;
		float _atan = Mathf.Atan2(_ray.z, _ray.x);
		transform.localRotation = Quaternion.Euler(0, -_atan * Mathf.Rad2Deg, 0);
		#endregion
	}

	bool mIsOn = false;
	public bool isOn {
		get => mIsOn;
		set {
			if(mIsOn != value) {
				mIsOn = value;
				animator.Play((mIsOn?"On":"Off"), -1, 0);
			}
		}
	}
}
