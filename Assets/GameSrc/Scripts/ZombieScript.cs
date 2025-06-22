using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
	int HP;
	void Start()
	{
		HP = 2;
	}
	void Update()
	{
		if (HP == 0) Kill();
	}
	void FixedUpdate()
	{
		
	}
	public void Kill()
	{
		Destroy(this.gameObject);
	}
}
