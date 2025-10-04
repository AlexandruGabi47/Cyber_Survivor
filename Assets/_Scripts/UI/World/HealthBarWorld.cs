using CAUnityFramework;
using UnityEngine;

public class HealthBarWorld : HealthBar
{
	[SerializeField] private Transform slider;

	protected override void UpdateCurrentHealth()
	{
		this.slider.localScale = new Vector3(this.health.Current / this.health.Max, 1f, 1f);
	}

	protected override void UpdateMaxHealth()
	{
		// Nothing to do here
	}
}
