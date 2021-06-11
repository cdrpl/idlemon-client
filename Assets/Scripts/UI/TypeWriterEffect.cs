using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TypeWriterEffect : MonoBehaviour {
    public Text label;

    void Awake() {
        if (label == null) {
            label = GetComponent<Text>();
        }
    }

    public void RunEffect(string msg) {
        label.text = string.Empty;
        StartCoroutine(R_TypeWriterEffect(msg));
    }

    IEnumerator R_TypeWriterEffect(string msg) {
        var wait = new WaitForSeconds(0.02f);

        for (int i = 0; i < msg.Length; i++) {
            label.text += msg[i];
            yield return wait;
        }
    }
}
