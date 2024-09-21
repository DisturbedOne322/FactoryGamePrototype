using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public event Action<ResourceBase, int> OnPickedUp;
    public event Action<ResourceBase, int> OnPutDown;

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
        OnPickedUp?.Invoke(resource, _storedResourcesAmountDict[resource]);
    }

    public void RemoveItem(ResourceBase resource)
    {
         _storedResourcesAmountDict[resource]--;
        OnPutDown?.Invoke(resource, _storedResourcesAmountDict[resource]);
    }
}
