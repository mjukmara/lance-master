using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Magazine {
    public List<GameObject> projectilePrefabs;
    public int at = 0;

    public bool IsEmpty() {
        return at >= projectilePrefabs.Count;
    }

    public GameObject PopProjectile() {
        GameObject projectilePrefab = projectilePrefabs[at];
        at++;
        return projectilePrefab;
    }

    public void Renew() {
        at = 0;
    }
}
