using System.Collections.Generic;
using Quest.Conditions.Base;
using UnityEngine;

namespace Quest.Conditions {
[CreateAssetMenu(fileName = "AndCondition", menuName = "Quest/Conditions/And")]
public class AndCondition : BaseCondition {
	[SerializeField] List<BaseCondition> conditions = new();

	protected override bool IsConditionMet() {
		foreach (BaseCondition condition in conditions) {
			if (!condition.IsMet()) return false;
		}
		return true;
	}
}
}
