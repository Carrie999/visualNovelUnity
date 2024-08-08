using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;


namespace CHARACTERS
{
    public class CharacterManager : MonoBehaviour
    {

        public static CharacterManager instance { get; private set; }
        private Dictionary<string, Character> characters = new Dictionary<string, Character>();

        private CharacterConfigSO config;

        // public CharacterManager()
        // {

        //     config = DialogueSystem.instance.config.characterConfigurationAsset;

        //     //  if (DialogueSystem.instance != null && DialogueSystem.instance.config != null)
        //     // {
        //     //     config = DialogueSystem.instance.config.characterConfigurationAsset;
        //     // }
        //     // else
        //     // {
        //     //     Debug.LogError("DialogueSystem instance or its config is not initialized.");
        //     // }
        // }

        private void Awake()
        {
            instance = this;
            // Debug.Log(11111);
            // Debug.Log(DialogueSystem.instance);
            // Debug.Log(DialogueSystem.instance.config);
            config = DialogueSystem.instance.config.characterConfigurationAsset;


        }

        public CharacterConfigData GetCharacterConfig(string characterName)
        {
            return config.GetConfig(characterName);
        }



        public Character GetCharacter(string characterName, bool createIfDoesNotExist = false)
        {
            if (characters.ContainsKey(characterName.ToLower()))
                return characters[characterName.ToLower()];
            else if (createIfDoesNotExist)
                return CreateCharacter(characterName);
            return null;

        }

        public Character CreateCharacter(string characterName)
        {
            if (characters.ContainsKey(characterName.ToLower()))
            {
                Debug.LogWarning($"A Character called '{characterName}' already exists. Did not create the character.");
                return null;
            }

            CHARACTER_INFO info = GetCharacterInfo(characterName);
            Character character = CreateCharacterFromInfo(info);

            characters.Add(characterName.ToLower(), character);
            return character;
        }


        private Character CreateCharacterFromInfo(CHARACTER_INFO info)
        {
            CharacterConfigData config = info.config;
            if (config.characterType == Character.CharacterType.Text)
                return new Character_Text(info.name, config);
            if (config.characterType == Character.CharacterType.Sprite || config.characterType == Character.CharacterType.SpriteSheet)
                return new Character_Sprite(info.name, config);
            if (config.characterType == Character.CharacterType.Live2D)
                return new Character_Live2D(info.name, config);
            if (config.characterType == Character.CharacterType.Model3D)
                return new Character_Model3D(info.name, config);
            return null;

        }


        private CHARACTER_INFO GetCharacterInfo(string characterName)
        {
            CHARACTER_INFO result = new CHARACTER_INFO();
            result.name = characterName;
            result.config = config.GetConfig(characterName);
            // Debug.Log(11);
            // Debug.Log(result.config.alias);
            // Debug.Log(result.name);
            return result;
        }


        private class CHARACTER_INFO
        {
            public string name = "";
            public CharacterConfigData config = null;
        }


    }
}
