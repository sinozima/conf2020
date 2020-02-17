using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class tank_move : MonoBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public float m_Speed = 12f;                 // How fast the tank moves forward and back.
    public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
    public AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
    public AudioClip m_EngineIdling;            // Audio to play when the tank isn't moving.
    public AudioClip m_EngineDriving;           // Audio to play when the tank is moving.
    public float m_PitchRange = 0.2f;
    public Material tank_m;
    public Material tank_e;
    public PhotonView PhotonView;

    private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
    private string m_TurnAxisName;              // The name of the input axis for turning.
    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    private float move_v, move_v2;         // The current value of the movement input.
    private float turn_v, turn_v2;             // The current value of the turn input.
    private float m_OriginalPitch;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        // When the tank is turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;

        // Also reset the input values.
        move_v = 0f;
        turn_v = 0f;
    }


    private void OnDisable()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        // The axes names are based on player number.
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;
        // Store the original pitch of the audio source.
        m_OriginalPitch = m_MovementAudio.pitch;
        move_v = 0f;
        turn_v = 0f;


    }


    private void Update()
    {
        EngineAudio();
    }


    private void EngineAudio()
    {
        // If there is no input (the tank is stationary)...
        if (Mathf.Abs(move_v) < 0.1f && Mathf.Abs(turn_v) < 0.1f)
        {
            // ... and if the audio source is currently playing the driving clip...
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                // ... change the clip to idling and play it.
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        else
        {
            // Otherwise if the tank is moving and if the idling clip is currently playing...
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                // ... change the clip to driving and play.
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }


    private void FixedUpdate()
    {
        if (PhotonView.IsMine == false)
        {
            return;
        }
        if (turn_v > 1f)
        {
            turn_v = 1f;
        }
        else if (turn_v < -1f)
        {
            turn_v = -1f;
        }

        if (move_v > 1f)
        {
            move_v = 1f;
        }
        else if (move_v < -1f)
        {
            move_v = -1f;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            move_v += 0.05f;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            move_v -= 0.05f;
        }
        else
        {
            if (move_v > 0)
            {
                move_v -= 0.07f;
            }
            else if (move_v < 0)
            {
                move_v += 0.07f;
            }
        }


        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turn_v += 0.05f;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turn_v -= 0.05f;
        }
        else
        {
            if (turn_v > 0)
            {
                turn_v -= 0.12f;
            }
            else if (turn_v < 0)
            {
                turn_v += 0.12f;
            }
        }
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move();
        Turn();
    }


    private void Move()
    {
        if (move_v <= 0.15f && move_v >= -0.15f)
        {
            move_v2 = 0;
        }
        else
        {
            move_v2 = move_v;
        }
        Vector3 movement = transform.forward * move_v2 * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn()
    {
        if (turn_v <= 0.15f && turn_v >= -0.15f)
        {
            turn_v2 = 0;
        }
        else
        {
            turn_v2 = turn_v;
        }
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = turn_v2 * m_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}
