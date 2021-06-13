using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallCollider : MonoBehaviour
{
	public delegate void OnWallCollisionEventHandler();
	public event OnWallCollisionEventHandler OnWallCollisionEvent;

	void OnCollisionEnter2D(Collision2D other) {
		if (other.collider.gameObject.layer == LayerMask.NameToLayer("Walls")) {
			Debug.Log("Player collided with a wall");
			OnWallCollisionEvent?.Invoke();
		}
	}
}
