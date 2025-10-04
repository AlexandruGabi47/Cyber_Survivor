using UnityEngine;
using CAUnityFramework;

public abstract class HealthBar : MonoBehaviour
{
	[SerializeField] protected Health health;

	void OnEnable()
	{
		this.health.OnMaxUpdate += this.UpdateMaxHealth;
		this.health.OnValueUpdate += this.UpdateCurrentHealth;
	}

	void OnDisable()
	{
		this.health.OnMaxUpdate -= this.UpdateMaxHealth;
		this.health.OnValueUpdate -= this.UpdateCurrentHealth;
	}

	void Start()
	{
		this.UpdateMaxHealth();
		this.UpdateCurrentHealth();
	}

	protected abstract void UpdateMaxHealth();
	protected abstract void UpdateCurrentHealth();
}