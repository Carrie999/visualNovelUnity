using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CMD_Extention_Example : CMD_DatabaseExtension
{

    new public static void Extend(CommandDatabase database)
    {
        database.AddCommand("print", new Action(PrintDefaultMessage));
        database.AddCommand("print_lp", new Action<string>(PrintUsermessage));
        database.AddCommand("print_mp", new Action<string[]>(PrintLines));

        database.AddCommand("lambda", new Action(() => { Debug.Log("Printing a default message to console from lambda command."); }));
        database.AddCommand("lambda_lp", new Action<string>((arg) => { Debug.Log($"Log User Lambda Message: '{arg}'"); }));
        database.AddCommand("lambda_mp", new Action<string[]>((args) => { Debug.Log(string.Join(", ", args)); }));

        database.AddCommand("process", new Func<IEnumerator>(SimpleProcess));
        database.AddCommand("process_lp", new Func<string, IEnumerator>(LineProcess));
        database.AddCommand("process_mp", new Func<string[], IEnumerator>(MultiLineProcess));

        database.AddCommand("moveCharDemo", new Func<string, IEnumerator>(MoveCharacter));
        database.AddCommand("playSong", new Func<string, IEnumerator>(PlaySong));

        database.AddCommand("setBackground", new Func<string, IEnumerator>(SetBackground));
    }


    private static void PrintDefaultMessage()
    {
        Debug.Log("Printing a default message to console.");
    }


    private static void PrintUsermessage(string message)
    {
        Debug.Log($"User Message: '{message}'");
    }
    private static void PrintLines(string[] lines)
    {
        int i = 1;
        foreach (string line in lines)
        {
            Debug.Log($"{i++}. '{line}'");
        }
    }

    private static IEnumerator SimpleProcess()
    {
        for (int i = 1; i <= 5; i++)
        {
            Debug.Log($"Process Running... [{i}]");
            yield return new WaitForSeconds(1);
        }
    }

    private static IEnumerator LineProcess(string data)
    {
        if (int.TryParse(data, out int num))
            for (int i = 1; i <= num; i++)
            {
                Debug.Log($"Process Running.. [{i}]");
                yield return new WaitForSeconds(1);
            }
    }


    private static IEnumerator MultiLineProcess(string[] data)
    {
        foreach (string line in data)
        {
            Debug.Log($"Process Message: '{line}'");
            yield return new WaitForSeconds(0.5f);
        }

    }

    private static IEnumerator MoveCharacter(string direction)
    {
        bool left = direction.ToLower() == "left";
        //Get the variables I need. This would-be defined somewhere else.
        Transform character = GameObject.Find("Image").transform;
        float moveSpeed = 15;
        // Calculate the target position for the image
        float targetX = left ? 8 : -8;
        // Calculate the current position of the image
        float currentX = character.position.x;
        // Move the image gradually towards the target position
        while (Mathf.Abs(targetX - currentX) > 0.1f)
        {
            Debug.Log($"Moving character to {(left ? "left" : "right")} [{currentX}/{targetX}]");
            currentX = Mathf.MoveTowards(currentX, targetX, moveSpeed * Time.deltaTime);
            character.position = new Vector3(currentX, character.position.y, character.position.z);
            yield return null;
        }
    }

    private static IEnumerator SetBackground(string imageName)
    {

        // 查找名为 "background" 的 Raw Image 对象
        GameObject backgroundImageObj = GameObject.Find("Background");

        if (backgroundImageObj != null)
        {
            RawImage backgroundImage = backgroundImageObj.GetComponent<RawImage>();

            if (backgroundImage != null)
            {
                // 获取要设置的新 Texture，可以从资源中加载或者通过其他方式获取
                Texture newTexture = Resources.Load<Texture>(imageName);

                // 将新的 Texture 赋值给 Raw Image 的 texture 属性
                backgroundImage.texture = newTexture;
            }
            else
            {
                Debug.LogError("RawImage component not found on 'background' GameObject.");
            }
        }
        else
        {
            Debug.LogError("GameObject with name 'background' not found in the scene.");
        }

        // // 等待一帧，确保背景图片已经更新
        yield return null;



    }

    private static IEnumerator PlaySong(string songName)
    {
        AudioClip clip = Resources.Load<AudioClip>(songName); // 从Resources文件夹加载音频，确保音频文件在Assets/Resources文件夹下

        if (clip == null)
        {
            Debug.LogError($"Audio clip '{songName}' not found.");
            yield break;
        }

        // 创建一个新的游戏对象来播放音频
        GameObject audioPlayer = new GameObject("AudioPlayer");
        AudioSource audioSource = audioPlayer.AddComponent<AudioSource>();

        // 播放音频
        audioSource.clip = clip;
        audioSource.Play();

        // 等待音频播放完成
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        // 播放完成后销毁音频对象
        GameObject.Destroy(audioPlayer);
    }
}