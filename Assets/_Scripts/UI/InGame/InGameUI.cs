using UnityEngine;
using UnityEngine.SceneManagement;
using CAUnityFramework;

public class InGameUI : StaticInstance<InGameUI>
{
	[Header("Menus")]
	[SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathMenu;

	[Header("Inputs")]
	[SerializeField] private GameObject InGameInput;
    [SerializeField] private GameObject MenuInput;

    public void TogglePauseMenu()
    {
        PauseManager.TogglePause();
        this.pauseMenu.SetActive(PauseManager.IsPaused());
    }

	public void ResumeGame()
	{
		PauseManager.UnpauseGame();
		this.pauseMenu.SetActive(false);
	}

	public void PauseGame()
	{
		PauseManager.PauseGame();
		this.pauseMenu.SetActive(true);
	}

	public void ReturnToMainMenu()
	{
		PauseManager.UnpauseGame();
		SceneManager.LoadSceneAsync(0);
	}

	public void DeathScreen()
	{
		PauseManager.PauseGame();
		this.deathMenu.SetActive(true);
	}

	public void Respawn()
	{
		PauseManager.UnpauseGame();
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
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

		switch (Application.platform)
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
			default:
				break;
		}

		Application.Quit();
	}

	public void SwitchToMenuInput()
    {
        this.InGameInput.SetActive(false);
        this.MenuInput.SetActive(true);
    }
}
