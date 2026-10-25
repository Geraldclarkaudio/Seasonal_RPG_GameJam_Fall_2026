using Quest.Conditions.Base;
using UnityEngine;
using WorldData;

namespace Quest.Conditions {
[CreateAssetMenu(fileName = "FlagCondition", menuName = "Quest/Conditions/Flag")]
public class FlagCondition : BaseCondition {
	[SerializeField] Flag flag;
	[SerializeField] bool expectedValue = true;

	protected override bool IsConditionMet() {
		return WorldState.IsSet(flag) == expectedValue;
	}
	
	#if UNITY_EDITOR
		void OnValidate() {
			if (!flag) {
				Debug.LogError($"{name}: Flag condition is missing", this);
			}
		}
	#endif
}
}
