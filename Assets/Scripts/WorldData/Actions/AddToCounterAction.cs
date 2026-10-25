using UnityEngine;
using WorldData.Actions.Base;

namespace WorldData.Actions {
[CreateAssetMenu(fileName = "AddToCounterAction", menuName = "World/Actions/Add To Counter")]
public class AddToCounterAction : BaseAction {
	[SerializeField] Counter counter;
	[Tooltip("Use a negative number to subtract")]
	[SerializeField] int amount = 1;

	public override void Execute() {
		if (!counter) {
			Debug.LogError($"{name}: Counter is missing, nothing was added", this);
			return;
		}

		counter.Add(amount);
	}

	void OnValidate() {
		if (!counter) {
			Debug.LogError($"{name}: Counter is missing", this);
		}
	}
}
}
