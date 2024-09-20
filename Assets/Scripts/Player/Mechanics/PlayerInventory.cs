using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerItemInteractionProgres), typeof(PlayerInventoryDisplay))]
public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private PlayerInventoryDisplay _playerInventoryDisplay;

    [SerializeField, Min(1)]
    private int _maxCapacity = 5;
    public int MaxCapacity => _maxCapacity;

    private Dictionary<ResourceBase, int> _storedResourcesAmountDict = new();

    public bool WillExceedCapacity(ResourceBase resource) => GetAmountStored(resource) + 1 > _maxCapacity;
    public int GetAmountStored(ResourceBase resource) => _storedResourcesAmountDict[resource];

    public void TryInitialize(ResourceBase resource)
    {
        if (!_storedResourcesAmountDict.ContainsKey(resource))
            _storedResourcesAmountDict.Add(resource, 0);
    }

    public void AddItem(ResourceBase resource)
    {
        _storedResourcesAmountDict[resource]++;
        _playerInventoryDisplay.AddItem(resource, _storedResourcesAmountDict[resource]);
    }

    public void RemoveItem(ResourceBase resource)
    {
         _storedResourcesAmountDict[resource]--;
        _playerInventoryDisplay.RemoveItem(resource, _storedResourcesAmountDict[resource]);
    }
}
