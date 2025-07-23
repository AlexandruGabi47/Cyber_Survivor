using UnityEngine;
using CAUnityFramework;
using System;

public class UIInventory : MonoBehaviour
{
    [Header("Inventory Grid Generation")]
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private Vector2Int size;

    private GameObject[,] inventorySlots;

    void Awake()
	{
		this.GenerateInventoryGrid();
	}

	public void UpdateInventoryGridSize(Vector2Int newSize)
	{
		this.size = newSize;
		this.RegenerateInventoryGrid();
	}

	public void RegenerateInventoryGrid()
	{
		this.DestroyInventoryGrid();
		this.GenerateInventoryGrid();
	}

	private void DestroyInventoryGrid()
	{
		foreach (var slot in this.inventorySlots)
		{
			Destroy(slot);
		}
		this.inventorySlots = null;
	}

	private void GenerateInventoryGrid()
	{
		if (!this.inventorySlotPrefab.TryGetComponent(out RectTransform prefabRect))
			Debug.LogError("Inventory slot prefab does not have RectTransform.");

		this.inventorySlots = new GameObject[this.size.y, this.size.x];

		GridGenerator.Generate(this.size, prefabRect.sizeDelta, (pos, index) =>
		{
			GameObject tempObj = Instantiate(this.inventorySlotPrefab, this.transform);

			RectTransform tempRectT = tempObj.GetComponent<RectTransform>();
			tempRectT.anchoredPosition = pos;

			tempObj.name = $"{this.name}_{this.inventorySlotPrefab.name}_{index.y}x{index.x}";

			this.inventorySlots[index.y, index.x] = tempObj;
		});
	}
}
