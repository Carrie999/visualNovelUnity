using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DIALOGUE
{
    public class ConversationManager
    {
        // public bool isRunning = false;
        // Start is called before the first frame update



        private DialogueSystem dialogueSystem = DialogueSystem.instance;
        private Coroutine process = null;

        public bool isRunning => process != null;

        private TextArchitect architect = null;
        private bool userPrompt = false;

        public ConversationManager(TextArchitect architect)
        {
            this.architect = architect;
            dialogueSystem.onUserPrompt_Next += OnUserPrompt_Next;
        }

        public Coroutine StartConversation(List<string> conversation)
        {
            StopConversation();
            process = dialogueSystem.StartCoroutine(RunningConversation(conversation));

            return process;
        }


        private void OnUserPrompt_Next()
        {
            userPrompt = true;
        }

        public void StopConversation()
        {
            if (!isRunning)
                return;
            dialogueSystem.StopCoroutine(process);
            process = null;
        }


        IEnumerator RunningConversation(List<string> conversation)
        {
            for (int i = 0; i < conversation.Count; i++)
            {

                if (string.IsNullOrWhiteSpace(conversation[i]))
                {
                    continue;
                }

                DIALOGUE_LINE line = DialogueParser.Parse(conversation[i]);


                if (line.hasDialogue)
                {
                    yield return Line_RunDialogue(line);
                }
                if (line.hasCommands)
                {
                    yield return Line_RunCommands(line);
                }
                // yield return new WaitForSeconds(0);
                if (line.hasDialogue)
                    yield return WaitForUserInput();
            }

        }


        IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
        {
            if (line.hasSpeaker)
                dialogueSystem.ShowSpeakerName(line.speakerData);
            // else
            // dialogueSystem.HideSpeakerName();


            // yield return BuildDialogue(line.dialogue);
            yield return BuildLineSegments(line.dialogueData);

            // yield return WaitForUserInput();

        }

        IEnumerator Line_RunCommands(DIALOGUE_LINE line)
        {
            // Debug.Log(line.commandData);
            List<DL_COMMAND_DATA.Command> commands = line.commandData.commands;
            foreach (DL_COMMAND_DATA.Command command in commands)
            {

                if (command.waitForCompletion)
                    yield return CommandManager.instance.Execute(command.name, command.arguments);
                else
                    CommandManager.instance.Execute(command.name, command.arguments);
            }

            yield return null;
        }

        IEnumerator BuildLineSegments(DL_DIALOGUE_DATA line)
        {
            for (int i = 0; i < line.segments.Count; i++)
            {
                DL_DIALOGUE_DATA.DIALOGUE_SEGMENT segment = line.segments[i];
                yield return WaitForDialogueSegmentSignalToBeTriggered(segment);

                yield return BuildDialogue(segment.dialogue, segment.appendText);
            }
        }


        IEnumerator WaitForDialogueSegmentSignalToBeTriggered(DL_DIALOGUE_DATA.DIALOGUE_SEGMENT segment)
        {
            switch (segment.startSignal)
            {
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.C:
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.A:
                    yield return WaitForUserInput();
                    break;
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.WC:
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.WA:
                    yield return new WaitForSeconds(segment.signalDelay);
                    break;
                default:
                    break;
            }
        }


        IEnumerator BuildDialogue(string dialogue, bool append = false)
        {
            if (!append)
            {
                architect.Build(dialogue);
            }
            else
            {
                architect.Append(dialogue);
            }


            while (architect.isBuilding)
            {
                if (userPrompt)
                {
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else architect.ForceComplete();
                    userPrompt = false;
                }
                yield return null;
            }
        }

        IEnumerator WaitForUserInput()
        {
            while (!userPrompt)
                yield return null;
            userPrompt = false;
        }

    }
}
