using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LakeEnterTrigger : MonoBehaviour
{
    public Volume _volume;
    private ChromaticAberration chromatic;
    private Vignette vignette;

    public float targetVignette = 0.25f;
    public float targetAberration = 0.25f;

    private PlayerCharacter _player;

    private bool trigger;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerCharacter>();

        if (_volume.profile.TryGet<ChromaticAberration>(out chromatic))
        {
            Debug.Log("Success");
            chromatic.intensity.value = 0;
        }

        if (_volume.profile.TryGet<Vignette>(out vignette))
        {
            Debug.Log("Success");
            vignette.intensity.value = 0;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (trigger)
        {
            trigger = false;
            Debug.Log("Triggered");
            StartCoroutine(SetChromatic());
            StartCoroutine(SetVignette());
        }
        
    }

    private IEnumerator SetChromatic()
    {
        float lerpElapsed = 0;
        float lerpDuration = 2;
        while (lerpElapsed < lerpDuration / 2)
        {
            chromatic.intensity.value = targetAberration * Mathf.Sin((Mathf.PI / lerpDuration) * lerpElapsed);
            lerpElapsed += Time.deltaTime;
            yield return null;
        }

    }

    private IEnumerator SetVignette()
    {
        float lerpElapsed = 0;
        float lerpDuration = 1.5f;
        while (lerpElapsed < lerpDuration / 2)
        {
            vignette.intensity.value = targetVignette * Mathf.Sin((Mathf.PI / lerpDuration) * lerpElapsed);
            lerpElapsed += Time.deltaTime;
            yield return null;
        }
    }

}
