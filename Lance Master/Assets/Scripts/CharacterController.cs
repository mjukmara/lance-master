using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    public float drag = 40f;
    public float moveAcceleration = 4f;
    public float dashForce = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput = Vector2.zero;
    private Vector2 moveDirection = Vector2.zero;
    private bool dashInput = false;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Debug.DrawLine(transform.position, transform.position + new Vector3(moveDirection.x, moveDirection.y), Color.blue);
    }

    private void FixedUpdate() {
        Move();

        if (dashInput) {
            rb.AddForce(moveDirection * dashForce * 1000f);
        }
        dashInput = false;
    }

    private void Move() {
        Vector2 velocity = rb.velocity;
        velocity += moveInput * moveAcceleration;
        rb.velocity = velocity;
        rb.drag = drag;
    }

    public void SetDashInput(bool dashInput) {
        this.dashInput = dashInput;
    }

    public void SetMoveInput(Vector2 axis) {
        moveInput = axis;
        moveDirection = moveInput.normalized;
    }

    public Vector2 GetMoveDirection() {
        return moveDirection;
    }
}
