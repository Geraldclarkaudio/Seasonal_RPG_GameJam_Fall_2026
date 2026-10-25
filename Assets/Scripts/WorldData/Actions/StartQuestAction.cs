using Quest;
using UnityEngine;
using WorldData.Actions.Base;

namespace WorldData.Actions {
[CreateAssetMenu(fileName = "StartQuestAction", menuName = "World/Actions/Start Quest")]
public class StartQuestAction : BaseAction {
	[SerializeField] QuestItem quest;

	public override void Execute() {
		if (!quest) {
			Debug.LogError($"{name}: Quest is missing, nothing was started", this);
			return;
		}

		if (!QuestManager.Instance) {
			Debug.LogError($"{name}: No QuestManager in the scene, could not start {quest.name}.", this);
			return;
		}

		QuestManager.Instance.StartQuest(quest);
	}

	void OnValidate() {
		if (!quest) {
			Debug.LogError($"{name}: Quest is missing", this);
		}
	}
}
}
