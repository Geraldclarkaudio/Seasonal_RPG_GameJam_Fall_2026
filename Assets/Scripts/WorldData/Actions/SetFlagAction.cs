using UnityEngine;
using WorldData.Actions.Base;

namespace WorldData.Actions {
[CreateAssetMenu(fileName = "SetFlagAction", menuName = "World/Actions/Set Flag")]
public class SetFlagAction : BaseAction {
	[SerializeField] Flag flag;
	[Tooltip("On: raise the flag. Off: lower it.")]
	[SerializeField] bool raise = true;

	public override void Execute() {
		if (!flag) {
			Debug.LogError($"{name}: Flag is missing, nothing was set", this);
			return;
		}

		if (raise) flag.Raise();
		else flag.Lower();
	}

	void OnValidate() {
		if (!flag) {
			Debug.LogError($"{name}: Flag is missing", this);
		}
	}
}
}
