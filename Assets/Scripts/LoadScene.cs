using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// A wrapper around unitys SceneManager.
/// </summary>
public class LoadScene : MonoBehaviour {
    public void Load(string name) {
        SceneManager.LoadScene(name);
    }

    public void Load(int index) {
        SceneManager.LoadScene(index);
    }

    public void LoadSceneAsync(string name) {
        StartCoroutine(R_LoadSceneAsync(name));
    }

    public void LoadSceneAsync(int index) {
        StartCoroutine(R_LoadSceneAsync(index));
    }

    IEnumerator R_LoadSceneAsync(string name) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }

    IEnumerator R_LoadSceneAsync(int index) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }
}
