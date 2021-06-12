using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
	public float speed;
	public float delay;
	public float aliveTime;
	public GameObject PE_ArrowHit;

	private bool fired = false;
	private float timer;
	private Vector2 direction;
	private Transform tr;
	private Rigidbody2D rb;
	private BoxCollider2D boxCollider2D;
	private Vector2 playerPos;

	void Awake()
	{
		tr = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
		boxCollider2D = GetComponent<BoxCollider2D>();

		boxCollider2D.enabled = false;
		Destroy(gameObject, aliveTime);
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
			boxCollider2D.enabled = true;
			rb.velocity = transform.right * speed * Time.deltaTime;
			fired = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag != "Player")
		{
			Instantiate(PE_ArrowHit, tr.position, Quaternion.identity);
			Destroy(gameObject, 0);
		}
	}
}
