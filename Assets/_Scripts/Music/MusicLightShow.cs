using CAUnityFramework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MusicLightShow : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    private readonly int sampleSize = 512;
    float[] samples;

    [SerializeField] private FFTWindow window = FFTWindow.Blackman;
    [Header("Lighting")]
    [SerializeField] private float maxIntensity = 20f;

    [SerializeField] private List<FrequencyBandHandler> frequencyBandHandlers;

    void Start()
	{
		this.samples = new float[this.sampleSize];
    }

	void Update()
    {
        if (PauseManager.IsPaused())
            return;

        this.audioSource.GetSpectrumData(this.samples, 0, this.window);

		foreach (var bandHandler in this.frequencyBandHandlers)
		{
            bandHandler.Update(this.samples, this.maxIntensity);
        }
    }
}
