using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderManager : MonoBehaviour
{
    public Avatar male, female;
    public Animator anim;
    public GameObject maleBody,femaleBody;
    void Start()
    {if (PlayerPrefs.GetString( "gender" ) == "male")
        {
            anim.avatar = male;
            maleBody.SetActive( true );
        }
        else
        {
            anim.avatar = female;
            femaleBody.SetActive( true );
        }
        PlayerPrefs.SetString( "gender", "male" );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
