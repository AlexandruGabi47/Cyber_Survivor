using CAUnityFramework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMB : MonoBehaviour
{
    [SerializeField] private List<AudioClip> hurtSounds;

    private Health health;

    void Start()
    {
        this.health = this.GetComponent<Health>();
        this.health.OnHit.AddListener(this.OnHit);
	}

    private void OnHit()
    {
        AudioClip clip = this.hurtSounds[Random.Range(0, this.hurtSounds.Count)];
        AudioSource.PlayClipAtPoint(clip, this.transform.position);
    }
}
