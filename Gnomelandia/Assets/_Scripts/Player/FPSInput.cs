using NUnit.Framework.Internal.Commands;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Scripts/FPS Input")]

public class FPSInput : MonoBehaviour
{

    [Header("Movement Attributes")]

    [Range(1.0f, 20.0f)]
    [SerializeField] float _speed = 5.0f;
    [SerializeField] float _sprintSpeed = 10.0f;
    [SerializeField] float _gravity = -9.8f;
    [SerializeField] float _jumpSpeed = 15f;
    CharacterController _controller;

    float _verticalVelocity;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        float _totalSpeed = _speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _totalSpeed = _sprintSpeed;
        }
       
        // Gather Input Info
        float deltaX = Input.GetAxis("Horizontal") * _totalSpeed;
        float deltaZ = Input.GetAxis("Vertical") * _totalSpeed;

        // Create Movement Vector
        Vector3 movement = new(deltaX, 0.0f, deltaZ);

        movement = Vector3.ClampMagnitude(movement, _totalSpeed);

        

        if (_controller.isGrounded)
        {
            _verticalVelocity = _gravity;

            if (Input.GetButtonDown("Jump"))
            {
                _verticalVelocity += _jumpSpeed;
            }
        }
        else
        {
            _verticalVelocity += _gravity * Time.deltaTime;
        }


        // Apply Nuance to gravity after position change.
        movement.y = _verticalVelocity;




        // Consider Framerate
        movement *= Time.deltaTime;

        //Convert movement vector to the reaction of the Plaer
        movement = transform.TransformDirection(movement);

        // Move
        _controller.Move(movement);

    }
}
