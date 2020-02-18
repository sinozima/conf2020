using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map_tank_move : MonoBehaviour
{
    public camera_move camera_move;
    public Vector3 tank_pos;
    public Vector3 map_tank_pos;
    public GameObject map_tank;
    public Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (camera_move.target_tank == true)
        {
            tank_pos = camera_move.target_tank.transform.position;
            map_tank_pos.x = tank_pos.z / 45 * 1.9f;
            map_tank_pos.y = tank_pos.x / 45 * -1.5f;
            map_tank_pos.z = -111f;
            
            map_tank.transform.localPosition = map_tank_pos;
            map_tank.transform.localRotation = Quaternion.Euler(0, 0, -1* camera_move.target_tank.transform.localEulerAngles.y + 180);
        }
    }
}
