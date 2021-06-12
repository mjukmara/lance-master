using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
    private CharacterController cc;
    private Transform target = null;
    private Vector2 targetDirection = Vector2.zero;

    private void Start() {
        cc = GetComponent<CharacterController>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        UpdateTargeting();
        Debug.DrawLine(transform.position, transform.position + new Vector3(targetDirection.x, targetDirection.y), Color.red);
    }

    private void FixedUpdate() {

    }

    private void UpdateTargeting() {
        targetDirection = (target.position - transform.position).normalized;
    }
}
