using System.Collections.Generic;
using UnityEngine;

namespace CAUnityFramework
{
	public class Inventory : MonoBehaviour
	{
		[SerializeField] private int size;

		private Dictionary<string, Item> resourceItems;
		private ItemSlot[] itemSlots;

		void Start()
		{


		}
	}
}
