using System.Collections;
using UnityEngine;
using CAUnityFramework;

public class PistolController : Weapon2D
{
    [Header("Muzzle Flash")]
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private float _flashDuration = 0.1f;

    public void ShowFlash()
    {
        if (this._muzzleFlash == null) return;
        this.StopAllCoroutines();
        this.StartCoroutine(this.Flash());
    }

    private IEnumerator Flash()
    {
        this._muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(this._flashDuration);
        this._muzzleFlash.SetActive(false);
    }

	protected override void FireBullet()
    {
        base.FireBullet();
        this.ShowFlash();
    }
}
