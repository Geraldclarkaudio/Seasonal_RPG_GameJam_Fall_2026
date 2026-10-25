using UnityEngine;
using WorldData.Actions.Base;

namespace WorldData.Actions {
[CreateAssetMenu(fileName = "RemoveItemAction", menuName = "World/Actions/Remove Item")]
public class RemoveItemAction : BaseAction {
	[SerializeField] ItemData item;
	[Min(1)][SerializeField] int amount = 1;

	public override void Execute() {
		if (!item) {
			Debug.LogError($"{name}: Item is missing, nothing was removed.", this);
			return;
		}

		WorldState.RemoveItem(item, amount);
	}

	void OnValidate() {
		if (!item) {
			Debug.LogError($"{name}: Item is missing", this);
		}
	}
}
}
