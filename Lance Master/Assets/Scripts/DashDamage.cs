using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDamage : MonoBehaviour
{
    public int damage = 25;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Dash collision with: " + other.gameObject.name);
        GameObject otherGo = other.gameObject;
        if (otherGo.tag == "Enemy") {
            Debug.Log("Trigger enter!");
            Enemy enemy = otherGo.GetComponent<Enemy>();
            enemy.Hit(damage);
        }
    }
}
