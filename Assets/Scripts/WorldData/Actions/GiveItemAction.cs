using UnityEngine;
using WorldData.Actions.Base;

namespace WorldData.Actions {
[CreateAssetMenu(fileName = "GiveItemAction", menuName = "World/Actions/Give Item")]
public class GiveItemAction : BaseAction {
	[SerializeField] ItemData item;
	[Min(1)][SerializeField] int amount = 1;

	public override void Execute() {
		if (!item) {
			Debug.LogError($"{name}: Item is missing, nothing was given", this);
			return;
		}

		WorldState.AddItem(item, amount);
	}

	void OnValidate() {
		if (!item) {
			Debug.LogError($"{name}: Item is missing", this);
		}
	}
}
}
