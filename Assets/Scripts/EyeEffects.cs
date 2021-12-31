using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EyeEffects : MonoBehaviour
{
    public Volume _volume;
    private ChromaticAberration chromatic;
    private Vignette vignette;

    private PlayerCharacter _player;
    private Light2D _playerLight;
    private Animator _playerAnimator;

    private float currLightIntensity;
    private float currLightRadius;
    private float currAberration;
    private float currVignette;
    private float stareDuration;
    private float targetVignette;

    public float slowMultiplier;
    public float dimLightOffset;
    public float dimRadiusOffset;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerCharacter>();
        _playerAnimator = _player.GetComponent<Animator>();
        _playerLight = _player.GetComponentInChildren<Light2D>();

        if (_volume.profile.TryGet<ChromaticAberration>(out chromatic))
        {
            currAberration = chromatic.intensity.value;
        }
        else
        {
            Debug.Log("Could not access abberation");
        }
        if (_volume.profile.TryGet<Vignette>(out vignette))
        {
            currAberration = vignette.intensity.value;
        }
        else
        {
            Debug.Log("Could not access vignette");
        }

        
        targetVignette = 0.5f;
        currLightRadius = _playerLight.pointLightOuterRadius;
        currLightIntensity = _playerLight.intensity;
    }

    public void TriggerEffects(int triggerID)
    {
        stareDuration = StareDurations.stareSeconds[triggerID];
        StartCoroutine(EyesOpened());
        StartCoroutine(LerpEffects());
        StartCoroutine(DimPlayerLight(dimRadiusOffset,dimLightOffset));
        StartCoroutine(SlowPlayer(slowMultiplier));
    } 

    private IEnumerator EyesOpened()
    {
        GameManager.instance.enemyEyesOpened = true;
        yield return new WaitForSeconds(1f);
        GameManager.instance.dropStickFromEye = true;
        yield return new WaitForSeconds(stareDuration-1);
        GameManager.instance.enemyEyesOpened = false;
        GameManager.instance.dropStickFromEye = false;
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
        _player.ChangeSpeed(multiplier);
        yield return new WaitForSeconds(stareDuration);
        _player.ChangeSpeed(1 / multiplier, true);
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
