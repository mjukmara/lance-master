using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
	public float speed;
	public float delay;

	private bool fired = false;
	private float timer;
	private Vector2 direction;
	private Transform tr;
	private Rigidbody2D rb;

	void Awake()
	{
		tr = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();

		GameObject player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		if (timer < delay)
		{
			timer += Time.deltaTime;
			return;
		}

		if (!fired)
		{
			Vector3 euler = tr.rotation.eulerAngles;
			Debug.Log(euler);
			rb.velocity = euler * speed;
			fired = true;
		}
	}
}
