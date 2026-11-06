using UnityEngine;
using WorldData.Actions.Base;

namespace WorldData.Actions {
[CreateAssetMenu(fileName = "SetClassAction", menuName = "World/Actions/Set Class")]
public class SetClassAction : BaseAction {
	[SerializeField] PlayerClass playerClass;

	public override void Execute() {
		WorldState.CurrentClass = playerClass;
	}
}
}
