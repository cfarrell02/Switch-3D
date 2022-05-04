using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManagement : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Door door;
    public bool isAtDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 9) return;
        if (other.name.Equals("ButtonPrompt")) text.text = "Press E to use the button";
        else if (other.name.Equals("JumpPrompt")) text.text = "Press Space to jump";
        else if (other.name.Equals("PickupPrompt")) text.text = "Press E to pick up certain objects";
        else if (other.name.Equals("DoorPrompt"))
        {
            if (door.isDoorOpen())
                setCrouchText();
            isAtDoor = true;
        }
    }
        public void setCrouchText()
        {
            text.text = "Use left control to crouch";
        }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 9) return;
        if(!other.name.Equals("DoorPrompt"))
        other.name = other.name + "_Completed";
        else
        {
            if (door.isDoorOpen())
            {
                other.name = other.name + "_Completed";
            }
        }
            
         text.text = "";
    }
}
