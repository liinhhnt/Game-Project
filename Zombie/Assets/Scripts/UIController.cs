using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject Menu;
    public GameObject Mode;
    public AnimationComplete modeAnimComplete;
    public GameObject Stage;
    public Animator anim;

    void Start()
    {
        //modeAnimComplete.onComplete += PlayStage;
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayMode()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("SampleScene");
        Debug.Log("Mode On");
        Menu.SetActive(false);
        Mode.SetActive(true);
    }

    public void PlayStage()
    {
        anim.SetTrigger("IsClosed");
        StartCoroutine(CloseStage());
    }

    private IEnumerator CloseStage()
    {
        yield return new WaitForSeconds(2f);
        Mode.SetActive(false);
        Stage.SetActive(true);
    }    
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Help()
    {
        SceneManager.LoadScene("HelpScene");
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0; //game is Pause
        // 1 = time passes as fast as realtime;
        // 0.5 = time passes 2x slower than realtime
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }    
    
    public void Home ()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }
}
