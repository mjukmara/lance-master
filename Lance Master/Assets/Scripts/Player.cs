using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
	public int health = 100;
	public float dashInvinsibiltyDuration = 0.2f;
	public float dashInvinsibiltyCooldown = 0f;
	private CharacterController cc;
	private Rigidbody2D rb;
	public CapsuleCollider2D capsuleCollider;
	public CapsuleCollider2D dashCollider;
	public PlayerWallCollider playerWallCollider;
	private Transform tr;
	private Transform spritesTransform;
	private Animator animator;
	public GameObject arrow;
	public Lance lance;
	public float lanceThrowSpeed = 20f;

	private void OnEnable()
	{
		cc.OnDashEvent += OnDash;
		lance.OnLanceStoppedEvent += OnLanceStopped;
		playerWallCollider.OnWallCollisionEvent += OnWallCollision;
	}

	private void OnDisable() {
		playerWallCollider.OnWallCollisionEvent -= OnWallCollision;
		lance.OnLanceStoppedEvent -= OnLanceStopped;
		cc.OnDashEvent -= OnDash;
	}

	private void Awake()
	{
		cc = GetComponent<CharacterController>();
		tr = GetComponent<Transform>();

		spritesTransform = gameObject.transform.Find("Sprites").GetComponent<Transform>();
		animator = gameObject.transform.Find("Sprites").GetComponent<Animator>();
		//capsuleCollider = GetComponent<CapsuleCollider2D>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		cc.SetMoveInput(input);
		float animInput = Mathf.Abs(Mathf.Sqrt(Mathf.Pow(input.x, 2) + Mathf.Pow(input.y, 2)));
		animator.SetFloat("Input", animInput);
		if (Mathf.Abs(input.x) > 0.1f)
		{
			float direction = (input.x < 0) ? -1 : 1;
			Vector3 spriteScale = spritesTransform.localScale;
			spriteScale.x = direction;
			spritesTransform.localScale = spriteScale;
		}

		dashInvinsibiltyCooldown = Mathf.Max(0, dashInvinsibiltyCooldown - Time.deltaTime);
		if (dashInvinsibiltyCooldown == 0) {
			capsuleCollider.enabled = true;
			dashCollider.enabled = false;
			animator.SetBool("Dashing", false);
		}

		if (!lance.IsFlying())
		{
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Right Bumper")) {
				if (!cc.dashing)
				{
					cc.SetDashInput(true);
					lance.Throw(lanceThrowSpeed, input);
				}
			}
		}
	}

	public void OnDash()
	{
		dashInvinsibiltyCooldown = dashInvinsibiltyDuration;
		capsuleCollider.enabled = false;
		dashCollider.enabled = true;

		gameObject.transform.Find("DashTrail").gameObject.GetComponent<ParticleSystem>().Play();
		float rz = Mathf.Atan2(cc.GetMoveDirection().y, cc.GetMoveDirection().x) * Mathf.Rad2Deg;
		Instantiate(arrow, tr.position, Quaternion.AngleAxis(rz, Vector3.forward));
		animator.SetBool("Dashing", true);
		string[] soundNames = new string[] { "dash1", "dash2", "dash3", "dash4", "dash5" };
		AudioManager.Instance.PlaySfx(soundNames[Random.Range(0, soundNames.Length)]);
	}

	public void Hit(int damage)
	{
		health = Mathf.Max(0, health - damage);


		if (health == 0)
		{
			string[] soundNames = new string[] { "player-rip1", "player-rip2", "player-rip3", "player-rip4", "player-rip5", "player-rip6", "player-rip7", "player-rip8", "player-rip9" };
			AudioManager.Instance.PlaySfx(soundNames[Random.Range(0, soundNames.Length)]);
			Destroy(gameObject);
			Game.Instance.TransitionTo(0);
		}
		else
		{
			string[] soundNames = new string[] { "ow1", "ow2", "ow3", "ow4", "ow5", "ow6" };
			AudioManager.Instance.PlaySfx(soundNames[Random.Range(0, soundNames.Length)]);
		}
	}

	public void OnLanceStopped(Lance lance)
	{
		PickUpLance();
	}

	public void PickUpLance()
	{
		Vector3 lancePickupPos = lance.transform.position;
		Vector3 dir = lancePickupPos - transform.position;
		rb.AddForce(dir * 3000f);
		//LeanTween.cancel(gameObject);
		//LeanTween.move(gameObject, lancePickupPos, 0.1f);
		lance.gameObject.transform.SetParent(transform);
		lance.transform.localPosition = Vector3.zero;
	}

	public void OnWallCollision() {
		//LeanTween.cancel(gameObject);
    }

	void OnCollisionEnter2D(Collision2D other) {
		if (health < 100) {
			if (other.gameObject.tag == "HealthPickup") {
				health += 20;
				AudioManager.Instance.PlaySfx("crunch1");
				Destroy(other.gameObject);
			}
		}
	}
}
