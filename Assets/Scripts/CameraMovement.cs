using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private float m_xMin, m_xMax;
    public float m_yMin, m_yMax;
    public float m_xCamOffset = 7;
    public float m_yCamOffset = -4;

    public GameObject m_startGate;
    public GameObject m_endGate;

    private GameObject m_player;

	// Use this for initialization
	void Start ()
    {
		m_player = GameObject.FindGameObjectWithTag ("Player");

        m_xMin = m_startGate.transform.position.x + 8;
        m_xMax = m_endGate.transform.position.x - 6;


    }
	
	// Update is called after the physics Update
	void LateUpdate ()
    {

        if (m_player.GetComponent<PlayerHealth>().m_isDead == false)
        {
            // Move the Camera with the player 
            // Between fixed points
            float x = (m_player.transform.position.x + m_xCamOffset);
            float y = (m_player.transform.position.y + m_yCamOffset);

            x = Mathf.Clamp(x, m_xMin, m_xMax);
            y = Mathf.Clamp(y, m_yMin, m_yMax);

            gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
        }
        
	}
}
