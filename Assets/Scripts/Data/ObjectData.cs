using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "Object Data")]
public class ObjectData : ScriptableObject {
	[SerializeField] GodController mGodPrefab;
	/// <summary> 角色預製物 </summary>
	public GodController godPrefab { get => mGodPrefab; }

	[SerializeField] Sprite[] patterns;
	public Sprite GetRamdomPattern() {
		return GetPattern(Random.Range(0, patterns.Length - 1));
	}
	public Sprite GetPattern(int p_index) {
		return patterns[Mathf.Clamp(p_index, 0, patterns.Length-1)];
	}

}
