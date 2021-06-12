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
	private LineRenderer lr;
	private GameObject player;

	void Awake()
	{
		tr = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
		boxCollider2D = GetComponent<BoxCollider2D>();
		lr = GetComponent<LineRenderer>();
		player = GameObject.FindGameObjectWithTag("Player");

		boxCollider2D.enabled = false;
		Destroy(gameObject, aliveTime);
	}

	void Update()
	{
		if (timer < delay)
		{
			timer += Time.deltaTime;
			//float ratio = timer / delay;
			//tr.position -= transform.right * ratio * Time.deltaTime * 10;
			return;
		}

		if (!fired)
		{
			boxCollider2D.enabled = true;
			fired = true;
		}

		Vector2 d = player.transform.position - tr.position;
		float atp = Mathf.Atan2(d.y, d.x);

		rb.velocity = transform.right * speed;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Instantiate(PE_ArrowHit, tr.position, Quaternion.identity);
			Destroy(gameObject, 0);
		}
	}
}
