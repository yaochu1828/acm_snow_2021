using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public GameObject stick;
    public Transform stickpos;
    public Transform holdpos;
    public Transform droppos;
    public Transform droppos_ins;
    public bool isHold = false;
    public bool canPick = false;
    public Animator stickanim;
    public bool stickMove = false;
    public Transform spawnpos;
    public bool isblow;
    public bool playerDuck;
    public AudioSource audioSource;
    public AudioClip[] fallClips;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    private void Start()
    {
        stickpos = stick.GetComponent<Transform>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void operateStick()
    {
        if (isHold == false && canPick == true && Input.GetKeyDown(KeyCode.J))
        {
            audioSource.clip = fallClips[0];
            audioSource.Play();
            stickpos.SetParent(player.transform);
            stickpos.localPosition = new Vector2(holdpos.localPosition.x, holdpos.localPosition.y);
            isHold = true;
            canPick = false;
            stickanim.SetBool("holdon", true);
        }
        if (isHold == true && Input.GetKeyUp(KeyCode.J) || (isHold==true && isblow==true && playerDuck == false))
        {
            audioSource.clip = fallClips[1];
            audioSource.Play();
            stickanim.SetBool("holdon", false);
            stickpos.parent = null;
            isHold = false;
            droppos_ins = droppos;

            stickMove = true;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            spawnpos.transform.position = new Vector2(spawnpos.transform.position.x, -8);
            isHold = true;
        }

        player.transform.position = spawnpos.position;
    }
    void Update()
    {
        operateStick();
    }
}
