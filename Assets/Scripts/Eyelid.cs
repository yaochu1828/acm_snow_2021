using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyelid : MonoBehaviour
{
    public Animator _eyelidAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("e"))
        {
            GameManager.instance.playerEyesClosed = true;
            _eyelidAnimator.SetBool("Open", false);
            _eyelidAnimator.SetBool("Close", true);
            CloseEyesQuit();
        }
        if (Input.GetKeyUp("e") && !GameManager.instance.isHug)
        {
            GameManager.instance.playerEyesClosed = false;
            _eyelidAnimator.SetBool("Open", true);
            _eyelidAnimator.SetBool("Close", false);
        }
    }

    private void CloseEyesQuit()
    {
        if (GameManager.instance.isHug)
        {
            StartCoroutine(QuitGame());
        }
    }

    private IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance._credits.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        Debug.Log("quitting game");
        Application.Quit();
    }

}
