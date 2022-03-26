using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdvancedPeopleSystem;
public class loadCharacter : MonoBehaviour
{
    public CharacterCustomization character;
    void Update()
    {
        if (Input.GetKeyDown( KeyCode.S ))
        {
            SaveCharacterToFileSystem(); // Save character to filesystem
        }
        if (Input.GetKeyDown( KeyCode.L ))
        {
            LoadLastSavedData(); // Load last character save
        }
    }
    private void Start()
    {
        LoadLastSavedData();
    }
    void SaveCharacterToFileSystem()
    {
        character.SaveCharacterToFile( CharacterCustomizationSetup.CharacterFileSaveFormat.Json ); // Save character to default unity persistentDataPath
    }
    void LoadLastSavedData()
    {
        var saveDatas = character.GetSavedCharacterDatas(); // Get character saves from path (empty = persistentDataPath)
        character.ApplySavedCharacterData( saveDatas[saveDatas.Count - 1] ); // Apply last saved data to character
    }
}
