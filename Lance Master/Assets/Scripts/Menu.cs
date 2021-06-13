using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject titleImage;
    public GameObject pressAnyKeyText;
    public Image backdropImage;
    public Color backdropStartColor;
    public bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        backdropStartColor = backdropImage.color;
        Tween();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (!started && inputAxis.magnitude > 0) {
            StartCoroutine(StartGame());
        } else if (!started && Input.anyKeyDown) {
            StartCoroutine(StartGame());
        }
    }

    public IEnumerator StartGame() {
        started = true;
        Destroy(titleImage);
        Destroy(pressAnyKeyText);
        yield return FadeOutBackdrop();
    }

    public void Tween() {
        LeanTween.cancel(titleImage);
        Quaternion startRotation = Quaternion.Euler(0, 0, -0.5f);
        transform.localScale = Vector3.one;
        transform.localRotation = startRotation;
        LeanTween.scale(titleImage, Vector3.one * 1.05f, 5.5f).setEaseInOutCubic().setLoopPingPong();
        LeanTween.rotate(titleImage, new Vector3(0, 0, 0.5f), 1.5f).setEaseInOutCubic().setLoopPingPong();

        LeanTween.cancel(pressAnyKeyText);
        transform.localScale = Vector3.one;
        LeanTween.scale(pressAnyKeyText, Vector3.one * 1.05f, 1.5f).setEasePunch().setLoopClamp();
        LeanTween.rotate(pressAnyKeyText, Vector3.one * 1.05f, 1.5f).setEasePunch().setLoopClamp();

    }

    IEnumerator FadeOutBackdrop() {
        LeanTween.value(gameObject, 0f, 1f, 0.5f)
            .setEaseLinear()
            .setOnUpdate((float value) => {
                backdropImage.color = Color.Lerp(backdropStartColor, Color.clear, value);
            });
        yield return new WaitForSeconds(0.5f);
    }
}
