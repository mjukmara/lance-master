using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float moveAcceleration = 1f;
    public float moveMaxSpeed = 4f;
    public float dashCooldown = 1f;
    public float dashMaxLength = 5f;
    public float dashForce = 20f;
    public float drag = 20f;
    private Rigidbody2D rb;
    private Vector2 moveInputVector = Vector2.zero;
    private Vector2 dashInputVector = Vector2.zero;
    private Vector2 mouseInput = Vector2.zero;
    private bool dashInput = false;
    private float dashCooldownRemaining = 0f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        mouseInput = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        moveInputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space)) {
            dashInput = true;
        }
    }

    private void FixedUpdate() {
        HandleMove();
        HandleDash();
    }

    private void HandleMove() {
        Vector2 velocity = rb.velocity;
        velocity += moveInputVector * moveAcceleration;
        rb.velocity = velocity;
        rb.drag = drag;
    }

    private void HandleDash() {
        if (dashInput) {
            Vector2 dashDirection = GetDashDirection();
            Vector2 force = dashDirection * dashForce * 1000f;
            rb.AddForce(force);
        }
        dashInput = false;
    }

    private Vector2 GetDashDirection() {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        dir.z = 0;
        return new Vector2(dir.x, dir.y).normalized;
    }
}
