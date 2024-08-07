using UnityEngine;

public class AntiRollBar : MonoBehaviour {

	[SerializeField] WheelCollider WheelLeft;
    [SerializeField] WheelCollider WheelRight;
    [SerializeField] float AntiRoll = 5000.0f;

	Rigidbody carRigidbody;

    void Awake() => carRigidbody = GetComponent<Rigidbody>();

    void FixedUpdate ()
	{
		WheelHit hit;
		float travelLeft = 1.0f;
		float travelRight = 1.0f;

		bool groundedLeft = WheelLeft.GetGroundHit(out hit);
		
		if (groundedLeft)
            travelLeft = (-WheelLeft.transform.InverseTransformPoint(hit.point).y - WheelLeft.radius) / WheelLeft.suspensionDistance;

		bool groundedRight = WheelRight.GetGroundHit(out hit);
		
		if (groundedRight)
            travelRight = (-WheelRight.transform.InverseTransformPoint(hit.point).y - WheelRight.radius) / WheelRight.suspensionDistance;

        float antiRollForce = (travelLeft - travelRight) * AntiRoll;

		if (groundedLeft)
			carRigidbody.AddForceAtPosition (WheelLeft.transform.up * -antiRollForce, WheelLeft.transform.position);

		if (groundedRight)
			carRigidbody.AddForceAtPosition (WheelRight.transform.up * antiRollForce, WheelRight.transform.position);
	}
}
