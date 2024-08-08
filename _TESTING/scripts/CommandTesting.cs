using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(Running());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CommandManager.instance.Execute("moveCharDemo", "left");
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CommandManager.instance.Execute("moveCharDemo", "right");
        }

    }


    IEnumerator Running()
    {
        yield return CommandManager.instance.Execute("print");
        yield return CommandManager.instance.Execute("print_lp", "hello world");
        yield return CommandManager.instance.Execute("print_mp", "line1", "line2", "line3", "line4");

        yield return CommandManager.instance.Execute("lambda");
        yield return CommandManager.instance.Execute("lambda_lp", "Hello Lambda!");
        yield return CommandManager.instance.Execute("lambda_mp", "Lambda1", "Lambda2", "Lambda3");

        yield return CommandManager.instance.Execute("process");
        yield return CommandManager.instance.Execute("process_lp", "3");
        yield return CommandManager.instance.Execute("process_mp", "process line 1", "process line 2", "process line 3");


        //Special Example

    }
}
