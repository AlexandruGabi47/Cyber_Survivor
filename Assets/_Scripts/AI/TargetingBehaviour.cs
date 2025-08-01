using UnityEngine;
using CAUnityFramework;

public class Targeting2DBehaviour : MonoBehaviour
{
	[Tooltip("The object it should track")]
	[SerializeField] private Transform trackedObject;

	[Tooltip("Whether it should flip the object on the X axis when facing left")]
	[SerializeField] private bool flipXOn180 = false;

	[Tooltip("Whether it should rotate towards the target instantly, otherwise it will lerp")]
	[SerializeField] private bool instantRotate = true;
	[Tooltip("The speed the object should rotate towards the target")]
	[SerializeField] private float rotationSpeed = 10f;

	private Rigidbody2D targetRb;

	void Start()
	{
		if (this.trackedObject == null)
			this.trackedObject = this.transform.parent;
	}

	void LateUpdate()
	{
		if (PauseManager.IsPaused())
			return;

		Vector2 target = this.trackedObject.position;

		if (this.instantRotate)
			this.FaceTowardsTarget(target);
		else
			this.LerpFaceTowardsTarget(target);


		if (this.flipXOn180)
		{
			if (this.transform.eulerAngles.z <= 180)
				this.transform.localScale = new Vector3(1, 1, 1);
			else
				this.transform.localScale = new Vector3(-1, 1, 1);
		}
	}

	private void LerpFaceTowardsTarget(Vector3 targetPos)
	{
		Vector3 direction = targetPos - this.transform.position;

		if (direction.sqrMagnitude > 0.0001f)
		{
			// Target rotation: make .up face the direction
			Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);

			// Lerp rotation
			this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * this.rotationSpeed);

			// Lock X and Y rotation (Z only)
			Vector3 euler = this.transform.rotation.eulerAngles;
			this.transform.rotation = Quaternion.Euler(0f, 0f, euler.z);
		}
	}

	private void FaceTowardsTarget(Vector3 targetPos)
	{
		this.transform.up = this.trackedObject.position - this.transform.position;
		// Sets x and y rotation to 0 as those shouldn't rotate in a 2d space;
		this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, this.transform.rotation.eulerAngles.z));
	}
}
