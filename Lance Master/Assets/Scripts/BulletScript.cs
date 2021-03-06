using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	public int damage = 10;
	public float speed;
	public float aliveTime = 2.0f;

	private Rigidbody2D rb;
	private Transform tr;
	public GameObject hitEffect;


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
			Instantiate(hitEffect, tr.position, Quaternion.identity);
			Destroy(gameObject, 0);

			Player playerScript = other.gameObject.GetComponent<Player>();
			playerScript.Hit(damage);

			AudioManager.Instance.PlaySfx("impact1");
		}
	}
}
