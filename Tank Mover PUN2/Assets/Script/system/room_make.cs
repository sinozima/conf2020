using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class room_make : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public camera_move camera_move;
    public string name;
    public GameObject namesetUI;
    public InputField name_field;
    public GameObject tankhealthUI;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        namesetUI.gameObject.SetActive(false);
        tankhealthUI.gameObject.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        namesetUI.gameObject.SetActive(true);
    }
    public void OnClick()
    {
        namesetUI.gameObject.SetActive(false);
        tank_make();

        tankhealthUI.gameObject.SetActive(true);
    }
    public void SetPlayerName(string field_name)
    {
        name = name_field.text;
    }

    public void tank_make()
    {
        camera_move.target_tank = PhotonNetwork.Instantiate("Tank", new Vector3(Random.Range(-3f, 3f),2f, Random.Range(-3f, 3f)), Quaternion.identity);
        camera_move.target_tank.GetComponent<tank_ui_set>().nameset(name);
    }
}
