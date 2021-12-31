using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StickMotion : MonoBehaviour
{
    private Transform stickpos;
    public float dropSpeed;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    private void Start()
    {
        stickpos = gameObject.GetComponent<Transform>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GameManager.instance.isHold == false)
        {
            GameManager.instance.canPick = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GameManager.instance.isHold == false)
        {
            GameManager.instance.canPick = false;
        }
    }
    private void Update()
    {
        if(GameManager.instance.stickMove == true)
        {
            stickpos.position = Vector2.MoveTowards(stickpos.position, GameManager.instance.droppos_ins.position, dropSpeed * Time.deltaTime);
        }
    }


    void DropStop()
    {
        GameManager.instance.stickMove = false;
    }
}
