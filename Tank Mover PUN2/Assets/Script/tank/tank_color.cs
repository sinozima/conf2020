using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class tank_color : MonoBehaviour
{
    public Material tank_material;
    public int id;
    public PhotonView PhotonView;
    public Renderer tank_re_1;
    public Renderer tank_re_2;
    public float color_r,color_b,color_g;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonView.IsMine == true)
        {
            Color_set();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    void Color_set()
    {
        int color_random = Random.Range(0, 3);
        int color_random2;
        int color_random3 = Random.Range(29, 231);
        if (color_random == 0)
        {
            color_random2 = Random.Range(0, 2);
            if (color_random2 == 0)
            {
                color_r = 29f / 255f;
                color_g = 231f / 255f;
                color_b = color_random3 / 255f;
            }
            if (color_random2 == 1)
            {
                color_r = 29f / 255f;
                color_g = color_random3 / 255f;
                color_b = 231f / 255f;
            }
        }
        if (color_random == 1)
        {
            color_random2 = Random.Range(0, 2);
            if (color_random2 == 0)
            {
                color_r = 231f / 255f;
                color_g = 29f / 255f;
                color_b = color_random3 / 255f;
            }
            if (color_random2 == 1)
            {
                color_r = color_random3 / 255f;
                color_g = 29f / 255f;
                color_b = 231f / 255f;
            }
        }
        if (color_random == 2)
        {
            color_random2 = Random.Range(0, 2);
            if (color_random2 == 0)
            {
                color_r = 231f / 255f;
                color_g = color_random3 / 255f;
                color_b = 29f / 255f;
            }
            if (color_random2 == 1)
            {
                color_r = color_random3 / 255f;
                color_g = 231f / 255f;
                color_b = 29f / 255f;
            }
        }
        PhotonView.RPC(nameof(tank_color_set), RpcTarget.AllBuffered, color_r,color_g,color_b);
    }
    [PunRPC]
    private void tank_color_set(float color_rr, float color_gg, float color_bb)
    {
        tank_re_1.material.color = new Color(color_rr, color_gg, color_bb);
        tank_re_2.material.color = new Color(color_rr, color_gg, color_bb);
    }
}
