using System;
using Quest.Conditions;
using Quest.Conditions.Base;
using UnityEngine;
using WorldData;

namespace Quest {
public class QuestManager : MonoBehaviour {
	public static QuestManager Instance { get; private set; }

	public event Action<QuestItem> QuestStarted;
	public event Action<QuestItem> QuestCompleted;
	public event Action<QuestItem> QuestFailed;

	bool isBusy;
	bool checkPending;

	void Awake() {
		if (Instance != null && Instance != this) {
			Debug.LogWarning($"Extra QuestManager on {name} removed. Only one is allowed.", this);
			Destroy(this);
			return;
		}
		Instance = this;
	}

	void OnDestroy() {
		if (Instance == this) Instance = null;
	}

	void OnEnable() {
		WorldState.Changed += OnWorldChanged;
		OnWorldChanged();
	}

	void OnDisable() {
		WorldState.Changed -= OnWorldChanged;
	}

	public bool CanStart(QuestItem quest) {
		return WorldState.GetQuestState(quest) == QuestState.NOT_STARTED
			&& IsMetOrEmpty(quest.StartCondition);
	}

	public bool StartQuest(QuestItem quest) {
		if (!CanStart(quest)) return false;

		Run(() => {
			WorldState.SetQuestState(quest, QuestState.ACTIVE);
			QuestStarted?.Invoke(quest);
		});
		return true;
	}

	public bool CanComplete(QuestItem quest) {
		return WorldState.GetQuestState(quest) == QuestState.ACTIVE
			&& !IsFailing(quest)
			&& IsMetOrEmpty(quest.CompleteCondition);
	}

	public bool CompleteQuest(QuestItem quest) {
		if (!CanComplete(quest)) return false;

		Run(() => {
			WorldState.SetQuestState(quest, QuestState.COMPLETED);
			// TODO: run the quest's outcome here once outcomes exist.
			QuestCompleted?.Invoke(quest);
		});
		return true;
	}

	void FailQuest(QuestItem quest) {
		Run(() => {
			WorldState.SetQuestState(quest, QuestState.FAILED);
			QuestFailed?.Invoke(quest);
		});
	}

	void OnWorldChanged() {
		checkPending = true;
		Run(null);
	}

	/// <summary>
	/// Runs a quest change, then re-checks active quests until nothing else changes.
	/// Changes made while busy (by outcomes or event listeners) are queued instead of
	/// re-checking halfway, so QuestStarted always fires before QuestCompleted.
	/// </summary>
	void Run(Action change) {
		if (isBusy) {
			change?.Invoke();
			return;
		}

		isBusy = true;
		try {
			change?.Invoke();
			while (checkPending) {
				checkPending = false;
				foreach (QuestItem quest in WorldState.GetQuests(QuestState.ACTIVE)) {
					CheckQuest(quest);
				}
			}
		}
		finally {
			isBusy = false;
		}
	}

	void CheckQuest(QuestItem quest) {
		if (WorldState.GetQuestState(quest) != QuestState.ACTIVE) return;

		if (IsFailing(quest)) {
			FailQuest(quest);
		}
		else if (quest.AutoComplete && CanComplete(quest)) {
			CompleteQuest(quest);
		}
	}

	static bool IsFailing(QuestItem quest) {
		return quest.FailCondition != null && quest.FailCondition.IsMet();
	}

	static bool IsMetOrEmpty(BaseCondition condition) {
		return condition == null || condition.IsMet();
	}
}
}
