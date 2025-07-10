using UnityEngine;
using CAUnityFramework;

public class PistolController : Weapon2D
{
    [Header("Muzzle Flash")]
    public GameObject MuzzleFlash;
    public float FlashDuration = 0.1f;

    public void ShowFlash()
    {
        if (this.MuzzleFlash == null) return;
        this.StopAllCoroutines();
        this.StartCoroutine(this.Flash());
    }

    private System.Collections.IEnumerator Flash()
    {
        this.MuzzleFlash.SetActive(true);
        yield return new WaitForSeconds(this.FlashDuration);
        this.MuzzleFlash.SetActive(false);
    }

	protected override void FireBullet()
    {
        base.FireBullet();
        this.ShowFlash();
    }
}
