using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cinemachine : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
