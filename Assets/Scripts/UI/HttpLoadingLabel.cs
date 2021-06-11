using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HttpLoadingLabel : MonoBehaviour {
    Text label;

    void Awake() {
        label = GetComponent<Text>();
    }

    void OnEnable() {
        StartCoroutine(LoadingAnim());
    }

    void OnDisable() {
        StopAllCoroutines();
        label.text = "";
    }

    IEnumerator LoadingAnim() {
        var wait = new WaitForSeconds(0.25f);

        while (true) {
            for (int i = 0; i < 3; i++) {
                label.text += ".";
                yield return wait;
            }
            for (int i = 3; i > 0; i--) {
                label.text = label.text.Substring(1);
                yield return wait;
            }
        }
    }
}
