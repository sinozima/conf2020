using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class shell : MonoBehaviour
{
    public float shell_speed;
    public GameObject shell_core;
    public int attack_time=2;
    public ParticleSystem shell_ex;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * shell_speed, ForceMode.VelocityChange);
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "tank")
        {
            attack_time -= 1;
            
            if (attack_time == 0)
            {
                other.gameObject.GetComponent<tank_health>().take_damage(30);
                Destroy(this.gameObject);
            }
        }

        if (other.gameObject.tag == "ground")
        {
            Instantiate(shell_ex, this.transform.position, Quaternion.Euler(-90,0,0));
            Destroy(this.gameObject);
        }
    }
}
