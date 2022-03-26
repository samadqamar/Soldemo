using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PhotonInit : MonoBehaviourPunCallbacks, IMatchmakingCallbacks
{
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

    }
    public void setname()
    {
        PhotonNetwork.NickName =PlayerPrefs.GetString( "username");
        
    }
    public void Join()
    {

        if (PhotonNetwork.IsConnected)
        {

            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log( "NOW CONNECTED" );
        /*if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }*/

    }
    public override void OnDisconnected( DisconnectCause cause )
    {
        Debug.Log( $"Disconnected because  {cause}" );
    }
    public void CreateRoom()
    {

      
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;
        PhotonNetwork.CreateRoom(  Random.Range(0,3).ToString(),roomOptions );
    }
    public void JoinedRoom()
    {
     //   PhotonNetwork.JoinRoom( JoinRoom.text );
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel( "Game" );
    }
    
    void IMatchmakingCallbacks.OnJoinRandomFailed( short returnCode, string message )
    {
        CreateRoom();
    }
}
