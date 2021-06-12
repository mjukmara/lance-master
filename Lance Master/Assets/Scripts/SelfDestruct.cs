using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
	public float delay = 2.0f;
	void Start()
	{
		Destroy(gameObject, delay);
	}
}
