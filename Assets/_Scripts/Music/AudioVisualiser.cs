using UnityEngine;

public class AudioVisualiser : MonoBehaviour
{
    public bool UseCachedRectTransform = true;

    [Header("Audio")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private AudioSource audioSource;
    private readonly int sampleSize = 512;
    float[] samples;
    RectTransform[] sampleObjectsRect;

    [SerializeField] private FFTWindow window = FFTWindow.Blackman;

    void Start()
    {
        this.samples = new float[this.sampleSize];

        this.sampleObjectsRect = new RectTransform[this.sampleSize];

		for (int i = 0; i < this.sampleSize; i++)
		{
            this.sampleObjectsRect[i] = Instantiate(this.prefab, this.transform).GetComponent<RectTransform>();
            this.sampleObjectsRect[i].anchoredPosition = new Vector3(-(this.sampleSize - i), 0, 0);
        }
    }

    void Update()
    {
        this.audioSource.GetSpectrumData(this.samples, 0, this.window);

		for (int i = 0; i < this.sampleSize; i++)
		{
            if(this.UseCachedRectTransform)
                this.sampleObjectsRect[i].sizeDelta = new Vector2(1, this.samples[i] * 3000f);
		}
    }
}
