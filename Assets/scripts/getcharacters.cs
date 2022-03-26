using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
using AdvancedPeopleSystem;
public class getcharacters : MonoBehaviour
{
    public List<string> playersNames;
    public List<GameObject> playersObject;
    public List<CharacterCustomization> characters;
    FirebaseStorage storage;
    StorageReference storageReference;
    public CharacterCustomization character;
    public string location;
    public int playerscount;
    public int playerscounting;
    public string apply;
    void Start()
    {
        playerscounting = PhotonNetwork.PlayerList.Length;
        Downloader();
    }
    public void Downloader()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            storage = FirebaseStorage.DefaultInstance;
            storageReference = storage.GetReferenceFromUrl( "gs://mrcrash-f9d8a.appspot.com/Characters/" + p.NickName );
            if (!Directory.Exists( Application.streamingAssetsPath + "/Characters" ))
            {
                Directory.CreateDirectory( Application.streamingAssetsPath + "/Characters" );
                Debug.Log( "directory created" );
            }
            string localfilelocation = "file://" + Application.streamingAssetsPath + "/Characters/" + p.NickName + ".json";
            if (!playersNames.Contains( p.NickName ))
            {
                playersNames.Add( p.NickName );
                playersObject.Add( GameObject.Find( p.NickName ) );
                storageReference.GetFileAsync( localfilelocation ).ContinueWithOnMainThread( task =>
                {
                    if (task.IsCompleted)
                    {
                        Debug.Log( "file downloaded for: " + p.NickName );
                        apply = "apply";
                        // i++;
                    }
                } );
            }
            
        }
        playerscounting = PhotonNetwork.PlayerList.Length;
    }
    public void applyCharacter()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
           
            if (playersObject[i] == null)
            {
                playersObject[i]=( GameObject.Find( playersNames[i] ) );
                character = playersObject[i].GetComponentInChildren<CharacterCustomization>();
                Debug.Log( "again added" );
            }
            else
            {
                character = playersObject[i].GetComponentInChildren<CharacterCustomization>();
                Debug.Log( "already added" );
            }
            
            
            location = string.Format( Application.streamingAssetsPath + "/Characters/" + playersNames[i] + ".json" );

            var data = File.ReadAllText( location );

            CharacterCustomizationSetup characterCustomizationSetup = CharacterCustomizationSetup.Deserialize( data, CharacterCustomizationSetup.CharacterFileSaveFormat.Json );

            characterCustomizationSetup.ApplyToCharacter( character );
            apply = " ";
            Debug.Log( "Character file "+ playersObject[i].name + " to:" + playersNames[i] );
        }
    }
    public void removePlayer()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (playersObject[i] == null)
            {
                playersObject.RemoveAt( i );
                playersNames.RemoveAt( i );
            }
            Debug.Log( "loops pos" + i );
        }
    }
        void Update()
    {
        playerscount = PhotonNetwork.PlayerList.Length;
        if (PhotonNetwork.PlayerList.Length > playerscounting)
        {
            Debug.Log( "new player joined" );
            Downloader();
        }
        if (PhotonNetwork.PlayerList.Length < playerscounting)
        {
            Debug.Log( "someone leaved" );
            removePlayer();
        }
        if (apply == "apply")
        {
            applyCharacter();
        }
        
        
    }
}
