using UnityEngine;

public class PickupTruckController : MonoBehaviour
{
    [SerializeField] WheelCollider FrontLeftWheelCollider;
    [SerializeField] WheelCollider FrontRightWheelCollider;
    [SerializeField] WheelCollider RearLeftWheelCollider;
    [SerializeField] WheelCollider RearRightWheelCollider;

    [SerializeField] Transform FrontLeftTransform;
    [SerializeField] Transform FrontRightTransform;
    [SerializeField] Transform RearLeftTransform;
    [SerializeField] Transform RearRightTransform;

    [SerializeField] float acceleration = 500f;
    [SerializeField] float breakingForce = 300f;
    [SerializeField] float maxTurnAngle = 15f;

    float currentAcceleration = 0f;
    float currentBreakingForce = 0f;
    float currentTurnAngle = 0f;

    private void FixedUpdate()
    {
        HandleCarAcceleration();
        HandleCarRotaion();
        HandleCarBreak();
        HandleWheelsVisuals();
    }

    private void HandleCarAcceleration()
    {
        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        FrontLeftWheelCollider.motorTorque = -currentAcceleration;
        FrontRightWheelCollider.motorTorque = -currentAcceleration;
    }

    private void HandleCarRotaion()
    {
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");

        FrontLeftWheelCollider.steerAngle = currentTurnAngle;
        FrontRightWheelCollider.steerAngle = currentTurnAngle;
    }

    private void HandleCarBreak()
    {
        if (Input.GetKey(KeyCode.Space))
            currentBreakingForce = breakingForce;
        else
            currentBreakingForce = 0f;

        FrontLeftWheelCollider.brakeTorque = currentBreakingForce;
        FrontRightWheelCollider.brakeTorque = currentBreakingForce;
        RearLeftWheelCollider.brakeTorque = currentBreakingForce;
        RearRightWheelCollider.brakeTorque = currentBreakingForce;
    }

    private void HandleWheelsVisuals()
    {
        UpdateWheelVisual(FrontLeftWheelCollider, FrontLeftTransform);
        UpdateWheelVisual(FrontRightWheelCollider, FrontRightTransform);
        UpdateWheelVisual(RearLeftWheelCollider, RearLeftTransform);
        UpdateWheelVisual(RearRightWheelCollider, RearRightTransform);
    }

    private void UpdateWheelVisual(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);

        transform.position = position;
        transform.rotation = rotation;
    }
}
