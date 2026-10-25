using System.Collections.Generic;
using UnityEngine;

namespace WorldData.Actions.Base {
/// <summary>
/// Used by quests (on start / complete / fail) and can be reused by dialogue, triggers, and so forth
/// </summary>
public abstract class BaseAction : ScriptableObject {
	public abstract void Execute();

	/// <summary>
	/// Runs every action in order. Empty slots are skipped with an error instead of stopping the rest
	/// </summary>
	public static void ExecuteAll(IReadOnlyList<BaseAction> actions, Object context = null) {
		if (actions == null) return;

		foreach (BaseAction action in actions) {
			if (!action) {
				Debug.LogError($"{(context ? context.name : "Action list")}: has an empty action slot, skipped.", context);
				continue;
			}
			action.Execute();
		}
	}
}
}
