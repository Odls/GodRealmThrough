using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum CHARACTOR_STATE {
    Idle	= 1 << 1,
    Attack	= 1 << 2,
	Move	= 1 << 3,
	Jump	= 1 << 4,
	Hit		= 1 << 5,
	Die		= 1 << 6,
}

[System.Serializable]
public struct CharacterStatus{
    /// <summary> 狀態 (由動畫控制) </summary>
    public CHARACTOR_STATE state;
    /// <summary> 速度 (由動畫控制) </summary>
    public float nowSpeed;

	/// <summary> 血量 </summary>
	public float hp;
}
