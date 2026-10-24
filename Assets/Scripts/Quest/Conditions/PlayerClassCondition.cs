using Quest.Conditions.Base;
using UnityEngine;
using WorldData;

namespace Quest.Conditions {
[CreateAssetMenu(fileName = "PlayerClassCondition", menuName = "Quest/Conditions/Player Class")]
public class PlayerClassCondition : BaseCondition {
	[SerializeField] PlayerClass playerClass;

	protected override bool IsConditionMet() {
		return WorldState.CurrentClass == playerClass;
	}
}
}
