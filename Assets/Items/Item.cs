using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
	[SerializeField] private string name;
	[SerializeField] private Sprite icon;
	[SerializeField] private bool isStackable;

	public virtual void OnPickup(GameObject picker) { }
}

[System.Serializable]
public class ItemStack
{
	private Item item;
	private int amount;

	public ItemStack(Item item, int amount)
	{
		this.item = item;
		this.amount = amount;
	}
}
