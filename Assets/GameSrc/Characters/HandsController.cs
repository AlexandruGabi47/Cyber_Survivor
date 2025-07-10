using UnityEngine;
using CAUnityFramework;
using UnityEngine.InputSystem;

public class HandsController : MonoBehaviour
{
    [SerializeField] private GameObject target;
	[SerializeField] private InputActionReference useAction;

	private Weapon2D weapon;

	public void Start()
	{
		this.weapon = this.GetComponentInChildren<PistolController>();
	}

	void Update()
	{
		this.transform.up = this.target.transform.position - this.transform.position;

		if (this.transform.eulerAngles.z <= 180)
			this.transform.localScale = new Vector3(1, 1, 1);
		else
			this.transform.localScale = new Vector3(-1, 1, 1);
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