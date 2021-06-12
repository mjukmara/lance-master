using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float moveAcceleration = 4f;
    public float moveMaxSpeed = 4f;
    public float dashCooldown = 1f;
    public float dashMaxLength = 5f;
    public float dashForce = 5f;
    public float drag = 40f;
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
        dashInputVector = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));

        //Debug.Log("Horizontal: " + Input.GetAxisRaw("Horizontal") + " - " + Input.GetButtonDown("Horizontal"));
        //Debug.Log("Vertical: " + Input.GetAxisRaw("Vertical") + " - " + Input.GetButtonDown("Vertical"));
        //Debug.Log("Horizontal2: " + Input.GetAxisRaw("Horizontal2") + " - " + Input.GetButtonDown("Horizontal2"));
        //Debug.Log("Vertical2: " + Input.GetAxisRaw("Vertical2") + " - " + Input.GetButtonDown("Vertical2"));
        //Debug.Log("Left Bumper: " + Input.GetAxisRaw("Left Bumper") + " - " + Input.GetButton("Left Bumper"));
        //Debug.Log("Right Bumper: " + Input.GetAxisRaw("Right Bumper") + " - " + Input.GetButton("Right Bumper"));
        //Debug.Log("L2: " + Input.GetAxisRaw("L2"));
        //Debug.Log("R2: " + Input.GetAxisRaw("R2"));

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Right Bumper")) {
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
            // Vector2 dashDirection = GetDashDirectionByMouse();
            Vector2 dashDirection = GetDashDirectionByJoystick();
            Vector2 force = dashDirection * dashForce * 1000f;
            rb.AddForce(force);
            dashDamageCooldown = dashDamageDuration;
            dashInvinsibleCooldown = dashInvinsibleDuration;
        }
        dashInput = false;
    }

    private Vector2 GetDashDirectionByMouse() {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        dir.z = 0;
        return new Vector2(dir.x, dir.y).normalized;
    }

    private Vector2 GetDashDirectionByJoystick() {
        return dashInputVector.normalized;
    }
}
