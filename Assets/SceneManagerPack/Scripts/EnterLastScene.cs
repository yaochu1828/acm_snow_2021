using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterLastScene : MonoBehaviour
{

    public GameObject _wind;

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            GameManager.instance.isLastScene = true;
            _wind.SetActive(false); //no blowing wind in last scene
            GameManager.instance.isblow = false;
            StopAllCoroutines();
        }
        GameManager.instance.isAtLake = false; // to end lake footstep sounds
    }

}
