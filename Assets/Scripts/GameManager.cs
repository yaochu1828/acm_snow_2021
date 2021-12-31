using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
    private bool firstTimeFall = true;
    private bool firstTimeBlown = true;
    public bool dropStickFromEye = false;
    public bool isFirstFrame = false;
    public bool isAtLake = false;
    public bool isLastScene = false;
    public bool isHug = false;
    public bool picking = false;

    private PlayerCharacter _player;
    private Canvas _worldSpaceCanvas;
    private TextMeshProUGUI _text;
    public Canvas _credits;

    public bool playerEyesClosed = false;
    public bool enemyEyesOpened = false;

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
        _player = FindObjectOfType<PlayerCharacter>();
        _worldSpaceCanvas = _player.gameObject.GetComponentInChildren<Canvas>();
        _text = _worldSpaceCanvas.GetComponent<TextMeshProUGUI>();
    }
    

    void OperateStick()
    {
        if (isHold == false && isHug==false && canPick == true && Input.GetKeyDown(KeyCode.J))
        {
            _player.GetComponent<Animator>().SetTrigger("picked");
            audioSource.clip = fallClips[0];
            audioSource.Play();
            StartCoroutine(PickingUp());
        }
        if (isHold == true && isHug==false && picking==false)
        {
            if ((isFirstFrame==false && Input.GetKey(KeyCode.J)==false) || (isblow == true && playerDuck == false) || (dropStickFromEye == true && playerEyesClosed == false))
            {
                stick.GetComponent<Renderer>().enabled = true;
                audioSource.clip = fallClips[1];
                audioSource.Play();
                stickanim.SetBool("holdon", false);
                stickpos.parent = null;
                isHold = false;
                droppos_ins = droppos;
                StartCoroutine(StartDropping());
                /*
                if (firstTimeFall)
                {
                    StartCoroutine(TypeLine("I have to hold on to it"));
                    firstTimeFall = false;
                }
                */
                if (isblow && firstTimeBlown)
                {
                    StartCoroutine(TypeLine("[Hold Space to duck]"));
                    firstTimeBlown = false;
                }
            }
        }
    }

    private IEnumerator PickingUp()
    {
        picking = true;
        yield return new WaitForSeconds(0.5f);
        picking = false;
    }
    private IEnumerator StartDropping()
    {
        stickMove = true;
        yield return new WaitForSeconds(0.4f);
        stickMove = false;
    }
    private void OnLevelWasLoaded(int level)
    {
        _player.transform.position = spawnpos.position;
        isFirstFrame = true;
        StartCoroutine(WaitFrames());
        if (level == 2)
        {
            spawnpos.transform.position = new Vector2(spawnpos.transform.position.x, -8);
            isHold = true;
        }
        if (level == 3)
        {
            spawnpos.transform.position = GameObject.Find("placement").transform.position;
            isHold = true;
        }

    }


    private IEnumerator WaitFrames()
    {
        yield return new WaitForSeconds(1f);
        isFirstFrame = false;

    }
    void Update()
    {
        OperateStick();
    }


    private IEnumerator TypeLine(string message)
    {
        yield return new WaitForSeconds(1);
        _text.text = "";
        char[] lineByChar = message.ToCharArray();
        foreach (char character in lineByChar)
        {
            _text.text += character;
            yield return null; //goes throught the loop once per frame
        }
        yield return new WaitForSeconds(2);
        _text.text = "";
    }
}
