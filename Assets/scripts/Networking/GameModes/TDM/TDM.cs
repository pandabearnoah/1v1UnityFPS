using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using UnityEngine.UI;

public class TDM : MonoBehaviourPun, IPunObservable {

    public GameObject LobbyCam;
    public Text SpawnCounter;

    public float spawnTime;
    float timer;
    bool hasPlayerSpawned = false;

    void Start()
    {
        LobbyCam.SetActive(true);
    }

    void Update()
    {
        timer += Time.deltaTime;
        SpawnCounter.text = "Spawning in: " + Mathf.Round(spawnTime - timer);
        if(timer >= spawnTime)
        {
            if (!hasPlayerSpawned)
            {
                LobbyCam.SetActive(false);
                SpawnCounter.gameObject.SetActive(false);
                PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);
                hasPlayerSpawned = true;
            }
            timer = 0;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

        }
        else if(stream.IsReading)
        {

        }
    }
}
