using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lance : MonoBehaviour {

	public float maxFlightTime = 0.5f;
	public bool flying = false;
	public bool stopped = false;
	public GameObject artPiece;
	private float velocity = 1f;
	private Vector2 direction = Vector2.zero;
	private float flightTime = 0f;

	public delegate void OnLanceStoppedEventHandler(Lance lance);
	public event OnLanceStoppedEventHandler OnLanceStoppedEvent;

	private void FixedUpdate() {
		flightTime += Time.deltaTime;

		if (IsFlying()) {
			artPiece.SetActive(true);
		} else {
			artPiece.SetActive(false);
		}

		if (flying && !stopped && flightTime > maxFlightTime) {
			Stop();
			return;
		}

		Vector2 pos = new Vector2(transform.position.x, transform.position.y);
		pos += direction.normalized * velocity * Time.deltaTime;
        transform.position = pos;
	}

	void OnCollisionEnter2D(Collision2D other) {
		Debug.Log("Collision with: " + other);
		Stop();

		/*if (other.gameObject.tag == "Player") {
			Instantiate(PE_ArrowHit, tr.position, Quaternion.identity);
			Destroy(gameObject, 0);
			playerScript.Hit(damage);

			AudioManager.Instance.PlaySfx("crunch1");
		}*/
	}

	public void Stop() {
		velocity = 0f;
		flying = false;
		stopped = true;
		OnLanceStoppedEvent?.Invoke(this);
	}

	public void Throw(float speed, Vector2 direction) {
		velocity = speed;
		this.direction = direction;
		flightTime = 0f;
		flying = true;
		stopped = false;
		transform.SetParent(null);
	}

	public bool IsFlying() {
		return flying && !stopped;
	}
}
