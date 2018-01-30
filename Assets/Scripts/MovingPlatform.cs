using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Transform m_StartPoint;
    public Transform m_endPoint;
    public GameObject m_blocker;
    public float m_speed = 1.0f;

    private bool m_moveUp = false;
    
    // Use this for initialization
    void Start ()
    {
        this.transform.position = m_StartPoint.position;
        
        m_moveUp = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
		if(m_moveUp == true)
        {
            Vector3 pos = this.transform.position;
            pos.y += m_speed * Time.deltaTime;

            if(pos.y <= m_endPoint.position.y)
            {
                pos.y = m_endPoint.position.y;
                m_moveUp = false;
                DestroyObject(m_blocker);
                
            }

            this.transform.position = pos;
        }
	}

    public void BeginMove()
    {
        // begin moving up
        m_moveUp = true;
    }
    


}
