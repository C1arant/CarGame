using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEditor;
using System.Threading;
using System;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public float MoveSpeed = 50;
    public float MaxSped = 15;
    public bool CanMove = false;
    public float Drag = 0.98f;
    public float SteerAngle = 20;
    public float Traction = 0.5f;
    //Ground check stuff
    public bool IsGrounded = true;
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.28f;
    public LayerMask GroundLayers;
    //Bool to reset physics after player dies
    public bool isDead = false;
    Rigidbody CarRigidbody;

    // Added wheel variables
    public Transform leftFrontWheel;  // Reference to left front wheel transform
    public Transform rightFrontWheel; // Reference to right front wheel transform
    public Transform leftBackWheel;  // Reference to left back wheel transform
    public Transform rightBackWheel; // Reference to right back wheel transform
    public float maxWheelTurnAngle = 30f; // Maximum angle the wheels can turn
    private float WheelSpin = 0f; //Spin value for the x rotation on the front tires

    private Vector3 MoveForce;
    private float currentSteerAngle = 0f; // To store current steering angle
    //New Input System
    private PlayerInput playerInput;
    private CarControllerInputs input;
    private bool IsCurrentDeviceMouse
    {
        get
        {
            return playerInput.currentControlScheme == "KeyboardMouse";
        }
    }
    void Start()
    {
        input = GetComponent<CarControllerInputs>();
        playerInput = GetComponent<PlayerInput>();
        CarRigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        GroundedCheck();
        if (isDead)
        {
            Reset();
        }

        if (!CanMove)
        {
            return;
        }

        if (IsGrounded)
        {
            //move
            MoveForce += transform.forward * MoveSpeed * input.move.y * Time.deltaTime;
            CarRigidbody.AddForce(MoveForce, ForceMode.Acceleration);
            var locVel = transform.InverseTransformDirection(CarRigidbody.linearVelocity);
            var Speed = locVel.z * 4;
            leftBackWheel.Rotate(Vector3.left, -Speed);
            rightBackWheel.Rotate(Vector3.right, -Speed);

            //stearing
            float steerInput = input.move.x;
            if (input.move.y < 0) steerInput = -steerInput;
            Quaternion deltaRotation = Quaternion.Euler((Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime) / 2);
            if (locVel.z > 0.5f || locVel.z < -0.5f) CarRigidbody.MoveRotation(CarRigidbody.rotation * deltaRotation);

            // Calculate the target steering angle
            currentSteerAngle = steerInput * maxWheelTurnAngle;

            // Set rotation of the front wheels by speed and steer angle
            if (Speed > 0.01f || Speed < 0.01f)
            {
                WheelSpin += Speed;
                leftFrontWheel.localRotation = Quaternion.Euler(WheelSpin, currentSteerAngle - 180.0f, 0.0f);
                rightFrontWheel.localRotation = Quaternion.Euler(-WheelSpin, currentSteerAngle, 0.0f);
            }
            else
            {
                leftFrontWheel.localRotation = Quaternion.AngleAxis(-180, Vector3.forward);
                rightFrontWheel.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
                leftFrontWheel.Rotate(Vector3.up, -currentSteerAngle);
                rightFrontWheel.Rotate(Vector3.up, currentSteerAngle);
            }


            //Drag
            MoveForce *= Drag;
            MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSped);

            //Traction
            Debug.DrawRay(transform.position, MoveForce.normalized * 3);
            Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
            MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime) * MoveForce.magnitude;
        }
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y,
            transform.position.z - GroundedOffset);
        IsGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);
    }

    private void Reset()
    {

    }
}