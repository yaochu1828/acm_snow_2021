using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{

    public Sprite eyesClosed;
    public Sprite halfClosed;
    public List<Sprite> stares = new List<Sprite>(); 
    

    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
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
        yield return new WaitForSeconds(StareDurations.stareSeconds[triggerNumber] - 0.45f);

        _renderer.sprite = halfClosed;
        yield return new WaitForSeconds(0.15f);
        _renderer.sprite = eyesClosed;

    }


}
