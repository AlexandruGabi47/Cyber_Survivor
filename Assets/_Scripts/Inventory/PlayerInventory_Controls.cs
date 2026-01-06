using System;
using UnityEngine;
using CAUnityFramework;
using UnityEngine.InputSystem;
using CAUnityFramework.ItemSystem;

public partial class PlayerInventory : MonoBehaviour
{
	#region Unity Event Functions
	public void OnEnable()
	{
		this.useItem.action.performed += this.GetUseItemAction();
		this.useItem.action.canceled += this.GetReleaseItemAction();
		this.useItem.action.Enable();

		this.reloadItem.action.performed += this.GetReloadItemAction();
		this.reloadItem.action.Enable();

		this.dropItem.action.performed += this.GetDropItemAction();
		this.dropItem.action.Enable();

		this.hotbarScroll.action.performed += this.GetHotbarScrollAction();
		this.hotbarScroll.action.Enable();
	}

	public void OnDisable()
	{
		this.useItem.action.performed -= this.GetUseItemAction();
		this.useItem.action.canceled -= this.GetReleaseItemAction();
		this.useItem.action.Disable();

		this.reloadItem.action.performed -= this.GetReloadItemAction();
		this.reloadItem.action.Disable();

		this.dropItem.action.performed -= this.GetDropItemAction();
		this.dropItem.action.Disable();

		this.hotbarScroll.action.performed -= this.GetHotbarScrollAction();
		this.hotbarScroll.action.Disable();
	}
	#endregion

	#region Input Action Callbacks
	private Action<InputAction.CallbackContext> GetUseItemAction() =>
		ctx => this.UseItem(ItemActionType.Primary, ItemActionState.Pressed);

	private Action<InputAction.CallbackContext> GetReleaseItemAction() =>
		ctx => this.UseItem(ItemActionType.Primary, ItemActionState.Released);

	private Action<InputAction.CallbackContext> GetReloadItemAction() =>
		ctx => this.UseItem(ItemActionType.Reload, ItemActionState.Pressed);

	private Action<InputAction.CallbackContext> GetDropItemAction() =>
		ctx => this.DropItem();

	private Action<InputAction.CallbackContext> GetHotbarScrollAction() =>
		ctx => this.ScrollInventory(ctx.ReadValue<Vector2>());
	#endregion
	
	private void UseItem(ItemActionType actionType, ItemActionState actionState)
	{
		if (this.mainInventory.Items.Count > 0)
		{
			this.mainInventory.Items[this.currentItemIndex].Use(actionType, actionState);
		}
	}

	private void DropItem()
	{
		if (this.mainInventory.Items.Count > 0)
		{
			ItemStack itemToDrop = this.mainInventory.Items[this.currentItemIndex];
			if (this.mainInventory.RemoveItem(itemToDrop))
			{
				if (this.currentItemIndex >= this.mainInventory.Items.Count)
					this.currentItemIndex = Math.Max(0, this.mainInventory.Items.Count - 1);

				this.inventoryUI.ShowSelectedSlot(this.currentItemIndex);
				ItemPickup2D pickup = Instantiate(this.prefabItemPickup, this.pickupParent.transform);
				pickup.transform.position = this.transform.position;
				pickup.SetItemInstance(itemToDrop);
			}
		}
	}

	private void ScrollInventory(Vector2 scrollValue)
	{
		if (scrollValue.y > 0f)
			this.ScrollUp();
		else if (scrollValue.y < 0f)
			this.ScrollDown();
	}

	private void ScrollUp()
	{
		this.currentItemIndex--;
		if (this.currentItemIndex < 0)
		{
			this.currentItemIndex = this.mainInventory.Items.Count - 1;
		}
		this.inventoryUI.ShowSelectedSlot(this.currentItemIndex);
	}

	private void ScrollDown()
	{
		this.currentItemIndex++;
		if (this.currentItemIndex >= this.mainInventory.Items.Count)
		{
			this.currentItemIndex = 0;
		}
		this.inventoryUI.ShowSelectedSlot(this.currentItemIndex);
	}
}
