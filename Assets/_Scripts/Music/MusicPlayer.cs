using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	[SerializeField] private AudioSource musicSource;
	[SerializeField] private AudioSource musicSourceAlt;
	[SerializeField] private float crossfadeDuration = 2f;

	private AudioSource currentSource;
	private AudioSource nextSource;
	private float crossfadeTimer = 0f;
	private bool isCrossfading = false;

	private void Awake()
	{
		currentSource = musicSource;
		nextSource = musicSourceAlt;
	}

	private void Update()
	{
		if (isCrossfading)
		{
			crossfadeTimer += Time.deltaTime;
			float t = crossfadeTimer / crossfadeDuration;
			currentSource.volume = Mathf.Lerp(1f, 0f, t);
			nextSource.volume = Mathf.Lerp(0f, 1f, t);
			if (t >= 1f)
			{
				isCrossfading = false;
				var temp = currentSource;
				currentSource = nextSource;
				nextSource = temp;
			}
		}
	}
	public void PlayMusic(AudioClip newClip)
	{
		if (currentSource.clip == newClip) return;
		nextSource.clip = newClip;
		nextSource.volume = 0f;
		nextSource.Play();
		crossfadeTimer = 0f;
		isCrossfading = true;
	}
}