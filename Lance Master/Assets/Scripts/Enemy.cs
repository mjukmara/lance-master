using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
    public float spass = 0.6f;
    public float deadZone = 0.1f;
    private CharacterController cc;
    private Transform target = null;
    private Vector2 targetDirection = Vector2.zero;
    private Vector2 perlinSeed;

    private void Awake() {
        cc = GetComponent<CharacterController>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        perlinSeed = new Vector2(Random.Range(0, 10000), Random.Range(0, 10000));
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
    }

    private void UpdateMoveDirection() {
        float x = Mathf.PerlinNoise(perlinSeed.x + Time.time * spass, 0f);
        float y = Mathf.PerlinNoise(0f, perlinSeed.x + Time.time * spass);

        if (x > 0.5f + deadZone) x = 1f;
        else if (x < 0.5f - deadZone) x = 0f;
        else x = 0.5f;

        if (y > 0.5f + deadZone) y = 1f;
        else if (y < 0.5f - deadZone) y = 0f;
        else y = 0.5f;

        x -= 0.5f;
        y -= 0.5f;
        x *= 2f;
        y *= 2f;

        cc.SetMoveInput(new Vector2(x, y));
    }
}
