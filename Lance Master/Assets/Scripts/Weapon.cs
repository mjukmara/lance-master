using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float fireRate = 1f;
    
    private bool trigger = false;
    private float fireCooldown = 0f;

    private void Update() {
        fireCooldown = Mathf.Min(0, fireCooldown - Time.deltaTime);

        if (IsTriggerDown() && !IsCooldown()) {
            
        }
    }

    public void Aim(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void SetTrigger(bool trigger) {
        this.trigger = trigger;
    }

    public bool IsTriggerDown() {
        return trigger;
    }
    public bool IsTriggerUp() {
        return !IsTriggerDown();
    }

    public bool IsCooldown() {
        return fireCooldown > 0;
    }
}
