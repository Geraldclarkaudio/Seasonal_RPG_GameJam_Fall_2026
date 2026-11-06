using UnityEngine;

namespace WorldData {
/// <summary>
/// A named number the game keeps track of, like stuff obtained or enemies killed
/// The asset is only the name used to clarify for us. The current value lives in WorldState
/// </summary>
[CreateAssetMenu(fileName = "Counter", menuName = "World/Counter")]
public class Counter : ScriptableObject {
	[SerializeField] bool useMin;
	[SerializeField] int min;
	[SerializeField] bool useMax;
	[SerializeField] int max;

	public int Get() {
		return WorldState.GetCount(this);
	}

	public void Set(int value) {
		WorldState.SetCount(this, value);
	}

	public void Add(int amount = 1) {
		WorldState.AddToCount(this, amount);
	}

	public int Clamp(int value) {
		if (useMin && value < min) value = min;
		if (useMax && value > max) value = max;
		return value;
	}

	void OnValidate() {
		if (useMin && useMax && min > max) {
			Debug.LogError($"{name}: Min is higher than Max", this);
		}
	}
}
}
