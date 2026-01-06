using UnityEngine;
using CAUnityFramework;
using CAUnityFramework.Editor.Utils;

[DisallowMultipleComponent]
public class TargetingBehaviour2D : MonoBehaviour
{
	[Tooltip("The object it should track")]
	[SerializeField] private Transform trackedObject;

	[Tooltip("Degrees to offset the facing direction (0 = right, 90 = up, 180 = left, 270 = down).")]
	[Range(0, 360)]
	[SerializeField] private float rotationOffset = 0f;

	[Tooltip("Rotate instantly or smoothly.")]
	[SerializeField] private bool instantRotate = true;

	[Tooltip("Rotation speed (used if not instant).")]
	[HideIf(nameof(instantRotate))]
	[SerializeField] private float rotationSpeed = 10f;

	void Start()
	{
		if (this.trackedObject == null)
			this.trackedObject = this.transform.parent;
	}

	void FixedUpdate()
	{
		if (PauseManager.IsPaused())
			return;

		if (this.trackedObject == null)
			return;

		Vector2 direction = (this.trackedObject.position - this.transform.position).normalized;
		if (direction.sqrMagnitude < 0.0001f)
			return;

		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		angle += this.rotationOffset;

		if (this.instantRotate)
		{
			this.transform.rotation = Quaternion.Euler(0, 0, angle);
		}
		else
		{
			float currentZ = this.transform.eulerAngles.z;
			float newZ = Mathf.LerpAngle(currentZ, angle, Time.deltaTime * this.rotationSpeed);
			this.transform.rotation = Quaternion.Euler(0, 0, newZ);
		}
	}

	public void SetTrackedObject(Transform newTrackedObject)
	{
		this.trackedObject = newTrackedObject;
	}
}
