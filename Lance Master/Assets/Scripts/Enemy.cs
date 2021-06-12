using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
    public Weapon weaponPrefab;
    public float spass = 0.6f;
    public float deadZone = 0.1f;
    public float chaseFactor = 0.5f;
    private CharacterController cc;
    private Transform target = null;
    private Vector2 targetDirection = Vector2.zero;
    private Vector2 perlinSeed;
    private Weapon weapon;

    private void Awake() {
        cc = GetComponent<CharacterController>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        perlinSeed = new Vector2(Random.Range(0, 10000), Random.Range(0, 10000));
        weapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        weapon.transform.SetParent(transform);
    }

    private void Update() {
        UpdateMoveDirection();
        UpdateTargeting();
        Debug.DrawLine(transform.position, transform.position + new Vector3(targetDirection.x, targetDirection.y), Color.red);
    }

    private void FixedUpdate() {

    }

    private void UpdateTargeting() {
        targetDirection = (target.position - transform.position).normalized;
        if (weapon) {
            weapon.Aim(targetDirection);
            weapon.SetFireTrigger(true);
        }
    }

    private void UpdateMoveDirection() {
        float x = Mathf.PerlinNoise(perlinSeed.x + Time.time * spass, 0f);
        float y = Mathf.PerlinNoise(0f, perlinSeed.x + Time.time * spass);

        Vector2 moveDir = new Vector2(x, y);

        moveDir += targetDirection * chaseFactor;

        if (moveDir.x > 0.5f + deadZone) moveDir.x = 1f;
        else if (moveDir.x < 0.5f - deadZone) moveDir.x = 0f;
        else moveDir.x = 0.5f;

        if (moveDir.y > 0.5f + deadZone) moveDir.y = 1f;
        else if (moveDir.y < 0.5f - deadZone) moveDir.y = 0f;
        else moveDir.y = 0.5f;

        moveDir.x -= 0.5f;
        moveDir.y -= 0.5f;
        moveDir.x *= 2f;
        moveDir.y *= 2f;

        cc.SetMoveInput(moveDir);
    }
}
