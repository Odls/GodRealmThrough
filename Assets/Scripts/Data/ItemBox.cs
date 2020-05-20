using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemBox {
	[SerializeField] ItemData mItemData = null;
	public ItemData itemData { get => mItemData; }

	[SerializeField] int mCount = 0;
	public int count { get => mCount; }


	/// <summary> 嘗試將相同的物品疊加 </summary>
	/// <param name="p_itemData"> 物品 </param>
	/// <returns> 是否成功加入 </returns>
	public bool TryPileItem(ItemData p_itemData) {
		if ((itemData != null) &&
		(itemData == p_itemData) &&
		(mCount < itemData.maxPileCount)) {
			mCount++;
			return true;
		} else {
			return false;
		}
	}

	/// <summary> 嘗試將相同的物品加入空格 </summary>
	/// <param name="p_itemData"> 物品 </param>
	/// <returns> 是否成功加入 </returns>
	public bool TryAddItem(ItemData p_itemData) {
		if (mItemData == null) {
			mItemData = p_itemData;
			mCount = 1;
			return true;
		} else {
			return false;
		}
	}
}