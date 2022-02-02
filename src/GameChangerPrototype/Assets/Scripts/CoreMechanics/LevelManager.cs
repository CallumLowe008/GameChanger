using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(string sceneToLoad) {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
