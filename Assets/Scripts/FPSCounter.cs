using UnityEngine;

public class FPSCounter : MonoBehaviour {
	public int frameRange = 60;

	public int highestFps { get; private set; }
	public int averageFps { get; private set; }
	public int lowestFps { get; private set; }

	static FPSCounter _instance { get; set; }
	int[] _fpsBuffer;
	int _fpsBufferIndex;

	void Awake() {
		if (_instance == null) {
			_instance = this;
		}
		else {
			Destroy(transform.parent.gameObject);
		}
	}

	void Update() {
		if (_fpsBuffer == null || _fpsBuffer.Length != frameRange) {
			initializeBuffer();
		}
		updateBuffer();
		calculateFps();
	}

	void initializeBuffer() {
		if (frameRange <= 0) {
			frameRange = 1;
		}
		_fpsBuffer = new int[frameRange];
		_fpsBufferIndex = 0;
	}

	void updateBuffer() {
		_fpsBuffer[_fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
		if (_fpsBufferIndex >= frameRange) {
			_fpsBufferIndex = 0;
		}
	}

	void calculateFps() {
		int sum = 0;
		int highest = 0;
		int lowest = int.MaxValue;
		for (int i = 0; i < frameRange; ++i) {
			int fps = _fpsBuffer[i];
			sum += fps;
			if (fps > highest) {
				highest = fps;
			}
			if (fps < lowest) {
				lowest = fps;
			}
		}
		highestFps = highest;
		averageFps = sum / frameRange;
		lowestFps = lowest;
	}
}
