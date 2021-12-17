using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EyeTrigger : MonoBehaviour
{

    private Volume _volume;
    private ChromaticAberration chromatic;
    private Vignette vignette;

    private float currLightIntensity;
    private float currLightRadius;
    private float currAberration;
    private float currVignette;
    private float stareDuration;
    private float targetVignette;

    public GameObject _eyes;
    private PlayerCharacter _player;
    private Light2D _playerLight;
    private Animator _playerAnimator;
    public int triggerID;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerCharacter>();
        _playerAnimator = _player.GetComponent<Animator>();
        _playerLight = _player.GetComponentInChildren<Light2D>();
        _volume = FindObjectOfType<Volume>();

        if (_volume.profile.TryGet<ChromaticAberration>(out chromatic))
        {
            currAberration = chromatic.intensity.value;
        }
        if (_volume.profile.TryGet<Vignette>(out vignette))
        {
            currAberration = vignette.intensity.value;
        }

        stareDuration = StareDurations.stareSeconds[triggerID];
        targetVignette = 0.5f;
        currLightRadius = _playerLight.pointLightOuterRadius;
        currLightIntensity = _playerLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _eyes.BroadcastMessage("Stare", triggerID);
        StartCoroutine(LerpEffects());
        StartCoroutine(SlowPlayer(0.2f));
        StartCoroutine(DimPlayerLight(0.22f,0.22f));
    }

    private IEnumerator LerpEffects()
    {
        float lerpElapsed = 0;
        float lerpDuration = 1.6f;
        while (lerpElapsed < lerpDuration / 2)
        {
            chromatic.intensity.value = currAberration + 
                                        (1 - currAberration) * Mathf.Sin((Mathf.PI / lerpDuration) * lerpElapsed);
            vignette.intensity.value = currVignette +
                                       (targetVignette - currVignette) * Mathf.Sin((Mathf.PI / lerpDuration) * lerpElapsed);
            lerpElapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(stareDuration - lerpDuration);

        while (lerpElapsed < lerpDuration)
        {
            chromatic.intensity.value = currAberration + 
                                        (1 - currAberration) * Mathf.Sin((Mathf.PI / lerpDuration) * lerpElapsed);
            vignette.intensity.value = currVignette +
                                       (targetVignette - currVignette) * Mathf.Sin((Mathf.PI / lerpDuration) * lerpElapsed);
            lerpElapsed += Time.deltaTime;
            yield return null;
        }

    }

    private IEnumerator SlowPlayer(float multiplier)
    {
        _player.moveSpeed = _player.moveSpeed * multiplier;
        _playerAnimator.SetFloat("moveSpeed", multiplier);

        yield return new WaitForSeconds(stareDuration);

        _player.moveSpeed = _player.moveSpeed / multiplier;
        _playerAnimator.SetFloat("moveSpeed", 1);
    }

    private IEnumerator DimPlayerLight(float radiusOffset, float intensityOffset)
    {
        float lerpElapsed = 0;
        float lerpDuration = 1.6f;
        while (lerpElapsed < lerpDuration / 2)
        {
            _playerLight.pointLightOuterRadius = currLightRadius -
                                                 radiusOffset * Mathf.Sin((Mathf.PI / lerpDuration) * lerpElapsed);
            _playerLight.intensity = currLightIntensity -
                                     intensityOffset * Mathf.Sin((Mathf.PI / lerpDuration) * lerpElapsed);
            lerpElapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(stareDuration - lerpDuration);

        while (lerpElapsed < lerpDuration)
        {
            _playerLight.pointLightOuterRadius = currLightRadius -
                                                 radiusOffset * Mathf.Sin((Mathf.PI / lerpDuration) * lerpElapsed);
            _playerLight.intensity = currLightIntensity -
                                     intensityOffset * Mathf.Sin((Mathf.PI / lerpDuration) * lerpElapsed);
            lerpElapsed += Time.deltaTime;
            yield return null;
        }
    }

}
