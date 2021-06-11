using UnityEngine;
using UnityEngine.UI;

public class VersionLabel : MonoBehaviour {
    void Awake() {
        GetComponent<Text>().text = "v" + Application.version;
    }
}
