using UnityEngine;
using CAUnityFramework.Networking;

public class PlayerList : MonoBehaviour
{
	[SerializeField] private GameObject playerListItemPrefab;
	[SerializeField] private GameObject playerListPanel;

	private void OnEnable()
	{
		ServerEvents.OnPlayerConnected += this.UpdatePlayerList;
		ServerEvents.OnPlayerDisconnected += this.UpdatePlayerList;
	}

	private void OnDisable()
	{
		ServerEvents.OnPlayerConnected -= this.UpdatePlayerList;
		ServerEvents.OnPlayerDisconnected -= this.UpdatePlayerList;
	}

	private void UpdatePlayerList()
	{
		foreach (Transform child in this.playerListPanel.transform)
		{
			Destroy(child.gameObject);
		}
		foreach (var player in NetworkManager.Instance.ConnectedPlayers)
		{
			var listItem = Instantiate(this.playerListItemPrefab, this.playerListPanel.transform);
			var textComponent = listItem.GetComponentInChildren<TMPro.TextMeshProUGUI>();
			if (textComponent != null)
			{
				textComponent.text = player.PlayerData.Username;
			}
		}
	}

	private void HidePlayerList()
	{
		this.playerListPanel.SetActive(false);
	}

	private void ShowPlayerList()
	{
		this.playerListPanel.SetActive(true);
	}
}