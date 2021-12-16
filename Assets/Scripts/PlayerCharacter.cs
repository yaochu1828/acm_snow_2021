using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public bool inCutScene { get; set; }
    public float moveSpeed;
    private Vector2 movement;
    public Animator anim;

    private Rigidbody2D _body;
    //private PlayerDialogue _dialogue;


    //private SpriteRenderer _renderer;
    //private Animator _animator;
    
    void Awake()
    {
        inCutScene = false;
    }

    // Start is called once the moment when the object is instantiated
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY);
        movement = movement.normalized;
        anim.SetFloat("walking", Mathf.Abs(moveX)+Mathf.Abs(moveY));

    }

    void Move()
    {
        _body.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }

    void Update()
    {
        if (!inCutScene)
        {
            ProcessInputs();
        }
    }

    void FixedUpdate()
    {
        // read movement input
        if (!inCutScene)
        {
            Move();
        }

    }


}
