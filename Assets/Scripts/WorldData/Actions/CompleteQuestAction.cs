using Quest;
using UnityEngine;
using WorldData.Actions.Base;

namespace WorldData.Actions {
/// <summary>
/// Turns in a quest. Does nothing if the quest is not active or its complete condition is not met yet
/// </summary>
[CreateAssetMenu(fileName = "CompleteQuestAction", menuName = "World/Actions/Complete Quest")]
public class CompleteQuestAction : BaseAction {
	[SerializeField] QuestItem quest;

	public override void Execute() {
		if (!quest) {
			Debug.LogError($"{name}: Quest is missing, nothing was completed", this);
			return;
		}

		if (!QuestManager.Instance) {
			Debug.LogError($"{name}: No QuestManager in the scene, could not complete {quest.name}.", this);
			return;
		}

		QuestManager.Instance.CompleteQuest(quest);
	}

	void OnValidate() {
		if (!quest) {
			Debug.LogError($"{name}: Quest is missing", this);
		}
	}
}
}
