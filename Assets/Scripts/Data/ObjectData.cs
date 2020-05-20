using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "Object Data")]
public class ObjectData : ScriptableObject {
	[SerializeField] Character mCharacterPrefab;
	/// <summary> 角色預製物 </summary>
	public Character characterPrefab { get => mCharacterPrefab; }

	[SerializeField] float mHp = 100;
	/// <summary> 總血量 </summary>
	public float hp { get => mHp; }

	[SerializeField] DropItem[] mDrops;
	/// <summary> 掉落物 </summary>
	public DropItem[] drops { get => mDrops; }

	[SerializeField] Sprite[] patterns;
	public Sprite GetRamdomPattern() {
		return GetPattern(Random.Range(0, patterns.Length - 1));
	}
	public Sprite GetPattern(int p_index) {
		return patterns[Mathf.Clamp(p_index, 0, patterns.Length-1)];
	}

}
