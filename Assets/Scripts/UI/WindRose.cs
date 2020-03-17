using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindRose : MonoBehaviour
{
    private GameObject m_player;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        Vector3 rotate = transform.eulerAngles;
        float angle = -Vector3.Angle(m_player.GetComponentInChildren<Sail>().GetWindDirection(), m_player.transform.forward);

        if (Vector3.Dot(m_player.GetComponentInChildren<Sail>().GetWindDirection(), m_player.transform.right) < 0)
            angle *= -1f;

        rotate.z = angle;
        transform.eulerAngles = rotate;
    }
}
