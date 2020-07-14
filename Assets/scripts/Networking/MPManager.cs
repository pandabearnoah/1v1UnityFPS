using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MPManager : MonoBehaviourPunCallbacks {

    public GameObject[] EnableObjectsOnConnect;
    public GameObject[] DisableOjectsOnConnect;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        foreach(GameObject obj in EnableObjectsOnConnect)
        {
            obj.SetActive(true);
        }

        foreach(GameObject obj in DisableOjectsOnConnect)
        {
            obj.SetActive(false);
        }

        Debug.Log("Connected to Master");
    }

    public void JoinTDM()
    {
        Debug.Log("Joining TDM");

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to Join TDM. Creating lobby.");
        CreateTDM();
    }

    public void CreateTDM()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        RoomOptions ro = new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true };
        PhotonNetwork.CreateRoom("defaultTDM", ro, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("TDM");
    }
}
