using System;
using System.Collections.Generic;
using Quest;
using UnityEngine;

namespace WorldData {
public static class WorldState {
	static readonly HashSet<Flag> flags = new();
	static readonly Dictionary<Counter, int> counters = new();
	static readonly Dictionary<QuestItem, QuestState> questStates = new();
	static readonly Dictionary<ItemData, int> items = new(); // TODO: placeholder inventory . to be replaced with the real inventory system.
	static readonly HashSet<Location> reachedLocations = new(); // TODO: placeholder
	static PlayerClass currentClass; // TODO: placeholder

	/// <summary>
	/// Fires after any value in the world state changes.
	/// </summary>
	public static event Action Changed;

	// TODO: placeholder. PlayerClassCondition should read from the real player class system or stuff 
	public static PlayerClass CurrentClass {
		get => currentClass;
		set {
			currentClass = value;
			Changed?.Invoke();
		}
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	static void Reset() {
		flags.Clear();
		counters.Clear();
		questStates.Clear();
		items.Clear();
		reachedLocations.Clear();
		currentClass = null;
		Changed = null;
	}

	public static bool IsSet(Flag flag) => flags.Contains(flag);

	public static void SetFlag(Flag flag, bool value = true) {
		bool changed = value ? flags.Add(flag) : flags.Remove(flag);
		if (changed) Changed?.Invoke();
	}

	public static int GetCount(Counter counter) => counters.TryGetValue(counter, out int value) ? value : 0;

	public static void SetCount(Counter counter, int value) {
		counters[counter] = value;
		Changed?.Invoke();
	}

	public static void AddToCount(Counter counter, int amount) => SetCount(counter, GetCount(counter) + amount);

	public static QuestState GetQuestState(QuestItem quest) => questStates.TryGetValue(quest, out QuestState state) ? state : QuestState.NOT_STARTED;

	public static void SetQuestState(QuestItem quest, QuestState state) {
		questStates[quest] = state;
		Changed?.Invoke();
	}

	/// <summary>
	/// Returns a new list, so it is safe to change quest states while looping over it
	/// NOT_STARTED quests are only included if they were explicitly set back to NOT_STARTED
	/// </summary>
	public static List<QuestItem> GetQuests(QuestState state) {
		List<QuestItem> result = new();
		foreach (KeyValuePair<QuestItem, QuestState> pair in questStates) {
			if (pair.Value == state) result.Add(pair.Key);
		}
		return result;
	}

	// TODO: placeholder inventory. HasItemCondition should read from the real inventory once it exists
	public static int GetItemCount(ItemData item) => items.TryGetValue(item, out int count) ? count : 0;

	public static void AddItem(ItemData item, int amount = 1) {
		items[item] = GetItemCount(item) + amount;
		Changed?.Invoke();
	}

	public static void RemoveItem(ItemData item, int amount = 1) {
		items[item] = Mathf.Max(0, GetItemCount(item) - amount);
		Changed?.Invoke();
	}

	// TODO: placeholder. LocationReachedCondition should read from the real location system once it exists
	public static bool HasReached(Location location) => reachedLocations.Contains(location);

	public static void MarkReached(Location location) {
		if (reachedLocations.Add(location)) Changed?.Invoke();
	}
}
}
