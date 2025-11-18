using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderTextUpdater : MonoBehaviour
{
	[SerializeField] private Slider slider;
	[SerializeField] private TMP_Text valueText;

	private void Awake()
	{
		this.slider.onValueChanged.AddListener(this.UpdateText);
	}

	public void UpdateText(float value)
	{
		this.valueText.text = value.ToString();
	}
}
