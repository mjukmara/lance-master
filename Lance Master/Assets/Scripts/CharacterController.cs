using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float spass = 0.6f;
    public float deadZone = 0.1f;
    public float drag = 40f;
    public float moveAcceleration = 4f;
    private Vector2 perlinSeed;
    Vector2 moveDirection = Vector2.zero;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        perlinSeed = new Vector2(Random.Range(0, 10000), Random.Range(0, 10000));
    }

    private void Update() {
        UpdateMoveDirection();

        Debug.DrawLine(transform.position, transform.position + new Vector3(moveDirection.x, moveDirection.y), Color.blue);
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        Vector2 velocity = rb.velocity;
        velocity += moveDirection * moveAcceleration;
        rb.velocity = velocity;
        rb.drag = drag;
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
        moveDirection = new Vector2(x, y);
    }
}
