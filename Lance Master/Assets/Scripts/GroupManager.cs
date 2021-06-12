using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour
{
	public List<GameObject> enemies;
	public GameObject spawnEffect;
	private List<GameObject> spawnedEnemies;
	private bool triggered = false;
	private bool spawned = false;

	void Update()
	{
		if (triggered && !spawned)
		{
			Debug.Log("Spawning enemies!");
			spawnedEnemies = new List<GameObject>();

			//Spawn enemies at child "spawnPoint_<i>" gameObject positions
			for (int i = 0; i < enemies.Count; i++)
			{
				if (i > gameObject.transform.Find("spawnPoints").childCount - 1) { continue; }
				Vector3 spawnPoint = gameObject.transform.Find("spawnPoints").GetChild(i).position;
				spawnedEnemies.Add((GameObject)Instantiate(enemies[i], spawnPoint, Quaternion.identity));
				Instantiate(spawnEffect, spawnPoint, Quaternion.identity);
			}

			spawned = true;
		}

		if (spawned)
		{
			if (Finished())
			{
				Debug.Log("Open the door!");
				GameObject door = gameObject.transform.Find("door").gameObject;
				door.GetComponent<BoxCollider2D>().enabled = false;
			}
		}
	}

	bool Finished()
	{
		bool isFinished = true;

		for (int i = 0; i < spawnedEnemies.Count; i++)
		{
			if (spawnedEnemies[i] != null)
			{
				isFinished = false;
			}
		}

		return isFinished;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			triggered = true;
		}
	}
}