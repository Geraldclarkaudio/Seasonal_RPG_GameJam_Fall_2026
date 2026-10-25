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

	#if UNITY_EDITOR
		void OnValidate() {
			if  (conditions.Count < 2) {
				Debug.LogError($" {name}: AND needs at least 2 conditions ", this);
			}

			if (conditions.Contains(null)) {
				Debug.LogError($" {name}: AND has an empty slot ", this);
			}
		}
	#endif
}
}
