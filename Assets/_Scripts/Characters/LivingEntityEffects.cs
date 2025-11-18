using CAUnityFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntityEffects : MonoBehaviour
{
	[SerializeField] private Health health;
	[SerializeField] private SpriteRenderer[] spriteRenderers;
	private List<Color> originalColors = new();

	[SerializeField] private Color onHitTint = Color.red;
	[SerializeField] private float tintDuration = 0.05f;

	public void Awake()
	{
		foreach (var spriteRenderer in this.spriteRenderers)
		{
			this.originalColors.Add(spriteRenderer.color);
		}
	}

	public void OnEnable()
	{
		if (this.health != null)
		{
			this.health.OnHit.AddListener(this.OnHit);
		}
	}

	public void OnDisable()
	{
		if (this.health != null)
		{
			this.health.OnHit.RemoveListener(this.OnHit);
		}
	}

	private void OnHit()
	{
		for (int index = 0; index < this.spriteRenderers.Length; index++)
		{
			this.StartCoroutine(this.TintSpriteRendererCoroutine(
				this.spriteRenderers[index],
				this.originalColors[index], 
				this.onHitTint, 
				this.tintDuration));
		}
	}

	private IEnumerator TintSpriteRendererCoroutine(
		SpriteRenderer spriteRenderer,
		Color originalColor,
		Color tint,
		float duration)
	{
		spriteRenderer.color = tint;
		yield return new WaitForSeconds(duration);
		spriteRenderer.color = originalColor;
	}
}