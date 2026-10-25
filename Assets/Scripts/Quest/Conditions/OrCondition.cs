using System.Collections.Generic;
using Quest.Conditions.Base;
using UnityEngine;

namespace Quest.Conditions {
[CreateAssetMenu(fileName = "OrCondition", menuName = "Quest/Conditions/Or")]
public class OrCondition : BaseCondition {
	[SerializeField] List<BaseCondition> conditions = new();

	protected override bool IsConditionMet() {
		foreach (BaseCondition condition in conditions) {
			if (condition.IsMet()) return true;
		}
		return false;
	}
	
	#if UNITY_EDITOR
		void OnValidate() {
			if (conditions.Count < 2) {
				Debug.LogError($"{name}: Condition requires 2 conditions at least", this);
			}
		}
	#endif
}
}
