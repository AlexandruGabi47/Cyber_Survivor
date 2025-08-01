using CAUnityFramework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    public void ReturnToMainMenu()
	{
		PauseManager.PauseGame(false);
		SceneManager.LoadSceneAsync(0);
	}
}
