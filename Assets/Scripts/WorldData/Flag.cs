using UnityEngine;

namespace WorldData {
[CreateAssetMenu(fileName = "Flag", menuName = "World/Flag")]
public class Flag : ScriptableObject {
	public bool IsRaised() {
		return WorldState.IsSet(this);
	}

	public void Raise() {
		WorldState.SetFlag(this);
	}

	public void Lower() {
		WorldState.SetFlag(this, false);
	}
}
}
