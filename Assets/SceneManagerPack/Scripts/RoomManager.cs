using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{

    public Animator transitionAnimator;
    public int currentRoom { get; private set; } = 0;
    private float holdSeconds = 0;

    public float transitionTime = 1f;
    public Animator _eyelidAnimator;


    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        // hold esc to quit
        if (Input.GetKeyDown("escape"))
        {
            //Debug.Log("esc held");
            holdSeconds += Time.deltaTime;
            if (holdSeconds > 1.25f)
            {
                QuitApplication();
            }
        }
        else
        {
            holdSeconds = 0;
        }

        if (Input.GetKeyDown("q"))
        {
            NextScene();
        }
    }

    public void NextScene()
    {
        StartCoroutine(SceneTransition());
    }


    private IEnumerator SceneTransition()
    {
        //int nextRoom = currentRoom + 1;
        //Debug.Log("Moving to room " + nextRoom);
        yield return new WaitForSeconds(5f);
        //transitionAnimator.SetTrigger("StartNext"); // Room fades out
        yield return new WaitForSeconds(3f);
        QuitApplication();
        //transitionAnimator.SetInteger("Level", nextRoom);

       // SceneManager.LoadScene("Scene_" + nextRoom);
            
        //transitionAnimator.SetTrigger("End"); // New room fades in
       // yield return new WaitForSeconds(transitionTime);
            
        //transitionAnimator.ResetTrigger("End");
       // transitionAnimator.ResetTrigger("StartNext");

        //currentRoom = nextRoom;
        //if (currentRoom == 7)
        //{
        //    yield return new WaitForSeconds(6);
        //    QuitApplication();
        //}

    }

    private void QuitApplication()
    {
        Application.Quit();
    }

}
