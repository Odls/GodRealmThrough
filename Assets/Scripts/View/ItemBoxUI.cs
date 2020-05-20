using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxUI : MonoBehaviour{
	[SerializeField] int index;
	[SerializeField] Image iconImage;
	[SerializeField] Text countText;


	int count {
		set {
			iconImage.gameObject.SetActive(value > 0);
			countText.gameObject.SetActive(value > 0);
			countText.text = value.ToString();
		}
	}

	private void Awake() {
		count = 0;
		ItemManager.OnItemChange += OnItemChange;
	}

	void OnItemChange(int p_index, ItemBox p_box) {
		if(p_index == index) {
			iconImage.sprite = p_box.itemData.icon;
			count = p_box.count;
		}
	}
}
