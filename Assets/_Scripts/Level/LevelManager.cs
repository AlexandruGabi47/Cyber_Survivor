using CAUnityFramework;
using UnityEngine;

public class LevelManager : StaticInstance<LevelManager>
{
	[SerializeField] private Transform playersRoot;
	[SerializeField] private Transform npcsRoot;
	[SerializeField] private Transform droppedItemsRoot;

	public static Transform PlayersRoot => Instance.playersRoot;
	public static Transform NPCsRoot => Instance.npcsRoot;
	public static Transform DroppedItemsRoot => Instance.droppedItemsRoot;

}
