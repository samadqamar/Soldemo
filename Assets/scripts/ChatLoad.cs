using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChatLoad : MonoBehaviour
{
    public GameObject chat;
    public void open()
    {
    
    }
    public void close ()
    {
        chat.SetActive( false );
    }
    public void Update()
    {
        if (Input.GetKeyDown( KeyCode.M ))
        {
            chat.SetActive( true );
        }
    }
}
