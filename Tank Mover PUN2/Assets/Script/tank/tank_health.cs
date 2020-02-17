using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class tank_health : MonoBehaviour
{
    public int health;
    public Slider tankhealth_slider;
    public PhotonView PhotonView;
    public Image tankhealth_image;
    public Color m_ZeroHealthColor;
    public Color m_FullHealthColor;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        tankhealth_slider.value = 100;
        tankhealth_image.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, 100 / 100f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
    
    public void take_damage(int damage)
    {
        health -= damage;
        PhotonView.RPC("minitankhealth_set", RpcTarget.AllBuffered, health);
    }

    public void destory()
    {
        PhotonNetwork.Destroy(this.gameObject);
    }
    [PunRPC]
    public void minitankhealth_set(int healthvalue)
    {
        tankhealth_slider.value = healthvalue;
        tankhealth_image.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, healthvalue / 100f);
    }
}
