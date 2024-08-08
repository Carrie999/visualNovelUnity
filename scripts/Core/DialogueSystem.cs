using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;


namespace DIALOGUE
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private DialogueSystemConfigurationSO _config;

        public DialogueSystemConfigurationSO config => _config;
        // [SerializeField] private DialogueContainer dialogueContainer = new DialogueContainer();
        public DialogueContainer dialogueContainer = new DialogueContainer();
        private ConversationManager conversationManager;
        //  = new ConversationManager()


        private TextArchitect architect;
        public static DialogueSystem instance
        {
            get; private set;
        }

        public delegate void DialogueSystemEvent();
        public event DialogueSystemEvent onUserPrompt_Next;
        public bool isRunningConversation => conversationManager.isRunning;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Initialize();
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }

        bool _initialized = false;
        private void Initialize()
        {
            if (_initialized)
                return;
            architect = new TextArchitect(dialogueContainer.dialogueText);
            conversationManager = new ConversationManager(architect);
        }


        public void OnUserPrompt_Next()
        {
            onUserPrompt_Next?.Invoke();
        }



        public void ApplySpeakerDataToDialogueContainer(string speakerName)
        {
            Character character = CharacterManager.instance.GetCharacter(speakerName);
            CharacterConfigData config = character != null ? character.config : CharacterManager.instance.GetCharacterConfig(speakerName);
            ApplySpeakerDataToDialogueContainer(config);

        }

        public void ApplySpeakerDataToDialogueContainer(CharacterConfigData config)
        {
            Debug.Log(66666666);
            Debug.Log(config.dialogueColor);
            Debug.Log(config.dialogueFont);
            Debug.Log(config.nameColor);
            Debug.Log(config.nameFont);
            dialogueContainer.SetDialogueColor(config.dialogueColor);
            dialogueContainer.SetDialogueFont(config.dialogueFont);

            dialogueContainer.nameContainer.SetNameColor(config.nameColor);
            dialogueContainer.nameContainer.SetNameFont(config.nameFont);
        }
        public void ShowSpeakerName(string speakerName = "")
        {

            if (speakerName.ToLower() != "narrator")
            {
                dialogueContainer.nameContainer.Show(speakerName);
            }
            else
            {
                HideSpeakerName();
            }


        }

        public void HideSpeakerName() => dialogueContainer.nameContainer.Hide();


        // public void Say(string speaker, string dialogue)
        // {
        //     List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
        //     Say(conversation);
        // }

        public Coroutine Say(string speaker, string dialogue)
        {
            List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
            return Say(conversation);
        }


        public Coroutine Say(List<string> conversation)
        {
            return conversationManager.StartConversation(conversation);
            // Debug.Log(1);
            // // Debug.Log(conversationManager.StartConversation);
            // Debug.Log(conversation);
        }


    }
}