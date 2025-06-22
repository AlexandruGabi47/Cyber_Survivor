using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterScript : MonoBehaviour
{
	//
	Rigidbody2D rb;
	int HP; 
	//
	public float maxSpeed;
	public float jumpForce;
	//
	float speed;
	float direction;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		//
		HP = 1;
		maxSpeed = 15;
		jumpForce = 26;
		//
		speed = 0;
		direction = 0;
	}

	private void FixedUpdate()
	{
		/*Input*/
		if (Input.GetKeyDown(KeyCode.W)) Jump();
		direction = Input.GetAxis("Horizontal");
		Move();
		//Fire
		if (Input.GetKeyDown(KeyCode.Space)) Fire();
		//Checks if it exceeds the maxSpeed or the jumpForce
		if (rb.linearVelocity.x > maxSpeed)		
			rb.linearVelocity = new Vector2(maxSpeed, rb.linearVelocity.y);		
		if (rb.linearVelocity.y > jumpForce)
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
	}

	public void Move()
	{
		//Rotates the character in the direction he is moving
		if (direction < 0) transform.rotation = new Quaternion(0, 180, 0, 0);
		if (direction > 0) transform.rotation = new Quaternion(0, 0, 0, 0);
		//
		rb.linearVelocity = new Vector2(direction * maxSpeed, rb.linearVelocity.y);
	}

	void Update()
	{
		if (HP == 0) Kill();
	}

	void Jump()
	{
		if (rb.linearVelocity.y == 0)
		{
			rb.AddForce(new Vector2(0, jumpForce * 100));
		}
	}

	void Fire()
	{
		Instantiate((GameObject)Resources.Load("Prefabs/Bullet"), rb.transform.position, transform.rotation);
	}

	public void Kill()
	{
		rb.linearVelocity = new Vector2(0, 0);
		transform.position = new Vector3(0, 0, 0);
	}
}
