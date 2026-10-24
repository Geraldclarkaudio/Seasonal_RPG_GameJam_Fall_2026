using Quest.Conditions.Base;
using UnityEngine;
using WorldData;

namespace Quest.Conditions {
[CreateAssetMenu(fileName = "HasItemCondition", menuName = "Quest/Conditions/Has Item")]
public class HasItemCondition : BaseCondition {
	[SerializeField] ItemData item;
	[SerializeField] int amount = 1;

	protected override bool IsConditionMet() {
		return WorldState.GetItemCount(item) >= amount;
	}

	public override string GetProgress() {
		return $"{WorldState.GetItemCount(item)}/{amount}";
	}
}
}
