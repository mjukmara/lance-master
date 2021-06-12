using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	public float speed;
	public float aliveTime = 2.0f;

	private Rigidbody2D rb;
	private Transform tr;
	public GameObject PE_RedLaserHit;


	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		tr = GetComponent<Transform>();
		rb.velocity = transform.right * speed;
		Destroy(gameObject, aliveTime);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Instantiate(PE_RedLaserHit, tr.position, Quaternion.identity);
			Destroy(gameObject, 0);
		}
	}
}
