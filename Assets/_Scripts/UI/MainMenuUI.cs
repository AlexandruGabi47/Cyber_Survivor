using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
	{
		SceneManager.LoadSceneAsync(1);
	}

	public void LoadGame()
	{

	}

	public void Settings()
	{

	}

	public void Quit()
	{
		Application.Quit();
	}
}
