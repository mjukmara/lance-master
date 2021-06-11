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
    public float drag = 20f;
    private Rigidbody2D rb;
    private Vector2 rawInput = Vector2.zero;
    private Vector2 mouseInput = Vector2.zero;
    private bool dashInput = false;
    private float dashCooldownRemaining = 0f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        mouseInput = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        rawInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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
        velocity += rawInput * moveAcceleration;
        velocity = Vector2.ClampMagnitude(velocity, moveMaxSpeed);
        rb.velocity = velocity;
        rb.drag = drag;
    }

    private void HandleDash() {
        if (dashInput) {
            Debug.Log(Time.time + ": Dash!");
            gameObject.transform.position = CalcDashTargetPosition();
        }
        dashInput = false;
    }

    private Vector2 CalcDashTargetPosition() {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        return pz;
    }
}
