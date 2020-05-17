using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterData", menuName ="Character Data")]
public class CharacterData : ScriptableObject{

	[SerializeField] float mMoveSpeed = 1;
	/// <summary> 基礎移動速度 </summary>
	public float walkSpeed { get => mMoveSpeed; }

	[SerializeField] AttackData[] attacks = new AttackData[] {
		new AttackData {
			name = "Attack",
			triggerAngle =45,
			triggerNear =0,
			triggerFar =2
		}
	};
	/// <summary> 取得有效攻擊列表 </summary>
	/// <param name="p_angle"> 目標角 (角度) </param>
	/// <param name="p_distance"> 目標距離 </param>
	public List<AttackData> GetValidAttack(float p_angle, float p_distance) {
		List<AttackData> _attackList = new List<AttackData>();
		return _attackList;
	}
}

[System.Serializable]
public struct AttackData {
	/// <summary> 名稱 (亦是播放的動畫名) </summary>
	public string name;

	/// <summary> 觸發角 (角度) </summary>
	public float triggerAngle;
	/// <summary> 觸發距離最小值 </summary>
	public float triggerNear;
	/// <summary> 觸發距離最大值 </summary>
	public float triggerFar;

}
