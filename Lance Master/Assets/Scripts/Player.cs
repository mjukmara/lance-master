using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
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
	private CharacterController cc;
	private Transform tr;
	public GameObject arrow;

	private void Start()
	{
		cc = GetComponent<CharacterController>();
		tr = GetComponent<Transform>();
	}

	private void Update()
	{
		cc.SetMoveInput(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Right Bumper"))
		{
			cc.SetDashInput(true);

			float rz = Mathf.Atan2(cc.GetMoveDirection().y, cc.GetMoveDirection().x) * Mathf.Rad2Deg;
			Instantiate(arrow, tr.position, Quaternion.AngleAxis(rz, Vector3.forward));
		}
	}
}
