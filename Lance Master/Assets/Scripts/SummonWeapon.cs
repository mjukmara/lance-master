using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonWeapon : MonoBehaviour
{
	public GameObject projectile;
	public GameObject shootEffect;
	private Vector3 target;
	private Transform tr;
	public int ammo = 3;
	public bool eyeSpawn = false;

	void Awake()
	{
		tr = GetComponent<Transform>();
	}

	void Update()
	{

	}

	public void Fire()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform.position;

		Vector2 scale = new Vector3(1, tr.localScale.y, tr.localScale.z);
		if (target.x < tr.position.x)
		{
			scale.x = -1;
		}
		tr.localScale = scale;

		Vector3 relativePosition = (target - tr.position);
		float angle = Mathf.Atan2(relativePosition.y, relativePosition.x) * Mathf.Rad2Deg;
		if (eyeSpawn)
		{
			Vector3 pos = gameObject.transform.Find("Eye").transform.position;
			Instantiate(projectile, pos, Quaternion.Euler(0f, 0f, angle));
			Instantiate(shootEffect, pos, Quaternion.identity);
		}
		else
		{
			Instantiate(projectile, tr.position, Quaternion.Euler(0f, 0f, angle));
			Instantiate(shootEffect, tr.position, Quaternion.identity);
		}
	}

	public void KillCountDown()
	{
		ammo--;
		if (ammo == 0)
			Destroy(gameObject, 0);
	}
}
