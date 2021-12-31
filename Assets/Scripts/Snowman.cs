using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour
{
    public GameObject _player;
    private Collider2D _collider;
    Animator anim;
    public StickMotion stick;
    public RoomManager sceneManager;
    public GameObject snowLight;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GameManager.instance.isHold)
        {
            GameManager.instance.isHug = true;
            snowLight.SetActive(true);
            stick.gameObject.SetActive(false);
            Destroy(_player);
            anim.SetBool("hug", true);
            //sceneManager.NextScene(); an alternative for ending (simple blacking out)
        }
            
    }
}
