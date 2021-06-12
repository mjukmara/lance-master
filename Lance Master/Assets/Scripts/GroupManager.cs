using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour
{
	public Group[] groups;
}

public struct Group
{
	public GameObject spawntrigger;
	public GameObject[] Enemies;
	public GameObject nextDoor;
}