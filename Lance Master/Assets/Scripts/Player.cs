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
	private Transform tr;
	private Rigidbody2D rb;
	private Vector2 moveInputVector = Vector2.zero;
	private Vector2 moveDirection = Vector2.zero;
	private Vector2 dashInputVector = Vector2.zero;
	private Vector2 dashDirection = Vector2.zero;
	private Vector2 mouseInput = Vector2.zero;
	private bool dashInput = false;
	private float dashDamageCooldown = 0f;
	private float dashInvinsibleCooldown = 0f;
	private bool attacking = false;
	private bool invinsible = false;
	public GameObject arrow;

	private void Start()
	{
		tr = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		mouseInput = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		dashInputVector = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));

		UpdateMoveDirection();

		Debug.DrawLine(transform.position, transform.position + new Vector3(moveDirection.x, moveDirection.y), Color.blue);
		Debug.DrawLine(transform.position, transform.position + new Vector3(dashDirection.x, dashDirection.y), Color.red);

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Right Bumper"))
		{
			dashInput = true;

			float rz = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			Instantiate(arrow, tr.position, Quaternion.AngleAxis(rz, Vector3.forward));
		}

		if (dashDamageCooldown > 0) dashDamageCooldown -= Time.deltaTime;
		else dashDamageCooldown = 0;
		attacking = (dashDamageCooldown > 0);

		if (dashInvinsibleCooldown > 0) dashInvinsibleCooldown -= Time.deltaTime;
		else dashInvinsibleCooldown = 0;
		invinsible = (dashInvinsibleCooldown > 0);
	}

	private void FixedUpdate()
	{
		Move();

		if (dashInput)
		{
			Dash();
			dashInput = false;
		}
	}

	private void Move()
	{
		Vector2 velocity = rb.velocity;
		velocity += moveInputVector * moveAcceleration;
		rb.velocity = velocity;
		rb.drag = drag;
	}

	private void Dash()
	{
		if (dashInput)
		{
			Vector2 force = moveDirection * dashForce * 1000f;
			rb.AddForce(force);
			dashDamageCooldown = dashDamageDuration;
			dashInvinsibleCooldown = dashInvinsibleDuration;
		}
		dashInput = false;
	}

	private Vector2 GetDirectionByMouse()
	{
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 dir = Input.mousePosition - pos;
		dir.z = 0;
		return new Vector2(dir.x, dir.y).normalized;
	}

	private Vector2 GetDirectionByJoystick()
	{
		return moveDirection.normalized;
	}

	private void UpdateMoveDirection()
	{
		moveInputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		moveDirection = moveInputVector.normalized;
	}
}
