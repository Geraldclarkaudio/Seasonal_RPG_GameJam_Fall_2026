using Quest.Conditions.Base;
using UnityEngine;
using WorldData;

namespace Quest.Conditions {
[CreateAssetMenu(fileName = "LocationReachedCondition", menuName = "Quest/Conditions/Location Reached")]
public class LocationReachedCondition : BaseCondition {
	[SerializeField] Location location;

	protected override bool IsConditionMet() {
		return WorldState.HasReached(location);
	}
	
	#if UNITY_EDITOR
	void OnValidate() {
		if (location == null) {
			Debug.LogError($"{name}: Condition requires a valid location", this);
		}
	}
#endif
}
}
