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
	[SerializeField] private TopDown2DCharacterController controller;

	[SerializeField] private Image statusEffectIcon;
    [SerializeField] private SerializableDictionary<MovementStatus, Sprite> movementSpriteMap;

    private MovementStatus currentStatus = MovementStatus.Idle;

	void OnEnable()
	{
		this.controller.OnMovementStatusUpdate.AddListener(this.UpdateStatus);
	}

    void OnDisable()
    {
        this.controller.OnMovementStatusUpdate.RemoveListener(this.UpdateStatus);
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
