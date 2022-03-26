using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class PlayerName : MonoBehaviour
{
    public Text nameTxt;
    public PhotonView view;
    void Start()
    {
        nameTxt.text = view.Owner.NickName;
    }
}
