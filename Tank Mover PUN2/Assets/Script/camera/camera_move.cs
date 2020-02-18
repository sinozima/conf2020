using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class camera_move : MonoBehaviour
{
    public GameObject target_tank;
    public Text tank_health_text;
    public float camera_movespeed;
    public room_make room_make;
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor;
    public Color m_ZeroHealthColor;
    public int health;
    Vector3 campos;
    void Start()
    {
        transform.parent = null;
        transform.rotation = Quaternion.Euler(60, -45, 0);
    }

    // Update is called once per framecampos.x += 8f;
    //campos.y += 19f;
    //      campos.z += -8f;
    void Update()
    {
        tank_set();
        
    }
    void FixedUpdate()
    {
        if (target_tank == true)
        {
            campos = Vector3.Slerp(this.transform.position, target_tank.transform.position, Time.deltaTime * camera_movespeed);
            transform.position = campos;
        }
    }

    public void tank_set()
    {
        if (target_tank == true)
        {
            health = target_tank.GetComponent<tank_health>().health;
            tank_health_text.text = "" + health;

            m_Slider.value = (float)health;

            m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, health / 100f);
            if (health == 0)
            {
                target_tank.GetComponent<tank_health>().destory();

                room_make.restart();
            }
        }
    }
}
