using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Eye : MonoBehaviour
{

    public Sprite eyesClosed;
    public Sprite halfClosed;
    public List<Sprite> stares = new List<Sprite>();

    private Light2D _eyeLight;
    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _eyeLight = GetComponentInChildren<Light2D>();
        _eyeLight.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TriggerStare(int triggerID)
    {
        StartCoroutine(Stare(triggerID));
    }

    private IEnumerator Stare(int triggerNumber)
    {
        _renderer.sprite = stares[triggerNumber];
        StartCoroutine(OpenLight(StareDurations.stareSeconds[triggerNumber]));
        yield return new WaitForSeconds(StareDurations.stareSeconds[triggerNumber] - 0.45f);

        _renderer.sprite = halfClosed;
        yield return new WaitForSeconds(0.15f);
        _renderer.sprite = eyesClosed;

    }

    private IEnumerator OpenLight(float duration)
    {
        float targetIntensity = 0.8f;
        float lerpElapsed = 0;
        float lerpDuration = 0.2f;
        while (lerpElapsed < lerpDuration / 2)
        {
            _eyeLight.intensity = (float) targetIntensity * Mathf.Sin((Mathf.PI / lerpDuration) * lerpElapsed);
            lerpElapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(duration - lerpDuration);

        while (lerpElapsed < lerpDuration)
        {
            _eyeLight.intensity = (float) targetIntensity * Mathf.Sin((Mathf.PI / lerpDuration) * lerpElapsed);
            lerpElapsed += Time.deltaTime;
            yield return null;
        }
    }


}
