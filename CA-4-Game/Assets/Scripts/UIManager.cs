using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Animator uiAnimator;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
