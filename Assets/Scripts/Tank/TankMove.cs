using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class handles the tank's rotation and movement.
/// </summary>
public class TankMove : MonoBehaviour
{
    // [SerializeField] allows this private variable to be visible in the Inspector and supports hot-reloading during runtime,
    // making it convenient to tune game behavior without recompiling the code.
    [SerializeField] public float moveSpeed = 1.0f; // move speed
    [SerializeField] public float rotSpeed = 1.0f; // rotation speed

    [HideInInspector] public float moveVal; // Movement value received from hardware input
    [HideInInspector] public float rotVal; // Rotation value received from hardware input
    Rigidbody rb; // rigidbody of this gameobject

    //private InputAction tankMove;
    //private InputAction tankRotate;

    private void Start()
    {
        moveVal = 0f;
        rotVal = 0f;
        rb = GetComponent<Rigidbody>();

        //tankMove = InputSystem.actions.FindAction("TankMove");
        //tankRotate = InputSystem.actions.FindAction("TankRotate");
    }

    public void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * moveSpeed * moveVal;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0,  rotSpeed * rotVal, 0));
    }

}