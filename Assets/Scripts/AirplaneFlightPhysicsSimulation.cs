using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AirplaneFlightPhysicsSimulation : MonoBehaviour
{
    public bool engineOn = false;
    public float thrust;
    public float liftCoefficient;
    public float stallAngle;
    public float stallLiftMultiplier;

    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.centerOfMass = new Vector3(0, -0.6f, -0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null ) return;

        // Thrust
        if (kb.spaceKey.isPressed)
        {
            engineOn = true;

            // Forward thrust
            rb.AddRelativeForce(
                Vector3.forward * thrust,
                ForceMode.Acceleration
                );
        }

        // Speed -- Calculate speed from direction
        float forwardSpeed = Vector3.Dot(
            rb.linearVelocity,
            transform.forward
            );

        // Lift -- Depends on forward speed, faster the plane -> stronger the lift
        if (engineOn && forwardSpeed > 5f)
        {
            // Lift, Velocity square
            float lift = forwardSpeed * forwardSpeed * liftCoefficient;

            // Check angle to simulate stall
            float pitchAngle = Vector3.Angle(
                transform.forward,
                Vector3.ProjectOnPlane(transform.forward, Vector3.up)
                );
            if (pitchAngle > stallAngle)
            {
                lift *= stallLiftMultiplier;
            }
        }
    }
}
