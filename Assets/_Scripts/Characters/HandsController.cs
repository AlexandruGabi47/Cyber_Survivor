using UnityEngine;
using CAUnityFramework;
using UnityEngine.InputSystem;

public class HandsController : MonoBehaviour
{
	[SerializeField] private InputActionReference useAction;

	private Weapon2D weapon;

	public void Start()
	{
		this.weapon = this.GetComponentInChildren<PistolController>();
	}

	private void OnEnable()
	{
		this.useAction.action.performed += ctx => this.HoldCurrentItemAction();
		this.useAction.action.canceled += ctx => this.ReleaseCurrentItemAction();
		this.useAction.action.Enable();
	}

	private void OnDisable()
	{
		this.useAction.action.performed -= ctx => this.HoldCurrentItemAction();
		this.useAction.action.canceled -= ctx => this.ReleaseCurrentItemAction();
		this.useAction.action.Disable();

		this.ReleaseCurrentItemAction();
	}

	private void HoldCurrentItemAction()
	{
		this.weapon.Hold();
	}

	private void ReleaseCurrentItemAction()
	{
		this.weapon.Release();
	}
}