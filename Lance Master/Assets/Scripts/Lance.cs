using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lance : MonoBehaviour {

    private void FixedUpdate() {
        Vector3 pos = transform.position;
        pos += transform.right * Time.deltaTime;
        transform.position = pos;
	}

	void OnCollisionEnter2D(Collision2D other) {
		Debug.Log("Collision with: " + other);
		/*if (other.gameObject.tag == "Player") {
			Instantiate(PE_ArrowHit, tr.position, Quaternion.identity);
			Destroy(gameObject, 0);
			playerScript.Hit(damage);

			AudioManager.Instance.PlaySfx("crunch1");
		}*/
	}
}
