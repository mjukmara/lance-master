using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
	[Range(0.1f, 2.0f)]
	public float magnitude;
	private Transform tr;
	private Transform player;

	void Start()
	{
		tr = GetComponent<Transform>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
	{
		tr.position = player.position * magnitude;
	}
}
