using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CAUnityFramework;

public class ListInventoryItemHandler : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI itemName;
	[SerializeField] private Image itemIcon;

	[SerializeField] private GameObject selectedObject;

	public void SetSelected(bool selected)
	{
		if (this.selectedObject != null)
			this.selectedObject.SetActive(selected);
	}

	public void UpdateDisplay(ItemInstance item)
	{
		if (item == null)
		{
			this.itemName.text = "Empty";
			this.itemIcon.sprite = null;
		}
		else
		{
			this.itemName.text = $"{item.GetDisplayName()} - x{item.Quantity}";
			this.itemIcon.sprite = item.GetIcon();
		}
		this.UpdateTextWidthToFit();
		this.ResizeSelectedOverlayToText();
	}

	private void UpdateTextWidthToFit()
	{
		if (this.itemName != null)
		{
			// Force TextMeshPro to update its layout
			this.itemName.ForceMeshUpdate();

			// Get the preferred width of the text
			float preferredWidth = this.itemName.preferredWidth;

			// Set the RectTransform width to match the text
			RectTransform textRect = this.itemName.GetComponent<RectTransform>();
			textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, preferredWidth);
		}
	}

	private void ResizeSelectedOverlayToText()
	{
		if (this.selectedObject != null && this.itemName != null)
		{
			RectTransform textRect = this.itemName.GetComponent<RectTransform>();
			RectTransform overlayRect = this.selectedObject.GetComponent<RectTransform>();

			// Optionally add some padding
			float paddingX = 26f;
			float paddingY = 8f;

			overlayRect.sizeDelta = new Vector2(
				textRect.rect.width + paddingX,
				textRect.rect.height + paddingY
			);
		}
	}
}
