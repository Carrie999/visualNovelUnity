using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;
public class TestingConversation : MonoBehaviour
{
    void Start()
    {
        StartConversation();

        // string line = "Speaker \"Dialogue Goes In Here!\" Command(arguments here)";
        // DialogueParser.Parse(line);
    }

    void StartConversation()
    {
        List<string> lines = FileManager.ReadTextAsset("conversation");

        DialogueSystem.instance.Say(lines);

        // foreach (string line in lines)
        // {

        //     if (string.IsNullOrEmpty(line))
        //         continue;
        //     Debug.Log($"Segmenting line '{line}'");
        //     DIALOGUE_LINE dlLine = DialogueParser.Parse(line);
        //     int i = 0;
        //     foreach (DL_DIALOGUE_DATA.DIALOGUE_SEGMENT segment in dlLine.dialogue.segments)
        //     {
        //         Debug.Log($"Segment [{i++}] = '(segment.dialogue)' [signal={segment.startSignal.ToString()}{(segment.signalDelay > 0 ? $"{segment.signalDelay}" : $"")}]");
        //     }
        // }


        // foreach (string line in lines)
        // {
        //     // Debug.Log(222);
        //     if (string.IsNullOrWhiteSpace(line))
        //         continue;
        //     DIALOGUE_LINE dl = DialogueParser.Parse(line);
        //     // Debug.Log(dl);
        //     // Debug.Log(222);
        //     // Debug.Log(dl.commandData);
        //     // Debug.Log(333);
        //     Debug.Log(dl.commandData.commands);
        //     for (int i = 0; i < dl.commandData.commands.Count; i++)
        //     {
        //         DL_COMMAND_DATA.Command command = dl.commandData.commands[i];
        //         Debug.Log($"Command - [{i}] - '{command.name}' has arguments [{string.Join(", ", command.arguments)}]");

        //     }
        // }
    }
}
