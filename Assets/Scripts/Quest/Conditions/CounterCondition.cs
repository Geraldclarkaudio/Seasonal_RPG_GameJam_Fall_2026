using Quest.Conditions.Base;
using UnityEngine;
using WorldData;

namespace Quest.Conditions {
[CreateAssetMenu(fileName = "CounterCondition", menuName = "Quest/Conditions/Counter")]
public class CounterCondition : BaseCondition {
	[SerializeField] Counter counter;
	[SerializeField] ComparisonType comparison = ComparisonType.GREATER_OR_EQUAL;
	[SerializeField] int target;

	protected override bool IsConditionMet() {
		int current = counter.Get();
		return comparison switch {
			ComparisonType.EQUAL => current == target,
			ComparisonType.NOT_EQUAL => current != target,
			ComparisonType.GREATER => current > target,
			ComparisonType.GREATER_OR_EQUAL => current >= target,
			ComparisonType.LESS => current < target,
			ComparisonType.LESS_OR_EQUAL => current <= target,
			_ => false
		};
	}

	public override string GetProgress() {
		return $"{counter.Get()}/{target}";
	}
}
}
