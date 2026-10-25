using UnityEngine;
using WorldData.Actions.Base;

namespace WorldData.Actions {
/// <summary>
/// Raises a GameEvent, so anything outside the quest system (doors, cutscenes, music) can react.
/// </summary>
[CreateAssetMenu(fileName = "RaiseGameEventAction", menuName = "World/Actions/Raise Game Event")]
public class RaiseGameEventAction : BaseAction {
	[SerializeField] GameEvent gameEvent;

	public override void Execute() {
		if (!gameEvent) {
			Debug.LogError($"{name}: GameEvent is missing, nothing was raised", this);
			return;
		}

		gameEvent.Raise();
	}

	void OnValidate() {
		if (!gameEvent) {
			Debug.LogError($"{name}: GameEvent is missing", this);
		}
	}
}
}
