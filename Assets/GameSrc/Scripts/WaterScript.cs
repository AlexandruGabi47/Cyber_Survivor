using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WaterScript : MonoBehaviour
{
	SpriteRenderer sr;
	float flag;
	// Use this for initialization
	void Start()
	{
		flag = 0.1f;
		sr = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		//Animates the water tile
		sr.size = new Vector2(sr.size.x + flag, sr.size.y);
		if (sr.size.x > 40.0f) flag = -0.05f;
		if (sr.size.x < 24.0f) flag = 0.05f;
	}
	void OnTriggerEnter2D(Collider2D coll)
	{
		CharacterScript player = coll.GetComponent<CharacterScript>();
		if (player)
		{
			StartCoroutine(KillCharacter(player));
		}
	}
	IEnumerator KillCharacter(CharacterScript player)
	{
		yield return new WaitForSeconds(0.5f);
		player.Kill();
	}
}
