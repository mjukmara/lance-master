﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private static Game _instance;

    public static Game Instance { get { return _instance; } }

    public SpriteRenderer fader;

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
            LoadScene(0);
        }
    }

    public void TransitionTo(int sceneIndex) {
        StartCoroutine(TransitionToScene(sceneIndex));
    }

    IEnumerator TransitionToScene(int index) {
        yield return new WaitForSeconds(2f);
        yield return FadeOut();
        LoadScene(index);
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
}