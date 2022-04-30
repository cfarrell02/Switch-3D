using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doorObject, valve;
    Animator doorAnimator, valveAnimator;
    public PlayerMovement playerMovement;
    bool hasValve;
    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = doorObject.GetComponent<Animator>();
        valveAnimator = valve.GetComponent<Animator>();
        hasValve = false;
      // valveBox = transform.Find("Valve Box").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (hasValve)
        {
            
        }
    }
}
