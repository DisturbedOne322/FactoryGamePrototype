using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WarehouseStorage
{
    [SerializeField, Min(1)]
    private int _storeCapacity = 10;
    public int _amountStored = 10;
    [SerializeField]
    private ResourceBase _resourceToStore;
    public ResourceBase StoredResourceType => _resourceToStore;

    public bool IsEmpty() => _amountStored == 0;
    public bool HasSpace() => _amountStored < _storeCapacity;
    public bool HasItems() => _amountStored > 0;

    public void Add(ResourceBase resource) => _amountStored++;

    public void Retrieve() => _amountStored--;
}
