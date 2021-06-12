using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float fireRate = 1f;
    public float reloadRate = 0.2f;
    public int magazineSize = 5;
    public float inaccuracy = 5f;
    public Magazine magazine;

    private bool trigger = false;
    private bool reloading = false;
    public float fireCooldown = 0f;
    public float reloadCooldown = 0f;

    private void Awake() {

    }

    private void Update() {
        fireCooldown = Mathf.Max(0, fireCooldown - Time.deltaTime);

        if (IsReloading()) {
            reloadCooldown = Mathf.Max(0, reloadCooldown - Time.deltaTime);
            if (reloadCooldown == 0) {
                magazine.Renew();
                reloading = false;
            }
        } else if (magazine.IsEmpty()) {
            Reload();
        } else if (IsTriggerDown() && !IsCooldown()) {
            Fire();
        }
    }

    public void Aim(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Fire() {
        if (magazine.IsEmpty()) return;

        SetCooldown(1f / fireRate);

        GameObject projectilePrefab = magazine.PopProjectile();
        Quaternion projectileRotation = transform.rotation;
        projectileRotation *= Quaternion.Euler(0, 0, Random.Range(-inaccuracy, inaccuracy));
        GameObject projectile = Instantiate(projectilePrefab, transform.position, projectileRotation);
        Debug.Log("Fire");
    }

    public void SetFireTrigger(bool pressed) {
        trigger = pressed;
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

    public void SetCooldown(float seconds) {
        fireCooldown = seconds;
    }

    public void Reload() {
        reloadCooldown = 1f / reloadRate;
        reloading = true;
    }

    public bool IsReloading() {
        return reloading;
    }
}
