using CAUnityFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : HealthBar
{
    [SerializeField] private Slider sliderUI;
    [SerializeField] private TextMeshProUGUI healthText;

	protected override void UpdateMaxHealth()
    {
        this.sliderUI.maxValue = this.health.Max;
        this.healthText.text = $"{this.health.Current} / {this.health.Max}";
	}

    protected override void UpdateCurrentHealth()
    {
        this.sliderUI.value = this.health.Current;
        this.healthText.text = $"{this.health.Current} / {this.health.Max}";
	}
}
