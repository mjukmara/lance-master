using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropIn : MonoBehaviour {
    public GameObject artObject;
    public float verticalStartOffset = 10f;
    private Vector3 currentPos;
    private Vector3 finalPos;
    public float tweenTime = 0.5f;
    public float tweenTimeRandomness = 0.1f;

    public void Awake() {
        finalPos = artObject.transform.position;
        currentPos = new Vector3(finalPos.x, finalPos.y + verticalStartOffset, finalPos.z);
        artObject.transform.position = currentPos;
    }

    public void Start() {
        Tween();
    }

    public void Tween() {
        LeanTween.cancel(gameObject);
        LeanTween.move(artObject, finalPos, tweenTime + Random.Range(0, tweenTimeRandomness)).setEase(LeanTweenType.easeOutBounce);
        AudioManager.Instance.PlaySfx("impact1");
    }
}
