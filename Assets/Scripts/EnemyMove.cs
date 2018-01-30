using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : UnityMove
{

    private float m_hitDistance = 0.7f;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = GetFirstRaycastHit(new Vector2(m_xMoveDirection, 0),2);// Physics2D.Raycast(transform.position, new Vector2(m_xMoveDirection, 0));
        if(hit.distance <= m_hitDistance)
        {
            if (hit.collider != null && hit.collider.tag == "Obstical")
            {
                ChangeDirection();
            }
        }

        Move();
    }
}
