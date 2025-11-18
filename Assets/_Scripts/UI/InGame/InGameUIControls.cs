using CAUnityFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(InGameUI))]
public class InGameUIControls : MonoBehaviour
{
    private InGameUI gameUI;

	[Header("Controls")]
	[SerializeField] private InputActionReference pauseKeyAction;

	private void Awake()
	{
		this.gameUI = this.GetComponent<InGameUI>();
	}

	private void OnEnable()
	{
		// Pause
		this.pauseKeyAction.action.performed += this.TogglePauseMenu;
		this.pauseKeyAction.action.Enable();
	}

	private void OnDisable()
	{
		// Pause
		this.pauseKeyAction.action.performed -= this.TogglePauseMenu;
		this.pauseKeyAction.action.Disable();
	}

	private void TogglePauseMenu(InputAction.CallbackContext context)
	{
		this.gameUI.TogglePauseMenu();
	}
}
