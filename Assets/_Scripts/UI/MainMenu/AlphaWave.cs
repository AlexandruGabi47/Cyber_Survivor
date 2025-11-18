using UnityEngine;
using UnityEngine.UI;

public class AlphaWave : MonoBehaviour
{
    Image image;

	[SerializeField]
    [Range(0, 255)] 
    int minAlpha = 10;

	[SerializeField]
	[Range(0, 255)] 
    int maxAlpha = 50;

    float MinAlpha => this.minAlpha / 255f;
    float MaxAlpha => this.maxAlpha / 255f;

	void Start()
    {
		this.image = this.GetComponent<Image>();
		Color color = this.image.color;
        color.a = this.MinAlpha;
		this.image.color = color;
	}

	void Update()
	{
		float t = (Mathf.Sin(Time.time) + 1f) * 0.5f;
		t = Mathf.SmoothStep(0f, 1f, t); // ease in and out
		float alpha = Mathf.Lerp(this.MinAlpha, this.MaxAlpha, t);
		this.image.color = new Color(
			this.image.color.r,
			this.image.color.g,
			this.image.color.b,
			alpha
		);
	}
}
