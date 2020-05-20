using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : CharacterPart {
	[SerializeField] Bullet bulletPrefab;
	/// <summary> 發射子彈 (由動畫呼叫) </summary>
	public void Shoot() {
		// Wait Animator Refresh Postion, Shoot When Update
		needShoot = true;
	}

	bool needShoot = false;
	void Update() {
		if (needShoot) {
			needShoot = false;
			Bullet _bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
			_bullet.Init(character);
		}
	}
}
