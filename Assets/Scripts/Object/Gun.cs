using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : CharacterPart {
	[SerializeField] Bullet bulletPrefab;
	public void Shoot() {
		Bullet _bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
		_bullet.Init(character);
	}
}
