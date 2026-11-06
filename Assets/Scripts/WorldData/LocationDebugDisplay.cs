using UnityEngine;

namespace WorldData {
public class LocationDebugDisplay : MonoBehaviour {
	[SerializeField] bool showOnScreen = true;
	[SerializeField] bool logChanges = true;

	void OnEnable() {
		WorldState.LocationChanged += OnLocationChanged;
	}

	void OnDisable() {
		WorldState.LocationChanged -= OnLocationChanged;
	}

	void OnLocationChanged(Location previous, Location current) {
		if (logChanges) {
			Debug.Log($"Location: {NameOf(previous)} -> {NameOf(current)}", current);
		}
	}

	void OnGUI() {
		if (!showOnScreen) return;
		GUI.Label(new Rect(10f, 10f, 400f, 25f), $"Location: {NameOf(WorldState.CurrentLocation)}");
	}

	static string NameOf(Location location) {
		return location ? location.DisplayName : "none";
	}
}
}
