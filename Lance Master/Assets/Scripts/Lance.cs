using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lance : MonoBehaviour
{

	public float maxFlightTime = 0.5f;
	public bool flying = false;
	public bool stopped = false;
	public GameObject artPiece;
	private float velocity = 1f;
	private Vector2 direction = Vector2.zero;
	private float flightTime = 0f;
	public Animator playerTree;
	private Transform spearTransform;
	private LineRenderer lr;
	private Transform playerTr;
	private Transform lanceHandle;

	public delegate void OnLanceStoppedEventHandler(Lance lance);
	public event OnLanceStoppedEventHandler OnLanceStoppedEvent;

	private void Start()
	{
		spearTransform = gameObject.transform.Find("spear").transform;
		lr = gameObject.transform.Find("spear").GetComponent<LineRenderer>();
		lanceHandle = gameObject.transform.Find("spear").Find("Handle").transform;
		playerTr = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void FixedUpdate()
	{
		flightTime += Time.deltaTime;

		if (IsFlying())
		{
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			spearTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			lr.SetPosition(0, playerTr.position);
			lr.SetPosition(1, lanceHandle.position);
			artPiece.SetActive(true);
			playerTree.SetBool("Thrown", true);
		}
		else
		{
			artPiece.SetActive(false);
		}

		if (flying && !stopped && flightTime > maxFlightTime)
		{
			Stop();
			return;
		}

		Vector2 pos = new Vector2(transform.position.x, transform.position.y);
		pos += direction.normalized * velocity * Time.deltaTime;
		transform.position = pos;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("Lance collision with: " + other.gameObject.name);
		Stop();

		/*if (other.gameObject.tag == "Player") {
			Instantiate(PE_ArrowHit, tr.position, Quaternion.identity);
			Destroy(gameObject, 0);
			playerScript.Hit(damage);

			AudioManager.Instance.PlaySfx("crunch1");
		}*/
	}

	public void Stop()
	{
		velocity = 0f;
		flying = false;
		stopped = true;
		OnLanceStoppedEvent?.Invoke(this);
		playerTree.SetBool("Thrown", false);
	}

	public void Throw(float speed, Vector2 direction)
	{
		velocity = speed;
		this.direction = direction;
		flightTime = 0f;
		flying = true;
		stopped = false;
		transform.SetParent(null);
	}

	public bool IsFlying()
	{
		return flying && !stopped;
	}
}
