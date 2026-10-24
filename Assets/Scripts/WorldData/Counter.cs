using UnityEngine;

namespace WorldData {
/// <summary>
/// A named number the game keeps track of, like stuff obtained or enemies killed
/// The asset is only the name. The current value lives in WorldState.
/// </summary>
[CreateAssetMenu(fileName = "Counter", menuName = "World/Counter")]
public class Counter : ScriptableObject {
	public int Get() {
		return WorldState.GetCount(this);
	}

	public void Set(int value) {
		WorldState.SetCount(this, value);
	}

	public void Add(int amount = 1) {
		WorldState.AddToCount(this, amount);
	}
}
}
