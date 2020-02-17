using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class tank_ui_set : MonoBehaviour
{
    public GameObject tanknameUI;
    public Text tanknameUI_text;
    public PhotonView PhotonView;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonView.IsMine == true)
        {
            tanknameUI_text.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        tanknameUI.gameObject.transform.rotation = Quaternion.Euler(60, -45, 0);
    }
    public void nameset(string name)
    {
        PhotonView.RPC(nameof(nameset_u), RpcTarget.AllBuffered,name);
    }
    [PunRPC]
    public void nameset_u(string name)
    {
        tanknameUI_text.text = name;
    }
}
