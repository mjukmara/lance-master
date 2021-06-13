using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPickup : MonoBehaviour
{
	public GameObject pickupEffect;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Instantiate(pickupEffect, gameObject.transform.position, Quaternion.identity);
			Game.Instance.FinishGame();
		}
	}
}
