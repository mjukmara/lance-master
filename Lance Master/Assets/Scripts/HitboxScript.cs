using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
	public GameObject hitEffect;
	void Awake()
	{
		Destroy(gameObject, 0.2f);
	}

	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Instantiate(hitEffect, transform.position, Quaternion.identity);
			Debug.Log("Hit with sword!");
		}
	}
}
