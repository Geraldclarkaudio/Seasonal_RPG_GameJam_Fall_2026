using Quest.Conditions.Base;
using UnityEngine;
using WorldData;

namespace Quest.Conditions {
[CreateAssetMenu(fileName = "QuestStateCondition", menuName = "Quest/Conditions/Quest State")]
public class QuestStateCondition : BaseCondition {
	[SerializeField] QuestItem quest;
	[SerializeField] QuestState state;

	protected override bool IsConditionMet() {
		return WorldState.GetQuestState(quest) == state;
	}
	

	void OnValidate() {
		if (quest == null) {
			Debug.LogError($"{name}: Quest is required", this);
		}
	}
		

}
}
