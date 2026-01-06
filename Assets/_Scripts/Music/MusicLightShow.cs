using CAUnityFramework;
using CAUnityFramework.System.Audio;
using System.Collections.Generic;
using UnityEngine;

public class MusicLightShow : MonoBehaviour
{
	[Header("Audio")]
	[SerializeField] private FFTWindow window = FFTWindow.Blackman;
    [Header("Lighting")]
    [SerializeField] private float maxIntensity = 20f;

    [SerializeField] private List<FrequencyBandHandler> frequencyBandHandlers;

	private AudioSource audioSource;
	private readonly int sampleSize = 512;
	float[] samples;

	void Start()
	{
		this.samples = new float[this.sampleSize];
    }

	void Update()
    {
        if (PauseManager.IsPaused())
            return;

        if (this.audioSource == null || !this.audioSource.isPlaying)
        {
            AudioManager.AudioGroupDictionary.TryGetValue(AudioChannel.Music, out AudioGroup musicGroup);
            this.audioSource = musicGroup.GetPlayingAudioSource();
		}

        if (this.audioSource == null)
            return;

		this.audioSource.GetSpectrumData(this.samples, 0, this.window);

		foreach (var bandHandler in this.frequencyBandHandlers)
		{
            bandHandler.Update(this.samples, this.maxIntensity);
        }
    }
}
