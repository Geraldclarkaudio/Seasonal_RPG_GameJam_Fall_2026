using JetBrains.Annotations;
using Quest.Conditions.Base;
using UnityEngine;

namespace Quest {
[CreateAssetMenu(fileName = "Quest Item", menuName = "Quest/Quest Item")]
public class QuestItem : ScriptableObject {
    [SerializeField] string id;
    [SerializeField] string questName;
    [SerializeField] string description;
    [SerializeField] QuestType questType;
    [SerializeField] [CanBeNull] string questGiver; // TODO: needs to be changed to actual NPC or relevant NPC data/SO
    [SerializeField] [CanBeNull] BaseCondition startCondition;
    [SerializeField] string outcome;
    [SerializeField] BaseCondition completeCondition;
    [SerializeField] BaseCondition failCondition;
    [Tooltip("On: completes the moment the complete condition is met. Off: must be turned in (CompleteQuest).")]
    [SerializeField] bool autoComplete;

    public string Id => id;
    public string QuestName => questName;
    public string Description => description;
    public QuestType QuestType => questType;
    public BaseCondition StartCondition => startCondition;
    public BaseCondition CompleteCondition => completeCondition;
    public BaseCondition FailCondition => failCondition;
    public bool AutoComplete => autoComplete;
}
}
