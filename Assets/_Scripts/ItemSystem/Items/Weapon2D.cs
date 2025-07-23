using UnityEngine;

namespace CAUnityFramework
{
    public class Weapon2D : MonoBehaviour
    {
		[Header("Weapon Stats")]
		[SerializeField] private float FireRate = 0.1f;
		[SerializeField] private float ExitVelocity = 20f;
		[SerializeField] private float Range = 100f;
		[SerializeField] private int MagazineCapacity = 30;
		[SerializeField] private float ReloadTime = 1f;

		[Header("Bullet")]
		[SerializeField] private GameObject bulletPrefab;
		[SerializeField] private int bulletPoolDefaultSize = 50;

		private bool fireHoldDown = false;
        private float fireCooldownLeft = 0f;

        void Start()
        {
            PoolManager.CreatePool(this.bulletPrefab, this.bulletPoolDefaultSize);
        }

		private void Update()
		{
            if (this.fireCooldownLeft > 0)
                this.fireCooldownLeft -= Time.deltaTime;

			if(this.fireHoldDown)
			{
				this.TryFireBullet();
			}
		}

		private void TryFireBullet()
		{
			if (this.CanFire())
			{
				this.FireBullet();
			}
		}

		protected virtual void FireBullet()
		{
			GameObject obj = PoolManager.Spawn(this.bulletPrefab, this.transform);
			this.StartCooldown();
		}

		private void StartCooldown()
		{
			this.fireCooldownLeft = this.FireRate;
		}

		private bool CanFire()
		{
			return this.fireCooldownLeft <= 0;
		}

		public void Hold()
        {
            this.fireHoldDown = true;
            this.TryFireBullet();
        }

        public void Release()
        {
            this.fireHoldDown = false;
        }
	}
}
