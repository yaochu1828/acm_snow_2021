using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public PlayerCharacter player;
    private Transform target;

    void Start()
    {
        target = player.gameObject.transform;
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10);
    }
}
