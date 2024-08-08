using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace TESTING
{
    public class Testing_architect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;
        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.fade;
        string[] lines = new string[5]
        {
            "The world is a crazy place sometimes",
            "Don't Lose hope",
            "things will get better!",
            "It's a bird? It's a plane? Nol - It's Super Sheltie!",
            "It's a bird? It's a plane? Nol - It's Super Sheltie2!"
        };
        // Start is called before the first frame update
        void Start()
        {
            // Debug.Log("start.");
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            // architect.buildMethod = TextArchitect.BuildMethod.instant;
            // architect.buildMethod = TextArchitect.BuildMethod.typewriter;
            architect.buildMethod = TextArchitect.BuildMethod.fade;

            architect.speed = 0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            if (bm != architect.buildMethod)
            {
                architect.buildMethod = bm;
                architect.Stop();
            }

            if (Input.GetKeyDown(KeyCode.S))
                architect.Stop();

            string longLine = "this is very longline, masfjaklsd hfhsdah fsdajf hsdajkf hdsajfh sdaf dsa ffsa dfhsdf hdsajlffjdsaf hdsajkf hsdfh sdkj fkjsdhgfurgfhurehuewur yewur hewhusfhsduhk hfs hfkudsghfjk dxh fuishfisa uhfdg sgsaf dsfasd sda. ";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Debug.Log("This is a log message.");


                if (architect.isBuilding)
                {
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();
                }
                else
                {
                    // architect.Build(lines[Random.Range(0, lines.Length)]);
                    architect.Build(longLine);

                }

            }




            // }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                // architect.Append(lines[Random.Range(0, lines.Length)]);
                architect.Append(longLine);
            }

        }


    }

}


