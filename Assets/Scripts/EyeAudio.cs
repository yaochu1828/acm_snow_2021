using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAudio : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.enemyEyesOpened == true)
        {
            audioSource.Play();
        }
        if(GameManager.instance.enemyEyesOpened == false)
        {
            audioSource.Stop();
        }
    }
}
