using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EyeTrigger : MonoBehaviour
{

    public Volume volume;
    private ChromaticAberration chromatic;

    public float chromaticLerpDuration;
    private float stareDuration;

    public GameObject _eyes;
    public PlayerCharacter _player;
    public int triggerID;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerCharacter>();

        if (volume.profile.TryGet<ChromaticAberration>(out chromatic))
        {
            Debug.Log("Success");
            chromatic.intensity.value = 0;
        }

        stareDuration = StareDurations.stareSeconds[triggerID];

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _eyes.BroadcastMessage("Stare", triggerID);
        StartCoroutine(LerpChromatic());
        _player.moveSpeed = _player.moveSpeed / 2;

    }

    private IEnumerator LerpChromatic()
    {
        float lerpElapsed = 0;
        float lerpDuration = stareDuration / 2;
        while (lerpElapsed < lerpDuration)
        {
            //chromatic.intensity.value = Mathf.Lerp(0, 1, lerpElapsed / lerpDuration);
            chromatic.intensity.value = Mathf.Sin((2 * Mathf.PI / lerpDuration) * lerpElapsed);
            lerpElapsed += Time.deltaTime;
            yield return null;
        }
        lerpElapsed = 0;

        //while (lerpElapsed < lerpDuration)
        //{
        //    chromatic.intensity.value = Mathf.Lerp(1, 0, lerpElapsed / lerpDuration);
        //    lerpElapsed += Time.deltaTime;
        //    yield return null;
        //}
    }

}

// (a, b, x) = a + (b -a ) * x
