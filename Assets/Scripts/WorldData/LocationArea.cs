using System.Collections.Generic;
using UnityEngine;

namespace WorldData {
/// <summary>
/// areas should not overlap. Leaving an area keeps it as the current location until another area is entered
/// </summary>
[RequireComponent(typeof(Collider))]
public class LocationArea : MonoBehaviour {
	const string PLAYER_TAG = "Player";

	[SerializeField] Location location;
	[SerializeField] Color gizmoColor = new(0.2f, 0.8f, 1f, 0.2f);

	[Header("Optional Event Triggers")]
	[SerializeField] List<GameEvent> onEnter = new();
	[SerializeField] List<GameEvent> onExit = new();

	public Location Location => location;

	void OnTriggerEnter(Collider other) {
		if (!other.CompareTag(PLAYER_TAG)) return;

		WorldState.SetCurrentLocation(location);
		RaiseAll(onEnter);
	}

	void OnTriggerExit(Collider other) {
		if (!other.CompareTag(PLAYER_TAG)) return;

		RaiseAll(onExit);
	}

	static void RaiseAll(List<GameEvent> events) {
		foreach (GameEvent gameEvent in events) {
			if (gameEvent) gameEvent.Raise();
		}
	}

	void OnValidate() {
		if (!location) {
			Debug.LogError($"{name}: LocationArea needs a Location assigned", this);
		}

		if (onEnter.Contains(null) || onExit.Contains(null)) {
			Debug.LogError($"{name}: LocationArea has an empty event slot", this);
		}

		if (TryGetComponent(out Collider col) && !col.isTrigger) {
			Debug.LogError($"{name}: LocationArea collider must have Is Trigger ticked", this);
		}
	}

	void OnDrawGizmos() {
		if (!TryGetComponent(out Collider col)) return;

		Gizmos.color = gizmoColor;
		if (col is BoxCollider box) {
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.DrawCube(box.center, box.size);
			Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 1f);
			Gizmos.DrawWireCube(box.center, box.size);
			Gizmos.matrix = Matrix4x4.identity;
		}
		else {
			Gizmos.DrawCube(col.bounds.center, col.bounds.size);
		}

#if UNITY_EDITOR
		string label = location ? location.DisplayName : "(no location)";
		UnityEditor.Handles.Label(col.bounds.center, label);
#endif
	}
}
}
