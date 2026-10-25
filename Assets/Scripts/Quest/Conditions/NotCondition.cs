using Quest.Conditions.Base;
using UnityEngine;

namespace Quest.Conditions {
[CreateAssetMenu(fileName = "NotCondition", menuName = "Quest/Conditions/Not")]
public class NotCondition : BaseCondition {
	[SerializeField] BaseCondition condition;

	protected override bool IsConditionMet() {
		return !condition.IsMet();
	}
	
	#if UNITY_EDITOR
		void OnValidate() {
			if (condition == null) {
				Debug.LogError($"{name}: Condition requires a valid condition of any type", this);
			}
		}
	#endif
}
}
