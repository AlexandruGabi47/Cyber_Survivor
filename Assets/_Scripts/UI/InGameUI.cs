using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    public void ReturnToMainMenu()
	{
		SceneManager.LoadSceneAsync(0);
	}
}
