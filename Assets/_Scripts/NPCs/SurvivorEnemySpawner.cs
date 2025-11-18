using UnityEngine;

public class SurvivorEnemySpawner : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private float spawnRadius = 15f; // Distance from player to spawn
	[SerializeField] private int enemiesPerWave = 3;
	[SerializeField] private float waveInterval = 5f;
	[SerializeField] private Transform targetToSpawnAround;

	private float waveTimer;

	void Start()
	{
		this.waveTimer = this.waveInterval;
		if (this.targetToSpawnAround == null && Camera.main != null)
			this.targetToSpawnAround = Camera.main.transform;
	}

	void Update()
	{
		this.waveTimer -= Time.deltaTime;
		if (this.waveTimer <= 0f)
		{
			this.SpawnWave();
			this.waveTimer = this.waveInterval;
		}
	}

	private void SpawnWave()
	{
		if (this.enemyPrefab == null || this.targetToSpawnAround == null)
			return;

		for (int i = 0; i < this.enemiesPerWave; i++)
		{
			Vector2 spawnPos = this.GetSpawnPosition();
			GameObject obj = Instantiate(this.enemyPrefab, spawnPos, Quaternion.identity, this.transform);
			obj.GetComponent<ChaserAI>()?.SetTarget(this.targetToSpawnAround);
		}
	}

	private Vector2 GetSpawnPosition()
	{
		// Try up to 10 times to find a valid spawn position
		for (int attempt = 0; attempt < 10; attempt++)
		{
			float angle = Random.Range(0f, Mathf.PI * 2f);
			Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * this.spawnRadius;
			Vector2 playerPos = this.targetToSpawnAround.position;
			Vector2 spawnPos = playerPos + offset;

			// Check for colliders at the spawn position (assuming walls are on Default layer or use a LayerMask)
			Collider2D hit = Physics2D.OverlapCircle(spawnPos, 0.5f, LayerMask.GetMask("Default"));
			if (hit == null)
			{
				return spawnPos;
			}
		}
		// Fallback: just return a random position if all attempts fail
		float fallbackAngle = Random.Range(0f, Mathf.PI * 2f);
		Vector2 fallbackOffset = new Vector2(Mathf.Cos(fallbackAngle), Mathf.Sin(fallbackAngle)) * this.spawnRadius;
		return (Vector2)this.targetToSpawnAround.position + fallbackOffset;
	}
}
