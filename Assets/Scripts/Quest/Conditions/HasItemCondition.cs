using Quest.Conditions.Base;
using UnityEngine;
using WorldData;

namespace Quest.Conditions {
[CreateAssetMenu(fileName = "HasItemCondition", menuName = "Quest/Conditions/Has Item")]
public class HasItemCondition : BaseCondition {
	[SerializeField] ItemData item;
	[Min(0)][SerializeField] int amount = 1;

	protected override bool IsConditionMet() {
		return WorldState.GetItemCount(item) >= amount;
	}

	public override string GetProgress() {
		return $"{WorldState.GetItemCount(item)}/{amount}";
	}
	
	void OnValidate() {
		if (item == null) {
			Debug.LogError($"{name}: Condition requires a valid item", this);
		}
	}
}
}
