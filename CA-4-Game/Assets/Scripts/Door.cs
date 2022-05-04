using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doorObject, valve;
    Animator doorAnimator;
    public PlayerMovement playerMovement;
    public TutorialManagement tutorial;
    bool hasValve, isOpen, valveRotating;
    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = doorObject.GetComponent<Animator>();
        hasValve = false;
      // valveBox = transform.Find("Valve Box").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (valveRotating)
        {
            valve.transform.Rotate(0,1,0);
        }
    }

    public bool isDoorOpen()
    {
        return isOpen;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Equals("Valve"))
        {
            valve = other.gameObject;
            hasValve = true;
            valve.layer = 0;
            playerMovement.setHoldingObject(false);
            valve.transform.position = new Vector3(-156f, 7.593f, 54.849f);
            valve.transform.rotation = Quaternion.identity;
            valve.transform.Rotate(-90, 0, 0);
            valve.transform.SetParent(this.transform);

        }
    }

    public void OpenDoor()
    {
        if (hasValve&&!isOpen)
        {
            valveRotating = true;
            isOpen = true;
            doorAnimator.SetBool("Open", true);
            StartCoroutine(stopRotating());
        }
    }

    IEnumerator stopRotating()
    {
        yield return new WaitForSeconds(5);
        valveRotating=false;
        if (tutorial.isAtDoor)
        {
            tutorial.setCrouchText();
        }
    }
}
