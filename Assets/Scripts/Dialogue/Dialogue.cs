using UnityEngine;

    [CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueObject/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public int dialogueID; // not really used right now.... 
        public string[] key; // the key fed to the current dialogue
        public Sprite[] icons; // photo of speaker
        public string[] speakerName; // name of person talking. 
        public string[] _animations; // used for state driven camera changes if we even decide to use Cinemachine... 
        public GameEvent[] _newUIEvents; // ui specific animations? 
        public GameEvent[] _dialoguePositionChanges; // change where the dialogue box appears on a line by line basis.. 
        public GameEvent[] _eventTriggers; // just another triger for the dialogue line in case the ones that already exist dont cover all our needs. can add more specific ones as we go if needed.  
        public GameEvent _endDialogueEvent; // dialogue ended
        public GameEvent[] _postDialogueEvents; // what happens AFTER the dialogue finishes? 
    }
