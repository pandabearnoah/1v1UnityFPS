using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class SuperSync : MonoBehaviourPun, IPunObservable {

    public Vector3 objPosition;
    public Quaternion objRotation;
    public Vector3 objScale;

    public float LerpSpeed = 3f;

    public void Update()
    {
        if (!photonView.IsMine)
        {
            UpdateTransform();
        }
    }

    private void UpdateTransform()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, objPosition, LerpSpeed * Time.deltaTime);
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, objRotation, LerpSpeed * Time.deltaTime);
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, objScale, LerpSpeed * Time.deltaTime);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(gameObject.transform.position);
            stream.SendNext(gameObject.transform.rotation);
            stream.SendNext(gameObject.transform.localScale);
        }
        else if (stream.IsReading)
        {
            objPosition = (Vector3)stream.ReceiveNext();
            objRotation = (Quaternion)stream.ReceiveNext();
            objScale = (Vector3)stream.ReceiveNext();
        }
    }
}
