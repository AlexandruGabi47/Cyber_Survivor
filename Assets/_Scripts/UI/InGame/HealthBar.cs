using UnityEngine;
using CAUnityFramework;

public abstract class HealthBar : MonoBehaviour
{
	[SerializeField] protected Health health;

	void OnEnable()
	{
		if (this.health != null)
		{
			this.health.OnMaxUpdate += this.UpdateMaxHealth;
			this.health.OnValueUpdate += this.UpdateCurrentHealth;
		}
	}

	void OnDisable()
	{
		if (this.health != null)
		{
			this.health.OnMaxUpdate -= this.UpdateMaxHealth;
			this.health.OnValueUpdate -= this.UpdateCurrentHealth;
		}
	}

	void Start()
	{
		this.UpdateMaxHealth();
		this.UpdateCurrentHealth();
	}

	protected abstract void UpdateMaxHealth();
	protected abstract void UpdateCurrentHealth();
}