using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class tank_shell : MonoBehaviour
{
    public GameObject tank_core;
    public float reload;
    public PhotonView PhotonView;
    private float reload_wait_time;
    public GameObject shell;
    // Start is called before the first frame update
    void Start()
    {
        PhotonView = GetComponent<PhotonView>();
        reload_wait_time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        reload_wait_time += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if (PhotonView.IsMine == true)
            {
                if (reload_wait_time >= reload)
                {
                    reload_wait_time = 0;
                    PhotonView.RPC("shell_start", RpcTarget.AllViaServer,tank_core.transform.position, tank_core.transform.eulerAngles.y);
                }
            }
        }
    }
    [PunRPC]
    private void shell_start(Vector3 pos,float ang)
    {
        Quaternion rot = Quaternion.Euler(0.0f, ang, 0.0f);
        Instantiate(shell, tank_core.transform.position, rot);
    }
}
