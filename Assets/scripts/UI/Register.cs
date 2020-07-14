using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class Register : MonoBehaviour {

    public InputField Username;
    public InputField Password;
    public InputField ConfPassword;
    public InputField Email;

    public void CreateAccount()
    {
        if(Password.text == ConfPassword.text)
        {
            RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
            request.Username = Username.text;
            request.Password = ConfPassword.text;
            request.Email = Email.text;
            request.DisplayName = Username.text;

            PlayFabClientAPI.RegisterPlayFabUser(request, result =>
            {
                Alerts a = new Alerts();
                StartCoroutine(a.CreateNewAlert("Your Account " + result.Username + " Has been created!"));
            }, error =>
            {
                Alerts a = new Alerts();
            StartCoroutine(a.CreateNewAlert(error.ErrorMessage));
            });
        }
    }
}
