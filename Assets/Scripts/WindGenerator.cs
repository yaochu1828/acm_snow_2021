using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGenerator : MonoBehaviour
{
    private ParticleSystem particle;
    private AudioSource audioSource;
    private PlayerCharacter _player;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    private void Start()
    {
        _player = FindObjectOfType<PlayerCharacter>();
        particle = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        particle.Stop();
        Invoke("windBlow", 3f);
    }

    void windBlow()
    {
        float randomTime = Random.Range(4, 6);
        if (!particle.isPlaying)
        {
            particle.Play();
            audioSource.Play();
            _player.ChangeSpeed(0.4f);
            Invoke("blowCall", 1f);
            Invoke("windBlow", randomTime - 1);
        }
        else if (particle.isPlaying)
        {
            particle.Stop();
            audioSource.Stop();
            _player.ChangeSpeed(1/0.4f,true);
            GameManager.instance.isblow = false;
            Invoke("windBlow", randomTime);
        }
    }

    void blowCall()
    {
        GameManager.instance.isblow = true;
    }
}
