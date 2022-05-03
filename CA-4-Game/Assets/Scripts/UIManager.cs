using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Animator uiAnimator;
    public float lookSensitivity = 100f;


    void Awake()
    {
        int numGameSessions = FindObjectsOfType<UIManager>().Length;
        if (numGameSessions > 1 )
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

   

    public void setSensitivity(float sensitivity)
    {
        lookSensitivity = sensitivity;
    }

    public void toggleFullScreen(bool val)
    {
        Screen.fullScreen = val;
    }

    public void StartGame()
    {
        //print("Start!");
        SceneManager.LoadScene("Office Block");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        uiAnimator.SetBool("Settings", true);
    }
    public void CloseSettings()
    {
        uiAnimator.SetBool("Settings", false);
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(uiAnimator == null)
        {
            uiAnimator = FindObjectOfType<Canvas>().GetComponent<Animator>();
        }
    }
}
