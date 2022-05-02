using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    Vector2 lookDirection;
    public float lookSpeed = 100f, xRotation = 0f;
    [SerializeField] private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        lookSpeed /= 1000;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerMovement.isPaused)
        {
            Look();
            playerBody.Rotate(Vector3.up * lookDirection.x * lookSpeed);
        }
        
    }
    public Vector2 getLookDirection()
    {
        return lookDirection;
    }
    void OnLook(InputValue input)
    {
        lookDirection = input.Get<Vector2>();
    }
    private void Look()
    {
        xRotation -= lookDirection.y * lookSpeed;
     //   xRotation *= lookSpeed;
        xRotation = Mathf.Clamp(xRotation, -90f, 60f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        playerMovement.InteractEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        playerMovement.InteractExit(other);
    }
}
