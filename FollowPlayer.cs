using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]private GameObject m_Player;
    [SerializeField]private Vector3 m_cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_Player.transform.position + m_cameraOffset;
    }
}
