using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private Button button;
    private Image innerBar;
    private Image healthBar;
    private GameObject player;
    private Transform playerTrans;
    public RectTransform loadingPanel;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        loadingPanel.sizeDelta = new Vector2(Screen.width, Screen.height);
        HideLoadingScreen();
        Debug.Log("Hide at start");
    }
    private void Update()
    {
        if (player != null)
        {
            float playerHealth = 1f;
            float tmp = Mathf.Abs(playerTrans.position.x);
            if (tmp == 1.0f)
                playerHealth = 1.0f;
            else
                playerHealth = 1 - Mathf.Clamp(tmp, -5f, 5f) / 5.0f;

            innerBar.fillAmount = playerHealth;

            if (playerHealth < 0.5f && innerBar.color != Color.red)
                innerBar.color = Color.red;
            else if (playerHealth > 0.5 && innerBar.color != Color.green)
                innerBar.color = Color.green;
        }
    }

    private void LateUpdate()
    {
        if (healthBar != null) 
            healthBar.transform.rotation = Camera.main.transform.rotation;
    }

    public void LoadFirstLevel()
    {
        //ShowLoadingScreen();
        //StartCoroutine(WaitBeforeLoadScene());
        //StartCoroutine(WaitBeforeHideLoadingScreen());
        //Debug.Log("Hide after load scene");

        SceneManager.sceneLoaded += OnSceneLoaded;
        loadingPanel.DOAnchorPosY(0, 1f).OnComplete(() => {
            SceneManager.LoadScene("WalkingScene");
            HideLoadingScreen();
        });
        //loadingPanel.DOAnchorPosY
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        UnityEditor.EditorApplication.isPlaying = false;
    }
    public void OnSceneLoaded (Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == SceneManager.GetSceneByName("WalkingScene").buildIndex)
        {
            GameObject go = GameObject.FindGameObjectWithTag("QuitButton");
            button = go.GetComponent<Button>();
            healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
            innerBar = GameObject.Find("InnerBar").GetComponent<Image>();
            player = GameObject.FindGameObjectWithTag("Player");
            playerTrans = player.transform;
            button.onClick.AddListener(QuitGame);
        }
    }

    private void ShowLoadingScreen()
    {
        //loadingPanel.anchoredPosition = Vector2.zero;
        loadingPanel.DOAnchorPosY(0, 0.2f);
        Debug.Log("Show");
    }
    
    private void HideLoadingScreen()
    {
       // loadingPanel.anchoredPosition = new Vector2(0, -Screen.height);
       loadingPanel.DOAnchorPosY(-(float)Screen.height, 0.2f);
        Debug.Log("Hide");
    }

    private IEnumerator WaitBeforeLoadScene()
    {
        ShowLoadingScreen();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("WalkingScene");
    }
    private IEnumerator WaitBeforeHideLoadingScreen()
    {
        yield return new WaitForSeconds(1.0f);
        HideLoadingScreen();
        Debug.Log("Hide after load screen");
    }
    
    //private Camera mainCamera;
    //[SerializeField] private bool useStaticBillboard;

    //private void Start()
    //{
    //    mainCamera = Camera.main;
    //}

    //public void ManagedLateUpdate()
    //{
    //    if (!useStaticBillboard)
    //    {
    //        transform.LookAt(mainCamera.transform);
    //    }
    //    else
    //    {
    //        transform.rotation = Camera.main.transform.rotation;
    //    }

    //    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    //}
}
