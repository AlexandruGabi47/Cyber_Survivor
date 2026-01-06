using CAUnityFramework;
using CAUnityFramework.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

class CyberSurvivor_GameManager : GameManager
{
	protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		AssignCameraToPlayerCameraPosition();
	}

	private static void AssignCameraToPlayerCameraPosition()
	{
		FollowerBehavior fbComp = CameraManager.Instance.GetComponent<FollowerBehavior>();
		GameObject camPosObj = GameObjectUtils.FindChildWithTag(LevelManager.PlayersRoot, "CameraPosition");
		if (fbComp != null && camPosObj != null)
			fbComp.SetTarget(camPosObj.transform);
	}
}