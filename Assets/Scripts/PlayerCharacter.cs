using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public bool inCutScene { get; set; }
    public float moveSpeed = 4.0f;
    private Vector2 movement;

    private Rigidbody2D _body;
    //private PlayerDialogue _dialogue;

    float last_input_x = 0;
    float last_input_y = 0;

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

    void Update()
    {
        // read movement input
        if (!inCutScene)
        {
            if (Input.GetAxisRaw("Horizontal") == 0) {
                movement.y = Input.GetAxisRaw("Vertical");
            }
            if (Input.GetAxisRaw("Vertical") == 0) {
                movement.x = Input.GetAxisRaw("Horizontal");
            }
            movement = movement.normalized;

            // animation

            //unit_vector_from_input * speed * time is basically multiplying each element in the vector by a scalar
            _body.MovePosition(_body.position + movement * moveSpeed * Time.fixedDeltaTime);

        }

    }


}
