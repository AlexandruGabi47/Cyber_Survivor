using CAUnityFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashBarUI : MonoBehaviour
{
    [SerializeField] private Slider sliderUI;
    [SerializeField] private TopDown2DCharacterController characterController;

	private void OnEnable()
	{
		if (this.characterController != null)
		{
			this.characterController.OnDashUpdated.AddListener(this.UpdateDashBar);
		}
	}

	private void OnDisable()
	{
		if (this.characterController != null)
		{
			this.characterController.OnDashUpdated.RemoveListener(this.UpdateDashBar);
		} 
	}

    void Start()
	{
		this.sliderUI.minValue = 0;
		this.sliderUI.maxValue = this.characterController.DashCooldown;
		this.UpdateDashBar(0f);
    }

    private void UpdateDashBar(float cooldownRemaining)
    {
        this.sliderUI.value = this.characterController.DashCooldown - cooldownRemaining;
	}
}
