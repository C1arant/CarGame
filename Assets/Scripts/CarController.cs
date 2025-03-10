using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEditor;
using System.Threading;
using System;

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

    private Vector3 MoveForce;
    private float currentSteerAngle = 0f; // To store current steering angle

    void Start()
    {
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
            MoveForce += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            CarRigidbody.AddForce(MoveForce, ForceMode.Acceleration);

            leftBackWheel.Rotate(Vector3.right, MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime * 1000);
            rightBackWheel.Rotate(Vector3.left, MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime * 1000);

            //stearing
            float steerInput = Input.GetAxis("Horizontal");
            if (Input.GetAxis("Vertical") < 0) steerInput = -steerInput;
            Quaternion deltaRotation = Quaternion.Euler(Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime);
            CarRigidbody.MoveRotation(CarRigidbody.rotation * deltaRotation);

            // Update wheel rotation
            if (leftFrontWheel != null && rightFrontWheel != null)
            {
                // Calculate the target steering angle
                currentSteerAngle = steerInput * maxWheelTurnAngle;

                // Reset wheels rotation to match car's rotation first
                leftFrontWheel.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
                rightFrontWheel.localRotation = Quaternion.AngleAxis(0, Vector3.forward);

                // Apply steering rotation to wheels
                leftFrontWheel.Rotate(Vector3.up, currentSteerAngle);
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
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        IsGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);
    }

    private void Reset()
    {

    }
}