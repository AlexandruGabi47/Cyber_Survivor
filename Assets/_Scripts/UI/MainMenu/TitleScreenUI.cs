using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUI : MonoBehaviour
{
	public void PlaySandbox()
	{
		SceneManager.LoadScene(GameScene.Sandbox);
	}

	public void Quit()
	{
		#if UNITY_EDITOR
		if (Application.isEditor)
		{
			UnityEditor.EditorApplication.isPlaying = false;
			return;
		}
		#endif

		switch(Application.platform)
		{
			case RuntimePlatform.WindowsPlayer:
			case RuntimePlatform.LinuxPlayer:
				bool confirmQuit = true; // Replace with actual confirmation dialog logic if needed
				if (!confirmQuit)
					return;
				break;
			case RuntimePlatform.WebGLPlayer:
				// WebGL does not support quitting the application
				return;
		}

		Application.Quit();
	}
}
