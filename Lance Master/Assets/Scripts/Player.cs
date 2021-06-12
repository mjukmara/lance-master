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
    public float dashDamageDuration = 1f;
    public float dashInvinsibleDuration = 1f;
    private Rigidbody2D rb;
    private Vector2 moveInputVector = Vector2.zero;
    private Vector2 dashInputVector = Vector2.zero;
    private Vector2 mouseInput = Vector2.zero;
    private bool dashInput = false;
    private float dashDamageCooldown = 0f;
    private float dashInvinsibleCooldown = 0f;
    private bool attacking = false;
    private bool invinsible = false;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        mouseInput = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        moveInputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Debug.Log("L1: " + Input.GetAxisRaw("L1") + " - " + Input.GetButtonDown("L1"));
        Debug.Log("R1: " + Input.GetAxisRaw("R1") + " - " + Input.GetButtonDown("R1"));
        //Debug.Log("L2: " + Input.GetAxisRaw("L2"));
        //Debug.Log("R2: " + Input.GetAxisRaw("R2"));

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("Fire1")) {
            dashInput = true;
        }

        if (dashDamageCooldown > 0) dashDamageCooldown -= Time.deltaTime;
        else dashDamageCooldown = 0;
        attacking = (dashDamageCooldown > 0);

        if (dashInvinsibleCooldown > 0) dashInvinsibleCooldown -= Time.deltaTime;
        else dashInvinsibleCooldown = 0;
        invinsible = (dashInvinsibleCooldown > 0);
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
            dashDamageCooldown = dashDamageDuration;
            dashInvinsibleCooldown = dashInvinsibleDuration;
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
