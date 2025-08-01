using UnityEngine;
using UnityEngine.SceneManagement;
using CAUnityFramework;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject InGameInput;
    [SerializeField] private GameObject MenuInput;

    public void TogglePauseMenu()
	{
        PauseManager.TogglePause();

        this.pauseMenu.SetActive(PauseManager.IsPaused());
    }
}
