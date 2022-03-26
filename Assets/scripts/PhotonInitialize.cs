using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class PhotonInitialize : MonoBehaviour
{
    public Text txt;
    public Transform spawn;
    // Start is called before the first frame update
    void Start()
    {
       GameObject temp= PhotonNetwork.Instantiate( "Characters", spawn.position, Quaternion.identity );
        temp.name = PlayerPrefs.GetString( "username" );
        Debug.Log( PhotonNetwork.CountOfPlayers );
    }
    private void Update()
    {
        txt.text =(PhotonNetwork.PlayerList.Length).ToString();
    }
}
