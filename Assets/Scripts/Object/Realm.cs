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
	void Start() {
		RealmManager.AddRealm(this);
	}
	void OnDestroy() {
		RealmManager.RemoveRealm(this);
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

	/// <summary> 指定座標是否在神界之內 </summary>
	/// <param name="p_pos">座標</param>
	public bool IsInRealm(Vector2 p_pos) {
		if (!isOn) { return false; }

		Vector3 _ray = (Vector3)p_pos - transform.position;
		return _ray.sqrMagnitude <= (radius * radius);
	}
}
