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

    private Vector3 mouseInWorldCoordinates;

    public int rotationSpeed;
    #endregion

    #region Player Movement
    [Header("Movement Variables")]
    private Vector2 input;

    public float moveSpeed;

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
    }

    private void FixedUpdate()
    {
        UpdateRotation();
        UpdateMovement();
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
        Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, mouseInWorldCoordinates);
        // Rotate turret towards desired rotation at set speed
        player.rotation = Quaternion.RotateTowards(player.rotation, desiredRotation, rotationSpeed * Time.fixedDeltaTime);
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
