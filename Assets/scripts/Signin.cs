using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using System;
using UnityEngine.SceneManagement;
using TMPro;
public class Signin : MonoBehaviour
{
  #region Signin
  public TMP_InputField SigninUsername;
  public TMP_InputField SignInpassword;
  bool SigninUiScreen = true;
  #endregion

  #region Regiser
  public TMP_InputField Username;
  public TMP_InputField RegisterEmail;
  public TMP_InputField Registerpassword;

  #endregion

  public GameObject warning, SigninUi, RegisterUi,form;





  String myUsername;
  String mypassword;
  //  public InputField password;
  DatabaseReference reference;
  void Start()
  {
    reference = FirebaseDatabase.DefaultInstance.RootReference;
  }
  public void Register()
  {

    reference.Child("Users").Child(Username.text).Child("Email").SetValueAsync(RegisterEmail.text);

    reference.Child("Users").Child(Username.text).Child("Password").SetValueAsync(Registerpassword.text);
    reference.Child("Users").Child(Username.text).Child("Username").SetValueAsync(Username.text);
        PlayerPrefs.SetString( "username", Username.text );
        Username.text = "";
    RegisterEmail.text = "";
    Registerpassword.text = "";
        toggleSigIn();
    }
  public void RetriveData()
  {
    FirebaseDatabase.DefaultInstance
      .GetReference("Users")
      .ValueChanged += Signin_ValueChanged;

  }

  void Signin_ValueChanged(object sender, ValueChangedEventArgs args)
  {

    if (args.Snapshot.HasChild(SigninUsername.text))
    {
      myUsername = args.Snapshot.Child(SigninUsername.text).Child("Username").GetValue(true).ToString();
      mypassword = args.Snapshot.Child(SigninUsername.text).Child("Password").GetValue(true).ToString();
      UserCheck(myUsername, mypassword);
    }
    else
    {
      warning.SetActive(true);
    }
  }

  private void UserCheck(String user, String pwd)
  {
    if (SigninUsername.text == user && SignInpassword.text == pwd)
    {
            // logged in
            form.SetActive( false );
            PlayerPrefs.SetString( "username", SigninUsername.text );
    }
    else
    {
      warning.SetActive(true);
      Debug.Log("Wrong Email Or Password");

    }

  }

  public void toggleSigIn()
  {
    if (SigninUi.activeInHierarchy)
    {
      SigninUi.SetActive(false);
      RegisterUi.SetActive(true);
    }
    else
    {
      SigninUi.SetActive(true);
      RegisterUi.SetActive(false);
    }
  }
}






