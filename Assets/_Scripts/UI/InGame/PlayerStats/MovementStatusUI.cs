using CAUnityFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MovementStatus
{
    Idle,
    Walking,
    Running,
	Crouching,
    Dashing
}

public class MovementStatusUI : MonoBehaviour
{
	[SerializeField] private TopDown2DCharacterController characterController;

	[SerializeField] private Image statusEffectIcon;
    [SerializeField] private SerializableDictionary<MovementStatus, Sprite> movementSpriteMap;

    private MovementStatus currentStatus = MovementStatus.Idle;

	void OnEnable()
	{
		if (this.characterController != null)
		{
			this.characterController.OnMovementStatusUpdate.AddListener(this.UpdateStatus);
		}
	}

    void OnDisable()
	{
		if (this.characterController != null)
		{
			this.characterController.OnMovementStatusUpdate.RemoveListener(this.UpdateStatus);
		}
	}

	public void UpdateStatus(MovementStatus newStatus)
    {
        if (newStatus != this.currentStatus)
        {
			this.currentStatus = newStatus;
            if (this.movementSpriteMap.ContainsKey(this.currentStatus))
            {
				this.statusEffectIcon.sprite = this.movementSpriteMap[this.currentStatus];
            }
        }
	}
}
