using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class Weapon : MonoBehaviourPun {

    public GameObject Bullet;
    public GameObject BulletSpawn;
    private string weaponName;
    private int weaponDamage;

    public Weapon(string weaponName, int weaponDamage)
    {
        this.weaponName = weaponName;
        this.weaponDamage = weaponDamage;
    }

    void Start()
    {
        foreach(Transform t in gameObject.transform)
        {
            if(t.tag == "Bullet")
            {
                //BulletSpawn = t.gameObject;
            }
        }
    }

    public void Shoot()
    {
        photonView.RPC("RPCShoot", RpcTarget.All);
    }

    [PunRPC]
    public void RPCShoot()
    {
        GameObject bullet = Instantiate(Bullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
        Rigidbody br = bullet.GetComponent<Rigidbody>();
        br.AddRelativeForce(Vector3.forward * 10000 * Time.deltaTime, ForceMode.Impulse);
    }
}
