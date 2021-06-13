using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private static Checkpoints _instance;
    public static Checkpoints Instance { get { return _instance; } }
    void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
    }
}
