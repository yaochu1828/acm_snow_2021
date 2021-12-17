using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EyeTrigger : MonoBehaviour
{

    public GameObject _eyes;
    private EyeEffects _eyeEffects;
    public int triggerID;


    private void Start()
    {
        _eyeEffects = FindObjectOfType<EyeEffects>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _eyes.BroadcastMessage("Stare", triggerID);
        _eyeEffects.TriggerEffects(triggerID);

    }

}
