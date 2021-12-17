using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    public bool inCutScene { get; set; }
    public float moveSpeed;
    private Vector2 movement;
    public Animator anim;
    private bool isDuck = false;

    private Rigidbody2D _body;
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
    }

    // Start is called once the moment when the object is instantiated
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        audioSource = GetComponent<AudioSource>();
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
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("ducking", false);
            isDuck = false;
        }
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
        if (sceneName == "Lake")
            return lakeclips[Random.Range(0, lakeclips.Length)];
        else
        {
            return groundclips[Random.Range(0, groundclips.Length)];
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
