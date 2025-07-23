using UnityEngine;
using UnityEngine.Events;

public class Pickupable : MonoBehaviour
{
    [SerializeField] private Item _item;

    public bool CanPickUp()
    { 
        return true;
    }
}
