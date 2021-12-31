using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class LevelExit : MonoBehaviour
{

    private RoomManager _roomControl;
    public GameObject _wind;

    void Start()
    {
        _roomControl = FindObjectOfType<RoomManager>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            _wind.SetActive(true);
        }
            
    }

}
