using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
	public int health = 100;
	public float moveAcceleration = 4f;
	public float moveMaxSpeed = 4f;
	public float dashCooldown = 1f;
	public float dashMaxLength = 5f;
	public float dashForce = 5f;
	public float drag = 40f;
	public float dashDamageDuration = 1f;
	public float dashInvinsibiltyDuration = 0.2f;
	public float dashInvinsibiltyCooldown = 0f;
	private CharacterController cc;
	private CapsuleCollider2D capsuleCollider;
	private Transform tr;
	public GameObject arrow;

	private void OnEnable()
	{
		cc.OnDashEvent += OnDash;

	}

	private void OnDisable()
	{
		cc.OnDashEvent -= OnDash;
	}

	private void Awake()
	{
		cc = GetComponent<CharacterController>();
		tr = GetComponent<Transform>();
		capsuleCollider = GetComponent<CapsuleCollider2D>();
	}

	private void Update()
	{
		dashInvinsibiltyCooldown = Mathf.Max(0, dashInvinsibiltyCooldown-Time.deltaTime);
		if (dashInvinsibiltyCooldown == 0) {
			capsuleCollider.enabled = true;
		}

		cc.SetMoveInput(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Right Bumper"))
		{
			cc.SetDashInput(true);
		}
	}

	public void OnDash()
	{
		dashInvinsibiltyCooldown = dashInvinsibiltyDuration;
		capsuleCollider.enabled = false;

		gameObject.transform.Find("DashTrail").gameObject.GetComponent<ParticleSystem>().Play();
		float rz = Mathf.Atan2(cc.GetMoveDirection().y, cc.GetMoveDirection().x) * Mathf.Rad2Deg;
		Instantiate(arrow, tr.position, Quaternion.AngleAxis(rz, Vector3.forward));

		string[] soundNames = new string[] { "dash1", "dash2", "dash3", "dash4", "dash5" };
		AudioManager.Instance.PlaySfx(soundNames[Random.Range(0, soundNames.Length)]);
	}

	public void Hit(int damage) {
		health = Mathf.Max(0, health - damage);

		string[] soundNames = new string[] { "ow1", "ow2", "ow3", "ow4", "ow5", "ow6" };
		AudioManager.Instance.PlaySfx(soundNames[Random.Range(0, soundNames.Length)]);

		if (health == 0) {
			Destroy(gameObject);
		}
	}
}
