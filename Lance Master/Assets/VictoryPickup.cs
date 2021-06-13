using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPickup : MonoBehaviour
{
	public GameObject pickupEffect;
	public float delay = 1.0f;
	bool pickup = false;

	private void Update()
	{
		if (pickup && delay > 0)
		{
			delay -= Time.deltaTime;
			//Game.Instance;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Instantiate(pickupEffect, gameObject.transform.position, Quaternion.identity);

		}
	}
}
