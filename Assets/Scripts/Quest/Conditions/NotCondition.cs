using Quest.Conditions.Base;
using UnityEngine;

namespace Quest.Conditions {
[CreateAssetMenu(fileName = "NotCondition", menuName = "Quest/Conditions/Not")]
public class NotCondition : BaseCondition {
	[SerializeField] BaseCondition condition;

	protected override bool IsConditionMet() {
		return !condition.IsMet();
	}
}
}
