using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityAction OnInteract;

	void Start()
    {
        
    }

    public void Interact()
	{
        this.OnInteract.Invoke();
	}
}
