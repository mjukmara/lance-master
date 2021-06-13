using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private static Game _instance;

    public static Game Instance { get { return _instance; } }

    public SpriteRenderer fader;
    public Transform respawnPoint;

    void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        fader.color = Color.black;

        StartCoroutine(FadeIn());
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TransitionTo(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            FinishGame();
        }
    }

    public void TransitionTo(int sceneIndex) {
        StartCoroutine(TransitionToScene(sceneIndex));
    }

    IEnumerator TransitionToScene(int index) {
        yield return new WaitForSeconds(2f);
        yield return FadeOut();
        LoadScene(index);
        yield return new WaitForSeconds(0.2f);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player) {
            player.transform.position = respawnPoint.position;
        }
        yield return new WaitForSeconds(0.2f);
        yield return FadeIn();
    }

    IEnumerator FadeOut() {
        LeanTween.value(gameObject, 0f, 1f, 0.5f)
            .setEaseLinear()
            .setOnUpdate((float value) => {
                fader.color = Color.Lerp(Color.clear, Color.black, value);
            });
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator FadeIn() {
        LeanTween.value(gameObject, 0f, 1f, 0.5f)
                .setEaseLinear()
                .setOnUpdate((float value) => {
                    fader.color = Color.Lerp(Color.black, Color.clear, value);
                });
        yield return new WaitForSeconds(0.5f);
    }

    static void LoadScene(int index) {
        SceneManager.LoadScene(index);
    }

    public void FinishGame() {
        StartCoroutine(TransitionToScene(1));
    }

    public void SaveRespawnPoint(Transform point) {
        respawnPoint = point;
    }
}
