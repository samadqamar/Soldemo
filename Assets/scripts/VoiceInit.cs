using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.Unity;
using Photon.Realtime;
using Photon.Pun;
using Photon.Voice.PUN;
public class VoiceInit : MonoBehaviour
{
    public Image mic;
    public Sprite micOn, micOff;
    public PhotonView view;
    public Recorder recorder;
    void Start()
    {
        recorder = gameObject.GetComponent<Recorder>();
    }


    void Update()
    {
        if (Input.GetKeyDown( KeyCode.P ))
        {
            if (view.IsMine)
            {
                if (mic.sprite == micOn)
                {
                    mic.sprite = micOff;
                    recorder.TransmitEnabled = false;
                }
                else
                {
                    mic.sprite = micOn;
                    recorder.TransmitEnabled = true;
                }
            }
        }
    }
}
