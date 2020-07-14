using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public void CreateAcc()
    {
        SceneManager.LoadSceneAsync("Register", LoadSceneMode.Additive);
    }

    public void LoginAcc()
    {
        SceneManager.LoadSceneAsync("Login", LoadSceneMode.Additive);
    }
}
