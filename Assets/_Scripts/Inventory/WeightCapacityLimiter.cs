using System.Collections.Generic;
using UnityEngine;
using CAUnityFramework;
using CAUnityFramework.ItemSystem;

public class WeightCapacityLimiter : BasicCapacityLimiter
{
    [SerializeField, Min(1)] private float maxWeight = 100f;

    public override bool CanAdd(List<ItemStack> items, ItemStack itemToAdd)
    {
        float currentWeight = 0f;
        foreach (var item in items)
        {
            currentWeight += 1f/*item.ItemData.Weight*/ * item.Quantity;
        }

        return currentWeight < this.maxWeight;
    }
}
