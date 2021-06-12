using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
	public float speed;
	public float delay;
	public float aliveTime;
	public GameObject PE_ArrowHit;
	[Range(0.0f, 1.0f)]
	public float attractionTime;
	public float rotationSpeed;

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
		lr = gameObject.transform.Find("ArrowSprite").Find("Chain").GetComponent<LineRenderer>();
		player = GameObject.FindGameObjectWithTag("Player");

		boxCollider2D.enabled = false;
		Destroy(gameObject, aliveTime);
	}

	void Update()
	{
		timer += Time.deltaTime;
		if (timer < delay) { return; }

		if (timer < delay + attractionTime)
		{
			lr.enabled = true;
			Vector3 dir = player.transform.position - transform.position;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * rotationSpeed);
		}
		else
		{
			lr.enabled = false;
		}

		if (!fired)
		{
			boxCollider2D.enabled = true;
			fired = true;
		}

		lr.SetPosition(0, tr.position);
		lr.SetPosition(1, player.transform.position);
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
