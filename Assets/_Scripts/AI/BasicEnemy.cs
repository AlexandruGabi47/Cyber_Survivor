using CAUnityFramework;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private float meleeDamage = 1f;
	[SerializeField] private float damageCooldown = 0.2f;

	private float lastDamageTime = 0f;

	[SerializeField] private DamageData damageDetails;

	private void Awake()
	{
		this.damageDetails = new DamageData
		{
			Damage = this.meleeDamage,
			Type = DamageType.Energy,
			Source = this.gameObject
		};
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player") && Time.time > this.lastDamageTime + this.damageCooldown)
		{
			var health = other.GetComponent<Health>();
			if (health != null)
			{
				health.DealDamage(this.damageDetails);
				this.lastDamageTime = Time.time;
			}
		}
	}
}
