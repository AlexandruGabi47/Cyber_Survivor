using UnityEngine;
using CAUnityFramework;
using UnityEngine.InputSystem;

public class EquippedItemController : MonoBehaviour
{
	[SerializeField] private InputActionReference useAction;

	private TargetingBehaviour2D targetingB;
	//temporary weapon reference
	private BasicWeapon2D weapon;

	public void Start()
	{
		this.weapon = this.GetComponentInChildren<BasicWeapon2D>();
		this.targetingB = this.GetComponent<TargetingBehaviour2D>();
		this.targetingB.SetTrackedObject(MouseManager.CursorObject);
	}

	public void Update()
	{
		if (PauseManager.IsPaused())
			return;
		//????
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
		if(MouseManager.IsPointerOverUI())
			return;

		if(this.weapon != null)
			this.weapon.Hold();
	}

	private void ReleaseCurrentItemAction()
	{
		if (this.weapon != null)
			this.weapon.Release();
	}
}