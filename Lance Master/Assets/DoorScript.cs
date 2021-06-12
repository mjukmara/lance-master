using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
	private Animator doorLeft;
	private Animator doorRight;

	void Start()
	{
		doorLeft = gameObject.transform.Find("door_left").GetComponent<Animator>();
		doorRight = gameObject.transform.Find("door_right").GetComponent<Animator>();
	}

	void Update()
	{

	}

	public void OpenDoor()
	{
		doorLeft.SetTrigger("Open");
		doorRight.SetTrigger("Open");
	}
}
