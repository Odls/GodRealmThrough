using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterData", menuName ="Character Data")]
public class CharacterData : ScriptableObject{

	[SerializeField] float mMoveSpeed = 1;
	/// <summary> 基礎移動速度 </summary>
	public float walkSpeed { get => mMoveSpeed; }
}
