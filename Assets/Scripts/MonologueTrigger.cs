using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonologueTrigger : MonoBehaviour
{
    private PlayerCharacter _player;
    private Canvas _worldSpaceCanvas;
    private TextMeshProUGUI _text;
    public string message;

    private void Start()
    {
        _player = FindObjectOfType<PlayerCharacter>();
        _worldSpaceCanvas = _player.gameObject.GetComponentInChildren<Canvas>();
        _text = _worldSpaceCanvas.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (true) //condition
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(TypeLine(message));
        Debug.Log("I am here");
    }

    private IEnumerator TypeLine(string message)
    {
        _text.text = "";
        char[] lineByChar = message.ToCharArray();
        foreach (char character in lineByChar)
        {
            _text.text += character;
            yield return null; //goes throught the loop once per frame
        }
        yield return new WaitForSeconds(2);
        _text.text = "";
    }


}
