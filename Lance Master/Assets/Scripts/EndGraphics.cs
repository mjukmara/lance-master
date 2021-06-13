using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGraphics : MonoBehaviour
{
    public GameObject title;
    public Image titleImage;
    public GameObject master;
    public Image masterImage;
    public GameObject pressAnyKey;
    public Image pressAnyKeyImage;
    public bool closable = false;

    private void Awake() {
        titleImage.color = Color.clear;
        masterImage.color = Color.clear;
        pressAnyKeyImage.color = Color.clear;
    }
    private void Start() {
        StartCoroutine(Animate());
    }

    private void Update() {
        if (closable && Input.anyKeyDown) {
            Debug.Log("Close");
            Application.Quit();
        }
    }

    IEnumerator Animate() {
        yield return AnimateTitle();
        yield return new WaitForSeconds(2.0f);
        yield return AnimateMaster();
        yield return new WaitForSeconds(2.0f);
        yield return AnimatePressAnyKey();
        closable = true;
    }

    IEnumerator AnimateTitle() {

        title.transform.localPosition = new Vector3(0, 0, 0);
        LeanTween.moveLocal(title, new Vector3(0, 80, 0), 2.5f).setEaseInOutBack();
        LeanTween.value(gameObject, 0f, 1f, 2.5f)
                .setEaseLinear()
                .setOnUpdate((float value) => {
                    titleImage.color = Color.Lerp(Color.clear, Color.white, value);
                });
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator AnimateMaster() {
        LeanTween.value(gameObject, 0f, 1f, 0.1f)
                .setEaseLinear()
                .setOnUpdate((float value) => {
                    masterImage.color = Color.Lerp(Color.clear, Color.white, value);
                });
        LeanTween.scale(master, Vector3.one * 2f, 0.5f).setEasePunch();
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator AnimatePressAnyKey() {
        LeanTween.value(gameObject, 0f, 1f, 0.1f)
                .setEaseLinear()
                .setOnUpdate((float value) => {
                    pressAnyKeyImage.color = Color.Lerp(Color.clear, Color.white, value);
                });
        LeanTween.scale(pressAnyKey, Vector3.one * 1.2f, 0.5f).setEasePunch();
        yield return new WaitForSeconds(0.5f);
    }
}
