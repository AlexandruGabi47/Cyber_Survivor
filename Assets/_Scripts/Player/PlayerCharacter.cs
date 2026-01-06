using CAUnityFramework;
using CAUnityFramework.System.Audio;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private List<AudioClip> hurtSounds;
    //Temp
    [SerializeField] private AudioClip backgroundMusic;

    private Health health;

    void Start()
    {
        this.health = this.GetComponent<Health>();
        this.health.OnHit.AddListener(this.OnHit);
        AudioManager.PlaySound(AudioChannel.Music, 
            new AudioPlayRequest(
	            clip: this.backgroundMusic,
                volume: 0.5f,
	            loop: true,
	            fadeIn: true,
	            fadeDuration: 2f));
	}

    private void OnHit()
    {
        AudioClip clip = this.hurtSounds[Random.Range(0, this.hurtSounds.Count)];
        AudioSource.PlayClipAtPoint(clip, this.transform.position);
		AudioManager.PlaySound(AudioChannel.Voice, new AudioPlayRequest(clip));
    }
}
