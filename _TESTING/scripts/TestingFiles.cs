using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingFiles : MonoBehaviour
{
    // private string fileName = "testFile";
    [SerializeField] private TextAsset fileName;


    void Start()
    {
        // Debug.Log(1);
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        // List<string> lines = FileManager.ReadTextFile(fileName, true);
        List<string> lines = FileManager.ReadTextAsset(fileName, true);

        foreach (string line in lines)
        {
            // Debug.Log(2);
            Debug.Log(line);
        }

        yield return null;


    }

}
