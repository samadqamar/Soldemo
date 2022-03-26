using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace UnityStandardAssets.Characters.ThirdPerson
{
    
    public class DisableInit : MonoBehaviourPun
    {
        PhotonView view;
        ThirdPersonUserControl user;
        ThirdPersonCharacter character;

        void Start()
        {
            view = GetComponent<PhotonView>();
            user = GetComponent<ThirdPersonUserControl>();
            character = GetComponent<ThirdPersonCharacter>();
            if (view.IsMine)
            {
                gameObject.tag = "Player";

            }
            else
            {
                Destroy( user );
                Destroy( character );
                Destroy( this );
            }
        }
    }
}