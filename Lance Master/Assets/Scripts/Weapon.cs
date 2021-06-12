using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float fireRate = 1f;
    
    private bool shooting = false;

    private void Update() {
        
    }

    public void Aim(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Shoot(bool shooting) {
        this.shooting = shooting;
    }
}
