using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupScript : MonoBehaviour
{
	public GameObject pickupEffect;

	void Start()
	{

	}

	void Update()
	{

	}

	void OnDestroy()
	{
		Instantiate(pickupEffect, gameObject.transform.position, Quaternion.identity);
	}
}
