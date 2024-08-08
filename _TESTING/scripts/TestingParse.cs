using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace TESTING
{
    public class TestingParse : MonoBehaviour
    {

        [SerializeField] private TextAsset file;
        // Start is called before the first frame update
        void Start()
        {
            SendFileToParse();

            // string line = "Speaker \"Dialogue Goes In Here!\" Command(arguments here)";
            // DialogueParser.Parse(line);
        }

        // Update is called once per frame
        void Update()
        {

        }


        void SendFileToParse()
        {
            List<string> lines = FileManager.ReadTextAsset("testFiles");

            foreach (string line in lines)
            {
                if (line == string.Empty)
                    continue;
                DIALOGUE_LINE dl = DialogueParser.Parse(line);
            }

        }
    }

}
