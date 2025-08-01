using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class FrequencyBandHandler
{
    [SerializeField] private string bandName;
    [SerializeField] private int sampleMin = 0;
    [SerializeField] private int sampleMax = 3;
    [SerializeField] private float intensityBoost = 10f;
    [SerializeField] private float intensityPower = 2f;
    [SerializeField] private Light2D[] lights;

    public void Update(float[] samples, float maxIntensity)
	{
        float bandMagnitude = this.GetAverage(samples, this.sampleMin, this.sampleMax);

        bandMagnitude = Mathf.Clamp(bandMagnitude * Mathf.Pow(this.intensityBoost, this.intensityPower), 0, maxIntensity);

        foreach (var light in this.lights)
        {
            light.intensity = bandMagnitude;
        }
    }

    float GetAverage(float[] data, int start, int end)
    {
        float sum = 0;
        for (int i = start; i < end; i++)
        {
            sum += data[i];
        }
        return sum / (end - start);
    }
}
