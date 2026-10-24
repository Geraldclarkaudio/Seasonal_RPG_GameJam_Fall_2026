using UnityEngine;

namespace Quest.Conditions.Base {
public abstract class BaseCondition : ScriptableObject {
	protected abstract bool IsConditionMet();

	public bool IsMet() {
		return IsConditionMet();
	}
	
	/// <summary>
	/// Returns a bool of the quest status and a string of whether the quest is Completed or Not done for display
	/// </summary>
	/// <returns>string</returns>
	public virtual string GetProgress() {
		return IsConditionMet() ? "Completed" : "Not done";
	}
}
}