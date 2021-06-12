using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
    public int health = 10;
    public Weapon weaponPrefab;
    public float spass = 0.6f;
    public float deadZone = 0.1f;
    public float chaseFactor = 0.5f;
    private CharacterController cc;
    private Transform target = null;
    private float targetDistance = 0f;
    private Vector2 targetDirection = Vector2.zero;
    private Vector2 perlinSeed;
    private Weapon weapon;
    private float waitOnSpawnCooldown = 1f;

    private void Awake() {
        cc = GetComponent<CharacterController>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        perlinSeed = new Vector2(Random.Range(0, 10000), Random.Range(0, 10000));
        if (weaponPrefab) {
            weapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
            weapon.transform.SetParent(transform);
        }
    }

    private void Update() {
        waitOnSpawnCooldown = Mathf.Max(0, waitOnSpawnCooldown - Time.deltaTime);
        if (waitOnSpawnCooldown > 0) return;

        UpdateMoveDirection();
        UpdateTargeting();
        Debug.DrawLine(transform.position, transform.position + new Vector3(targetDirection.x, targetDirection.y), Color.red);
    }

    private void FixedUpdate() {

    }

    private void UpdateTargeting() {
        if (!target) {
            weapon.SetFireTrigger(false);
            return;
        }

        targetDistance = (target.position - transform.position).magnitude;
        targetDirection = (target.position - transform.position).normalized;
        if (weapon) {
            weapon.Aim(targetDirection);
            weapon.SetFireTrigger(true);
        }
    }

    private void UpdateMoveDirection() {
        float x = Mathf.PerlinNoise(perlinSeed.x + Time.time * spass, 0f) * 2f - 1f;
        float y = Mathf.PerlinNoise(0f, perlinSeed.x + Time.time * spass) * 2f - 1f;

        Vector2 moveDir = new Vector2(x, y);

        moveDir += targetDirection * chaseFactor;
        
        if (moveDir.magnitude < deadZone) moveDir = Vector2.zero;

        cc.SetMoveInput(moveDir);
    }
    public void Hit(int damage) {
        health = Mathf.Max(0, health - damage);

        string[] soundNames = new string[] { "mob-rip1", "mob-rip2", "mob-rip3", "mob-rip4", "mob-rip5", "mob-rip6", "mob-rip7", "mob-rip8", "mob-rip9" };
        AudioManager.Instance.PlaySfx(soundNames[Random.Range(0, soundNames.Length)]);

        if (health == 0) {
            Destroy(gameObject);
        }
    }
}
