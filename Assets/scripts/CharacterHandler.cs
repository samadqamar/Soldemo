using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Photon.Pun;
using Photon.Realtime;
using System.Globalization;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
using AdvancedPeopleSystem;
using UnityEngine;
using AdvancedPeopleSystem;


    public class CharacterHandler : MonoBehaviourPunCallbacks

    {
    public PhotonView view;
    /*        public GameObject play;
            public PhotonView view;
            public List<string> playersNames;
            public List<GameObject> playersObject;
            public GameObject[] players;
            FirebaseStorage storage;
            StorageReference storageReference;
            public CharacterCustomization character;
            public string location;
        public string name;
        int index;
        int i = 0;
        //SavedCharacterData data = null;
        void LoadLastSavedData()
            {
                var saveDatas = character.GetSavedCharacterDatas();
                character.ApplySavedCharacterData( saveDatas[saveDatas.Count - 1] );
            }*/
    void Start()
        {
        gameObject.name = view.Owner.NickName;
       /* players = GameObject.FindGameObjectsWithTag( "OtherPlayer" );
        playersNames = new List<string>(); 
            foreach (Player p in PhotonNetwork.PlayerList)
            {
            *//*if (i == PhotonNetwork.PlayerList.Length) 
            {
             //   applyCharacter();
            }
            //last element 

            else
            {*//*
                playersObject.Add( GameObject.Find( p.NickName ) );
                storage = FirebaseStorage.DefaultInstance;
                storageReference = storage.GetReferenceFromUrl( "gs://mrcrash-f9d8a.appspot.com/Characters/" + p.NickName );
                if (!Directory.Exists( Application.streamingAssetsPath + "/Characters" ))
                {
                    Directory.CreateDirectory( Application.streamingAssetsPath + "/Characters" );
                    Debug.Log( "directory created" );
                }
                string localfilelocation = "file://" + Application.streamingAssetsPath + "/Characters/" + p.NickName + ".json";

                storageReference.GetFileAsync( localfilelocation ).ContinueWithOnMainThread( task =>
                {
                    if (task.IsCompleted)
                    {
                        //name = "go";
                        i++;
                    }
                } );
                
            //}
           
            }*/

        }
    /*    public void applyCharacter()
        {
        play = GameObject.FindGameObjectWithTag( "Player" );
        character = play.GetComponentInChildren<CharacterCustomization>();
        players = GameObject.FindGameObjectsWithTag( "OtherPlayer" );
        location = string.Format( Application.streamingAssetsPath + "/Characters/" + PlayerPrefs.GetString("username") + ".json" );
        var data1 = File.ReadAllText( location );
        CharacterCustomizationSetup characterCustomizationSetup1 = CharacterCustomizationSetup.Deserialize( data1, CharacterCustomizationSetup.CharacterFileSaveFormat.Json );
        characterCustomizationSetup1.ApplyToCharacter( character );
        foreach (Player p in PhotonNetwork.PlayerListOthers)
        {
            character = players[index].GetComponent<CharacterCustomization>();
            location = string.Format( Application.streamingAssetsPath + "/Characters/" + p.NickName + ".json" );
            var data = File.ReadAllText( location );
            CharacterCustomizationSetup characterCustomizationSetup = CharacterCustomizationSetup.Deserialize( data, CharacterCustomizationSetup.CharacterFileSaveFormat.Json );
            characterCustomizationSetup.ApplyToCharacter( character );
            index++;
        }



    }       
        void Update()
        {
            if (Input.GetKeyDown( KeyCode.J ))
            {
          //      StartCoroutine( LoadBanda() );
            }
            if (name=="go")
            {
            applyCharacter();
            name = null;
            }
        }*/
    }