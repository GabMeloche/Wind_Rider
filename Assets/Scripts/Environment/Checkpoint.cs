using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameObject m_player;

	void Start ()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

        if (m_player == null)
            Debug.LogError("Checkpoint(s) could not find GameObject with Player tag");
    }

    private void OnTriggerEnter(Collider p_other)
    {
        if (p_other.gameObject.GetComponentInParent<Rigidbody>().gameObject.CompareTag("Player"))
        {
            p_other.gameObject.GetComponentInParent<PlayerInput>().SetLastCheckpoint(transform);
        }
    }

}
