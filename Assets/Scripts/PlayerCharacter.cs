using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    public bool inCutScene { get; set; }
    public float moveSpeed = 1;
    private Vector2 movement;
    public Animator anim;
    private bool isDuck = false;
    public Transform holdpos;

    private Rigidbody2D _body;
    private StickMotion _stick;
    //private PlayerDialogue _dialogue;

    private AudioSource audioSource;
    private Scene currentScene;

    private string sceneName;

    //private SpriteRenderer _renderer;
    //private Animator _animator;
    [SerializeField]
    private AudioClip[] groundclips;
    public AudioClip[] lakeclips;


    void Awake()
    {
        inCutScene = false;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once the moment when the object is instantiated
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        audioSource = GetComponent<AudioSource>();
        _stick = FindObjectOfType<StickMotion>();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY);
        movement = movement.normalized;
        anim.SetFloat("walking", Mathf.Abs(moveX)+Mathf.Abs(moveY));
    }
    void ProcessDucking()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("ducking", true);
            isDuck = true;
            GameManager.instance.playerDuck = true;
            //_stick.gameObject.GetComponent<Renderer>().enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("ducking", false);
            isDuck = false;
            GameManager.instance.playerDuck = false;
            //_stick.gameObject.GetComponent<Renderer>().enabled = true;
        }
    }
    public void StickToHand()
    {
        _stick.gameObject.transform.SetParent(gameObject.transform);
        _stick.gameObject.transform.localPosition = new Vector2(holdpos.localPosition.x, holdpos.localPosition.y);
        GameManager.instance.picking = false;
        GameManager.instance.isHold = true;
        GameManager.instance.canPick = false;
        _stick.GetComponent<Animator>().SetBool("holdon", true);
    }
    public void SettingStick()
    {
        if (GameManager.instance.isHold == true)
            _stick.gameObject.GetComponent<Renderer>().enabled = false;
    }

    public void DuckingStick()
    {
        if(GameManager.instance.isHold==true)
            _stick.gameObject.GetComponent<Renderer>().enabled = true;
    }

    void Move()
    {
        _body.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }

    public void StepAudio()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        if (GameManager.instance.isAtLake == true)
            return lakeclips[Random.Range(0, lakeclips.Length)];
        else
        {
            return groundclips[Random.Range(0, groundclips.Length)];
        }
    }

    public void ChangeSpeed(float multiplier, bool reset = false)
    {
        moveSpeed *= multiplier;
        if (reset)
        {
            moveSpeed = 1;
            anim.SetFloat("moveSpeed", 1);
        }
        else
        {
            anim.SetFloat("moveSpeed", multiplier);
        }
    }

    void Update()
    {
        if (!inCutScene)
        {
            ProcessInputs();
            ProcessDucking();
        }
    }

    void FixedUpdate()
    {
        if(isDuck)
        {
            _body.velocity = new Vector2(0, 0);
        }
        // read movement input
        if (!inCutScene && !isDuck)
        {
            Move();
        }

    }


}
