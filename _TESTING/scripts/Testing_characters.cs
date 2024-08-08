using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;
using DIALOGUE;

namespace TESTING
{

    public class Testing_characters : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // Character character = CharacterManager.instance.CreateCharacter("Elen");
            // Character Elen = CharacterManager.instance.CreateCharacter("Elen");
            // Character Stella = CharacterManager.instance.CreateCharacter("Stella");
            // Character Adam = CharacterManager.instance.CreateCharacter("Adam");
            StartCoroutine(Test());
        }



        IEnumerator Test()
        {
            Character Elen = CharacterManager.instance.CreateCharacter("Elen");
            Character Stella = CharacterManager.instance.CreateCharacter("Stella");
            List<string> lines = new List<string>()
            {
            // "elen \"Hi!\"",
            // "This is a line.",
            // "And another.",
            // "And a last one."

                "elen \"Hi!\"",
                " My name is Elen.",
                "what's your name?",
                " Oh, (wa 1) that's very nice."
            };
            // yield return DialogueSystem.instance.Say(lines);

            yield return Elen.Say(lines);


            List<string> lines1 = new List<string>()
            {
            // "elen \"Hi!\"",
            // "This is a line.",
            // "And another.",
            // "And a last one."
                "Stella: Hi, there!",
                "Stella: My name is Stella.",
                "Stella: what's your name?",
                "Stella: Oh, (wa 1) that's very nice."
            };
            // yield return DialogueSystem.instance.Say(lines);

            yield return Stella.Say(lines1);
            Debug.Log("Finished");
        }
        // Update is called once per frame

        // Update is called once per frame
        void Update()
        {

        }
    }

}
