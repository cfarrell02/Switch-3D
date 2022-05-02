using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    Vector2 moveDirection;
    Vector3 velocity;
    public float speed = 15f, gravity = -9.8f, groundDistance = 2f, jumpHeight = 3f, sprintModifier = 1.5f, maxHealth;
    public Transform groundCheck;
    public LayerMask groundMask,physicsMask;
    private bool  isSprinting, isSwitched, isAtButton, physicsObject, holdingObject, isInHazard, isAtValve,isCrouching;
    public Animator animator, uiAnimator;
    int isGrounded;
    Animator buttonAnimator;
    AudioSource audioSource;
    public AudioClip[] footsteps;
    public bool isPaused = false;
    public GameObject camera;
    GameObject physicsItem;
    CharacterController characterController;
    private float health;
    [SerializeField]
    private TextMeshProUGUI healthText;
    [SerializeField] private Door door;

    void Start()
    {
        isCrouching = false;
        isInHazard = false;
        health = maxHealth;
        holdingObject = false;
        characterController = GetComponent<CharacterController>();
        isSwitched = true;
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if(health >=0)
            healthText.text = ((int) health)+"";
        if (!isPaused)
        {
            Move();
            Sound();
        }
        if (isInHazard)
        {
            health-=50*Time.deltaTime;
        }
        if(health <= 0)
        {
            Die();
        }
        
    }

    public void OnCrouch(InputValue input)
    {
        if (isCrouching)
        {
            isCrouching = false;
            characterController.height = 1.94f;
        }
        else
        {
            isCrouching=true;
            characterController.height = 1;
        }
    }
    public void setHoldingObject(bool val)
    {
        holdingObject = val;
    }
    public void InteractEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Valve Box"))
        {
            isAtValve = true;
        }
        
        else if (other.gameObject.layer == 7)
        {
            buttonAnimator = other.gameObject.GetComponent<Animator>();
            isAtButton = true;
            
        }
        else if (other.gameObject.layer == 8 && !holdingObject)
        {
            physicsObject = true;
            physicsItem = other.gameObject;
        }
    }

    public void InteractExit(Collider other)
    {
        if (other.gameObject.name.Equals("Valve Box"))
        {
            isAtValve = false;
        }
        else
        if (other.gameObject.layer == 7)
            isAtButton = false;
        else if (other.gameObject.layer == 8 && !holdingObject)
        {
            physicsObject = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            isInHazard = true;
        }
    }
     void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 4)
            isInHazard = false;
    }
    void OnPause(InputValue input)
    {
        if (!isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            uiAnimator.SetBool("isPaused", true);
            isPaused = true;
        }
        else { isPaused = false;
            uiAnimator.SetBool("isPaused", false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    void Die()
    {
     //   yield return new WaitForSeconds(1);
        isPaused = true;
        uiAnimator.SetBool("isDead", true);
        StartCoroutine(returnToMenu());
    }

    IEnumerator returnToMenu()
    {
        yield return new WaitForSeconds(5);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Menu");

    }


    void Sound()
    {
        int index = Random.Range(0, footsteps.Length - 1);
        if (moveDirection.magnitude > 0 && isGrounded!=0 && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(footsteps[index]);
        }

    }


    void OnUse(InputValue input)
    {
       // print("Is Near Physics Object " + physicsObject + "\n Is Holding it? " + holdingObject);
        if (isAtButton && !uiAnimator.GetBool("teleport"))
            StartCoroutine(teleport());
        if (physicsObject && !holdingObject && !isAtValve)
        {


            holdingObject = true;
            physicsItem.transform.SetParent(camera.transform);
            physicsItem.GetComponent<Rigidbody>().useGravity = false;
            physicsItem.GetComponent<Rigidbody>().freezeRotation = true;
            // physicsItem.GetComponent<Rigidbody>().isKinematic = true;
            physicsItem.GetComponent<Rigidbody>().drag = 100f;
         //   physicsItem.transform.position = this.transform.position + camera.transform.forward * 3;
        }
        else if (holdingObject)
        {

            holdingObject = false;
            physicsItem.GetComponent<Rigidbody>().useGravity = true;
            physicsItem.GetComponent<Rigidbody>().drag = 0f;
             physicsItem.GetComponent<Rigidbody>().freezeRotation = false;
            // physicsItem.GetComponent<Rigidbody>().isKinematic = false;
            physicsItem.transform.SetParent(null);
        
        } if (isAtValve)
        {
            holdingObject = false;
            physicsObject = false;
            door.OpenDoor();
        }
    }

    IEnumerator teleport()
        {
        //buttonAnimator.SetBool("IsPressed", true);
        isPaused = true;
        uiAnimator.SetBool("teleport", true);
        
        yield return new WaitForSeconds(1.1f);
        characterController.enabled = false;
        if (!isSwitched)
        {
            isSwitched = true;
            transform.position = new Vector3(transform.position.x - 200, transform.position.y, transform.position.z);
        }
        else
        {
            isSwitched = false;
            transform.position = new Vector3(transform.position.x + 200, transform.position.y, transform.position.z);
        }
        characterController.enabled = true;
        isPaused = false;
        yield return new WaitForSeconds(5);
        uiAnimator.SetBool("teleport", false);
       // buttonAnimator.SetBool("IsPressed", false);

    }

    void OnMove(InputValue input)
    {
        moveDirection = input.Get<Vector2>();
       
    }

    void OnSprint(InputValue input)
    {
        if (input.isPressed)
        {
            isSprinting = true;
        }else
            isSprinting=false;
    }

    void OnJump(InputValue input)
    {
            
            if (isGrounded==1 ||( isGrounded == 2 && !holdingObject)) { 
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
           
    }
        
        
    }

    private void Move()
    {
        if (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask)) isGrounded = 1;
        else if (Physics.CheckSphere(groundCheck.position, groundDistance, physicsMask)) isGrounded = 2;
        else isGrounded = 0;
        if(isGrounded !=0 && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        Vector3 move = transform.right * moveDirection.x + transform.forward * moveDirection.y;

        if(isSprinting)
            controller.Move(move * (sprintModifier*speed) / 100);
        else
        controller.Move(move * speed / 100);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
