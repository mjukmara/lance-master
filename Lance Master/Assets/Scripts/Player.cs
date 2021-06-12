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
	private CapsuleCollider2D capsuleCollider;
	private Transform tr;
	private Transform spritesTransform;
	private Animator animator;
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

		spritesTransform = gameObject.transform.Find("Sprites").GetComponent<Transform>();
		animator = gameObject.transform.Find("Sprites").GetComponent<Animator>();
		capsuleCollider = GetComponent<CapsuleCollider2D>();
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
		if (dashInvinsibiltyCooldown == 0)
		{
			capsuleCollider.enabled = true;
			animator.SetBool("Dashing", false);
		}

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
		animator.SetBool("Dashing", true);
		string[] soundNames = new string[] { "dash1", "dash2", "dash3", "dash4", "dash5" };
		AudioManager.Instance.PlaySfx(soundNames[Random.Range(0, soundNames.Length)]);
	}

	public void Hit(int damage)
	{
		health = Mathf.Max(0, health - damage);


		if (health == 0) {
			string[] soundNames = new string[] { "player-rip1", "player-rip2", "player-rip3", "player-rip4", "player-rip5", "player-rip6", "player-rip7", "player-rip8", "player-rip9" };
			AudioManager.Instance.PlaySfx(soundNames[Random.Range(0, soundNames.Length)]);
			Destroy(gameObject);
		} else {
			string[] soundNames = new string[] { "ow1", "ow2", "ow3", "ow4", "ow5", "ow6" };
			AudioManager.Instance.PlaySfx(soundNames[Random.Range(0, soundNames.Length)]);
		}
	}
}
