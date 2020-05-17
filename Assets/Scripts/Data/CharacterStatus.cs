using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CHARACTOR_STATE {
    Idle,
    Attack,
    Move,
    Jump
}

[System.Serializable]
public struct CharacterStatus{
    /// <summary> 狀態 (由動畫控制) </summary>
    public CHARACTOR_STATE state;
    /// <summary> 速度 (由動畫控制) </summary>
    public float nowSpeed;
}
