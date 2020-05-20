using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DropItem {
	[SerializeField] ItemData mData;
	/// <summary> 道具 </summary>
	public ItemData data { get => mData; }

	[SerializeField] int mCount;
	/// <summary> 數量 </summary>
	public int count { get => mCount; }

}

[CreateAssetMenu(fileName = "ItemData", menuName = "Item Data")]
public class ItemData : ScriptableObject {
	[SerializeField] Sprite mIcon;
	/// <summary> 圖示 </summary>
	public Sprite icon { get => mIcon; }

	[SerializeField] int mMaxPileCount;
	/// <summary> 在道具箱中收納在一格內的數量上限 </summary>
	public int maxPileCount { get => mMaxPileCount; }
}
