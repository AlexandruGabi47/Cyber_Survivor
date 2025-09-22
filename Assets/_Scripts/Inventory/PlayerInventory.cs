using UnityEngine;
using CAUnityFramework;
using UnityEngine.InputSystem;

public partial class PlayerInventory : MonoBehaviour
{
	[Header("Inventory")]
	[SerializeField] private BaseInventory mainInventory = new();
    [SerializeField] private UIListInventoryMB inventoryUI;

	[Header("Input Actions")]
	[SerializeField] private InputActionReference useItem;
	[SerializeField] private InputActionReference dropItem;
	[SerializeField] private InputActionReference hotbarScroll;

	[Header("Item Pickup")]
	[SerializeField] private GameObject pickupParent;
	[SerializeField] private ItemPickup2D prefabItemPickup;

	private int currentItemIndex = 0;

	void Start()
	{
		this.mainInventory.OnInventoryContentUpdate += this.UpdateInventoryUI;
	}

	private void UpdateInventoryUI()
	{
		this.inventoryUI.UpdateInventory(this.mainInventory);
		this.inventoryUI.ShowSelectedSlot(this.currentItemIndex);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.TryGetComponent(out ItemPickup2D itemPickup))
		{
			if (itemPickup.CanBePickedUp())
			{
				this.PickupItem(itemPickup);
			}
		}
	}

	private void PickupItem(ItemPickup2D itemPickup)
	{
		ItemInstance itemIns = itemPickup.ItemStack;
		this.mainInventory.TryAddItem(itemIns, out ItemInstance remainingItems);
		itemPickup.SetItemInstance(remainingItems);
		itemPickup.PickupDone();
	}
}
