using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class LevelExit : MonoBehaviour
{

    private RoomManager _roomControl;

    void Start()
    {
        _roomControl = FindObjectOfType<RoomManager>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            Debug.Log("Exit Reached");
            _roomControl.NextScene();
        }
        else
        {
            Debug.Log("No collider");
        }
    }

}
