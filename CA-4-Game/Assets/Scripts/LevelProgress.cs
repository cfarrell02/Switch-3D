using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    bool isAtLift;
    public AudioSource audioSource;
    public AudioClip buttonSound, liftSound;
    public Animator liftDoors;

    public void OnUse()
    {
        if (isAtLift)
        {
            audioSource.PlayOneShot(buttonSound);
            StartCoroutine(NextLevel());
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1);
        liftDoors.SetBool("Open", false);
        yield return new WaitForSeconds(2);
        audioSource.PlayOneShot(liftSound,.2f);
        yield return new WaitForSeconds(4);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("LevelEnd"))
        {
            isAtLift = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("LevelEnd"))
        {
            isAtLift = false;
        }
    }
}
