using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletScript : MonoBehaviour
{
	float Speed;
	Rigidbody2D rb;

	void Start()
	{
		Speed = 30;
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		if (transform.position.x > 100 || transform.position.x < -100)
			Destroy(this.gameObject);
		rb.linearVelocity = Speed * ((transform.rotation.y != 0.0f) ? Vector2.left : Vector2.right);
	}
	private void OnTriggerEnter2D(Collider2D coll)
	{
		ZombieScript zombie = coll.GetComponent<ZombieScript>();
		if(zombie)
		{
			StartCoroutine(KillCharacter(zombie));
		}
	}
	IEnumerator KillCharacter(ZombieScript zombie)
	{
		yield return new WaitForSeconds(0.025f);
		zombie.Kill();
		Destroy(this.gameObject);
	}
}
