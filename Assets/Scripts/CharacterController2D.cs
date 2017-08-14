using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    #region Variables
    #region Player Rotation
    [Header("Rotation Variables")]
    public Camera mainCamera;
    public Transform player;
    public Transform head;

    private Vector3 mouseInWorldCoordinates;

    public int rotationSpeed;
    #endregion

    #region Player Movement
    [Header("Movement Variables")]
    private Vector2 input;

    public float movespeedbase;
    public float moveSpeed;
    public float moveSpeed75;
    public float moveSpeed50;
    public float moveSpeed25;

    private Rigidbody2D rigidBody2D;
    #endregion
    #endregion

    private void Start()
    {
        // Set position to be 1 unit in front of the turret
        mouseInWorldCoordinates = player.position + player.forward;

        // Get component
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateInput();
        StaminaEffect();
    }

    private void FixedUpdate()
    {
        UpdateRotation();
        UpdateMovement();
    }
    public void StaminaEffect()
    {
        //if stamina is over 75% speed is 100%
        if (Player.instance.Stamina > (Player.instance.MaxStamina * 0.75f))
        {
            moveSpeed = movespeedbase;
        }
        //if stamina is over 50% but less than 75% speed is reduced
        if (Player.instance.Stamina > (Player.instance.MaxStamina * 0.5f) && Player.instance.Stamina <= (Player.instance.MaxStamina * 0.75f))
        {
            moveSpeed = moveSpeed75;
        }
        //if stamina is over 25% but less than 50% speed is reduced
        if (Player.instance.Stamina > (Player.instance.MaxStamina * 0.25f) && Player.instance.Stamina <= (Player.instance.MaxStamina * 0.50f))
        {
            moveSpeed = moveSpeed50;
        }
        //if stamina is less than 25% speed is reduced
        if (Player.instance.Stamina <= (Player.instance.MaxStamina * 0.25f))
        {
            moveSpeed = moveSpeed25;
        }
    }

    private void UpdateRotation()
    {
        // Get touch position
        Vector3 mousePosition = Input.mousePosition;
        // Set distance from camera
        mousePosition.z = Mathf.Abs(mainCamera.transform.position.y - player.position.y);
        // Convert touch position to world coordinates
        mouseInWorldCoordinates = Camera.main.ScreenToWorldPoint(mousePosition);
        // Get desired rotation using world position
        Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, mouseInWorldCoordinates - player.transform.position);
        // Rotate turret towards desired rotation at set speed
        head.rotation = Quaternion.RotateTowards(head.rotation, desiredRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    private void UpdateInput()
    {
        // Store horizontal and vertical input
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void UpdateMovement()
    {
        // If magnitude is greater than 1 normalize input
        Vector2 moveDirection = input.magnitude < 1 ? input: input.normalized;

        // Move player based on input and move speed
        rigidBody2D.MovePosition(rigidBody2D.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}