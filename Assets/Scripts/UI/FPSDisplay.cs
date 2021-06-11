using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour {
	public Text highestFpsLabel, averageFpsLabel, LowestFpsLabel;

	[SerializeField] FPSColor[] _coloring;
	FPSCounter _fpsCounter;

	static string[] _stringsTo99 = {
		"00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
		"10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
		"20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
		"30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
		"40", "41", "42", "43", "44", "45", "46", "47", "48", "49",
		"50", "51", "52", "53", "54", "55", "56", "57", "58", "59",
		"60", "61", "62", "63", "64", "65", "66", "67", "68", "69",
		"70", "71", "72", "73", "74", "75", "76", "77", "78", "79",
		"80", "81", "82", "83", "84", "85", "86", "87", "88", "89",
		"90", "91", "92", "93", "94", "95", "96", "97", "98", "99"
	};

	void Awake() {
		_fpsCounter = GetComponent<FPSCounter>();

		if (_coloring == null || _coloring.Length == 0) {
			_coloring = new FPSColor[5];
			_coloring[0] = new FPSColor(60, Color.cyan);
			_coloring[1] = new FPSColor(45, Color.green);
			_coloring[2] = new FPSColor(30, Color.yellow);
			_coloring[3] = new FPSColor(15, new Color(1f, 195f / 255f, 0, 1f)); //Orange color
			_coloring[4] = new FPSColor(0, Color.red);
		}
	}

	void Update() {
		display(highestFpsLabel, _fpsCounter.highestFps);
		display(averageFpsLabel, _fpsCounter.averageFps);
		display(LowestFpsLabel, _fpsCounter.lowestFps);
	}

	void display(Text label, int fps) {
		label.text = _stringsTo99[Mathf.Clamp(fps, 0, 99)];
		for (int i = 0; i < _coloring.Length; i++) {
			if (fps >= _coloring[i].minimumFps) {
				label.color = _coloring[i].color;
				break;
			}
		}
	}

	[System.Serializable]
	private struct FPSColor {
		public Color color;
		public int minimumFps;

		public FPSColor(int minFps, Color color) {
			this.color = color;
			minimumFps = minFps;
		}
	}
}
