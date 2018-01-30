using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : UnityMove {

    private float m_hitDistance = 20;
    private bool m_startToMove = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = GetFirstRaycastHit(new Vector2(m_xMoveDirection, 0), 0);
        if (hit.distance <= m_hitDistance)
        {
            if (hit.collider != null && hit.collider.tag == "Player")
            {
                m_startToMove = true;
            }
        }

        if (m_startToMove)
            Move();
    }
}