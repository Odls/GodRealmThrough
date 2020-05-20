using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class ItemBox {
	[SerializeField] ItemData itemData = null;
	[SerializeField] int count = 0;

	/// <summary> 嘗試加入物品 </summary>
	/// <param name="p_itemData"> 物品 </param>
	/// <returns> 是否成功加入 </returns>
	public bool TryAddItem(ItemData p_itemData) {
		if(itemData == null) {
			itemData = p_itemData;
			count = 1;
			return true;
		}else if(itemData == p_itemData) {
			count++;
			return true;
		}
		return false;
	}
}

public static class ItemManager{
	const int boxSize = 10;
	static ItemBox[] boxs = new ItemBox[boxSize];

	/// <summary> 增加道具 </summary>
	/// <param name="p_itemData"> 道具 </param>
	public static void AddItem(ItemData p_itemData) {
		bool _canAdd = false;
		for(int f=0; f< boxs.Length; f++) {
			if(boxs[f].TryAddItem(p_itemData)) {
				_canAdd = true;
				break;
			}
		}

		if (!_canAdd) {
			// TODO : No Empty Box
		}
	}
}


