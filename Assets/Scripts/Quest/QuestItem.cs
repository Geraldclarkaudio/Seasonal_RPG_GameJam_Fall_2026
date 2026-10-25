using System.Collections.Generic;
using JetBrains.Annotations;
using Quest.Conditions.Base;
using UnityEngine;
using WorldData.Actions.Base;

namespace Quest {
[CreateAssetMenu(fileName = "Quest Item", menuName = "Quest/Quest Item")]
public class QuestItem : ScriptableObject {
    [SerializeField] string id;
    [SerializeField] string questName;
    [SerializeField][TextArea] string description;
    [SerializeField] QuestType questType;
    [SerializeField] [CanBeNull] string questGiver; // TODO: needs to be changed to actual NPC or relevant NPC data/SO
    [SerializeField] [CanBeNull] BaseCondition startCondition;
    [SerializeField] BaseCondition completeCondition;
    [SerializeField] BaseCondition failCondition;
    [Tooltip("On: completes the moment the complete condition is met. Off: must be turned in (CompleteQuest).")]
    [SerializeField] bool autoComplete;

    // this is used so we can have actions happen on quest start, completion or failure 
    [Header("Actions")]
    [SerializeField] List<BaseAction> onStart = new();
    [SerializeField] List<BaseAction> onComplete = new();
    [SerializeField] List<BaseAction> onFail = new();

    public string Id => id;
    public string QuestName => questName;
    public string Description => description;
    public QuestType QuestType => questType;
    public BaseCondition StartCondition => startCondition;
    public BaseCondition CompleteCondition => completeCondition;
    public BaseCondition FailCondition => failCondition;
    public bool AutoComplete => autoComplete;
    public IReadOnlyList<BaseAction> OnStart => onStart;
    public IReadOnlyList<BaseAction> OnComplete => onComplete;
    public IReadOnlyList<BaseAction> OnFail => onFail;
}
}
