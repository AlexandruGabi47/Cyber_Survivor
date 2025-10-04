using CAUnityFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIListInventoryMB : UIBaseInventory
{
	public GameObject itemDisplayPrefab;
	public float itemHeight = 15f;

	private readonly List<ListInventoryItemHandler> itemHandlers = new();

	public override void UpdateInventory(BaseInventory inventory)
	{
		this.UpdateItemHandlersDisplay(inventory);
		this.AddItemHandlersForNewItems(inventory);
	}

	private void UpdateItemHandlersDisplay(BaseInventory inventory)
	{
		for (int index = 0; index < this.itemHandlers.Count; index++)
		{
			if (index < inventory.Items.Count)
			{
				this.itemHandlers[index].UpdateDisplay(inventory.Items[index]);
				this.itemHandlers[index].gameObject.SetActive(true);
			}
			else
			{
				this.itemHandlers[index].UpdateDisplay(null);
				this.itemHandlers[index].gameObject.SetActive(false);
			}
		}
	}

	private void AddItemHandlersForNewItems(BaseInventory inventory)
	{
		for (int index = 0; index < inventory.Items.Count; index++)
		{
			if (index < this.itemHandlers.Count)
				continue;

			var itemGO = Instantiate(this.itemDisplayPrefab, this.transform);
			itemGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -this.itemHeight * index);

			var itemHandler = itemGO.GetComponent<ListInventoryItemHandler>();
			itemHandler.UpdateDisplay(inventory.Items[index]);
			this.itemHandlers.Add(itemHandler);
		}
	}

	public void ShowSelectedSlot(int selectedInventoryIndex)
	{
		for (int index = 0; index < this.itemHandlers.Count; index++)
		{
			this.itemHandlers[index].SetSelected(index == selectedInventoryIndex);
		}
	}
}
