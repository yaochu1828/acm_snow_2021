using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyelid : MonoBehaviour
{
    public Animator _eyelidAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("e"))
        {
            _eyelidAnimator.SetBool("Open", false);
            _eyelidAnimator.SetBool("Close", true);
        }
        if (Input.GetKeyUp("e"))
        {
            _eyelidAnimator.SetBool("Open", true);
            _eyelidAnimator.SetBool("Close", false);
        }
    }
}
