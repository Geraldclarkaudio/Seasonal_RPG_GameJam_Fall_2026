using UnityEngine;

namespace Quest.Conditions.Base {
public abstract class BaseCondition : ScriptableObject {
	[Tooltip("Flips the result. Ticked means this condition is met when the check is NOT true")]
	[SerializeField] bool invert;

	protected abstract bool IsConditionMet();

	public bool IsMet() {
		return IsConditionMet() != invert;
	}

	/// <summary>
	/// Returns a string of whether the condition is Completed or Not done for display
	/// </summary>
	/// <returns>string</returns>
	public virtual string GetProgress() {
		return IsMet() ? "Completed" : "Not done";
	}
}
}