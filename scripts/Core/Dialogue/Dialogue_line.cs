using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class DIALOGUE_LINE
    {
        public string speakerData;
        public DL_DIALOGUE_DATA dialogueData;
        public DL_COMMAND_DATA commandData;


        public bool hasSpeaker => speakerData != string.Empty;
        // public bool hasSpeaker => speakerData != null;
        // public bool hasDialogue => dialogue != string.Empty;
        // public bool hasDialogue => dialogue.hasDialogue;
        public bool hasDialogue => dialogueData != null;
        // public bool hasCommands => commands != string.Empty;
        public bool hasCommands => commandData != null;


        public DIALOGUE_LINE(string speaker, string dialogue, string commands)
        {
            this.speakerData = speaker;
            // this.dialogue = new DL_DIALOGUE_DATA(dialogue);
            // this.dialogueData = (string.IsNullOrWhiteSpace(dialogue) ? null : new DL_DIALOGUE_DATA(dialogue));
            this.dialogueData = new DL_DIALOGUE_DATA(dialogue);
            // this.commandData = (string.IsNullOrWhiteSpace(commands) ? null : new DL_COMMAND_DATA(commands));
            this.commandData = new DL_COMMAND_DATA(commands);
        }

    }
}


