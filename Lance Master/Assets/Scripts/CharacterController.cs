using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float drag = 40f;
    public float moveAcceleration = 4f;
    Vector2 moveDirection = Vector2.zero;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
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

    public void SetMoveInput(Vector2 axis) {
        moveDirection = axis;
    }
}
