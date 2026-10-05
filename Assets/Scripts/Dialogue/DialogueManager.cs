
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

    public class DialogueManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _speakerName;
        [SerializeField]
        private TMP_Text textComponent;
        [SerializeField]
        private Image _iconObject;
        [SerializeField]
        private float canProceed = -1;
        [SerializeField]
        private float textRate = 1f; //forces text to wait 1 second. adjust to feel if we even want a cooldown  

        public Dialogue[] dialogues; // the list of dialogue scriptable objects..probably wont need this, just depends on how we actually need dialogue to fit into the design. 
        public Dialogue currentDialogue; // the currently used dialogue SO
        public int keyIndex; // which index of the keys for that dialogue
        public int dialogueIndex;//which dialogue asset in the dialogues array. 

        public bool dialogueIsActive; 
        public Canvas _dialogueCanvas;

        public static event Action onBeginDialogue;
        public static event Action onEndDialogue;

        [SerializeField]
        private RectTransform _dialogueRectTransform;
        [SerializeField]
        private RectTransform[] _dialoguePanelPositions; // if we want this its here now.. 

        [SerializeField]
        private Button _nextButton;
    /// <summary>
    /// json stuff
    /// </summary>
        JSONNode _langNode;
        string _langCode = "en";

    private void Awake()
    {
        LoadMockData();
    }
        public string GetText(string key)
            {
                string value = _langNode?[key];
                return value ?? "--missing--";
            }

        private void LoadMockData()
        {
            // Load Dev Language File from StreamingAssets
            string langFilePath = Path.Combine(Application.streamingAssetsPath, "language.json");
            if (File.Exists(langFilePath))
            {
                string langDataAsJson = File.ReadAllText(langFilePath);
                var lang = JSON.Parse(langDataAsJson)[_langCode];
                LanguageUpdate(lang.ToString());
            }
        }

        public void LanguageUpdate(string langJSON)
        {
            if (string.IsNullOrEmpty(langJSON))
                return;
            //Debug.Log("LangUpdate()");
            _langNode = JSON.Parse(langJSON);
        }

        public void StartDialogue() // called when button is clicked or specific event happens. 
        {
            onBeginDialogue?.Invoke();

            _dialogueCanvas.enabled = true;

            if (dialogueIndex < dialogues.Length)
            {
                currentDialogue = dialogues[dialogueIndex];
                keyIndex = 0;//reset keys back to 0
                dialogueIndex = currentDialogue.dialogueID;
                dialogueIsActive = true;
            }

            if (currentDialogue != null)
            {

                if (currentDialogue._animations.Length > 0) // anims? 
                {
                    if (currentDialogue._animations[keyIndex] != null)
                    {
                    //do we want camera animation state changes? 
                    }
            }
                if (currentDialogue._newUIEvents.Length > 0) // ui events? 
                {
                    if (currentDialogue._newUIEvents[keyIndex] != null)
                    {
                        currentDialogue._newUIEvents[keyIndex].Raise();
                    }
                }
                if (currentDialogue._dialoguePositionChanges.Length > 0) // pos changes?
                {
                    if (currentDialogue._dialoguePositionChanges[keyIndex] != null)
                    {
                        currentDialogue._dialoguePositionChanges[keyIndex].Raise();
                    }
                }
                if (currentDialogue._eventTriggers != null) // trigs? 
                {
                    if (currentDialogue._eventTriggers.Length > 0)
                    {
                        if (currentDialogue._eventTriggers[keyIndex] != null)
                        {
                            currentDialogue._eventTriggers[keyIndex].Raise();
                        }
                    }
                }
            }

            if (_iconObject != null)
            {
                _iconObject.sprite = currentDialogue.icons[keyIndex];
            }


            textComponent.text = GetText(currentDialogue.key[keyIndex]);
            if (currentDialogue.speakerName.Length > 0)
            {
                _speakerName.text = currentDialogue.speakerName[keyIndex];
            }

            textRate = textComponent.text.Length * 0.0667f;
            canProceed = Time.time + textRate;
        }

        public void NextLine()
        {
            if (canProceed < Time.time)
            {
                if (keyIndex < currentDialogue.key.Length - 1) // if the current dialgue asset's key index is less than the length of the array...
                {
                    keyIndex++;

                    if (currentDialogue._animations.Length > 0)
                    {
                        if (currentDialogue._animations[keyIndex] != null)
                        {
                            //do we want camera animation state changes? 
                        }
                    }

                    if (currentDialogue._newUIEvents.Length > 0)
                    {
                        if (currentDialogue._newUIEvents[keyIndex] != null)
                        {
                            currentDialogue._newUIEvents[keyIndex].Raise();
                        }
                    }
                    if (currentDialogue._dialoguePositionChanges.Length > 0) // triple checking null.. why?
                    {
                        if (currentDialogue._dialoguePositionChanges[keyIndex] != null)
                        {
                            currentDialogue._dialoguePositionChanges[keyIndex].Raise();
                        }
                    }
                    if (currentDialogue._eventTriggers != null)
                    {
                        if (currentDialogue._eventTriggers.Length > 0)
                        {
                            if (currentDialogue._eventTriggers[keyIndex] != null)
                            {
                                currentDialogue._eventTriggers[keyIndex].Raise();
                            }
                        }
                    }

                    if (_iconObject != null)
                    {
                        _iconObject.sprite = currentDialogue.icons[keyIndex];
                    }


                    textComponent.text = GetText(currentDialogue.key[keyIndex]);
                    if (currentDialogue.speakerName.Length > 0)
                    {
                        _speakerName.text = currentDialogue.speakerName[keyIndex];
                    }

                    textRate = textComponent.text.Length * 0.0667f;
                    canProceed = Time.time + textRate;
                }
                else if (keyIndex >= currentDialogue.key.Length - 1) // at the end of the keys
                {
                    if (currentDialogue._endDialogueEvent != null)
                    {
                        currentDialogue._endDialogueEvent.Raise(); // if something should happen at the end of a dialogue, do that thing
                    }

                    if (currentDialogue._postDialogueEvents != null)
                    {
                        foreach (GameEvent ev in currentDialogue._postDialogueEvents)
                        {
                            ev.Raise();
                        }
                    }

                   // dialogueIndex++; // move to the next dialogue asset for the next time dialogues take place. 
                    dialogueIsActive = false;
                keyIndex = 0;
                    _dialogueCanvas.enabled = false;
                    onEndDialogue?.Invoke(); // can cause problems if more than one thing subs to this in each scene..
                }
            }
        }

        public void ChangeDialoguePosition(int position)
        {
            _dialogueRectTransform.anchoredPosition = _dialoguePanelPositions[position].anchoredPosition;
        }

        private void Update()
        {
            if (dialogueIsActive)
            {
                //testing delete later (used to make wait time between dialogue lines 0. If we want to just let players spam it, I dont care)
                canProceed = 0;
            }
        }
    }