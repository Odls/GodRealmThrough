using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemManager{
	const int boxSize = 10;
	static ItemBox[] boxs = new ItemBox[boxSize];

	static ItemManager() {
		for (int f = 0; f < boxs.Length; f++) {
			boxs[f] = new ItemBox();
		}
	}

	public static System.Action<int, ItemBox> OnItemChange;

	/// <summary> 增加道具 </summary>
	/// <param name="p_itemData"> 道具 </param>
	public static void AddItem(ItemData p_itemData) {
		bool _canAdd = false;

		// Try Pile
		for (int f = 0; f < boxs.Length; f++) {
			if (boxs[f].TryPileItem(p_itemData)) {
				_canAdd = true;
				OnItemChange?.Invoke(f, boxs[f]);
				break;
			}
		}

		if (!_canAdd) {
			// Try Add
			for (int f = 0; f < boxs.Length; f++) {
				if (boxs[f].TryAddItem(p_itemData)) {
					_canAdd = true;
					OnItemChange?.Invoke(f, boxs[f]);
					break;
				}
			}
		}

		if (!_canAdd) {
			// TODO : No Empty Box
		}
	}
}


