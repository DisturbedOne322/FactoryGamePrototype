using System;
using UnityEngine;

public class WarehouseFacility : MonoBehaviour
{
    [SerializeField]
    private WarehouseStorage[] _warehouseStorages;

    private WarehouseStorage _cachedStorage;

    public bool SubstorageHasSpace(ResourceBase resource)
    {
        _cachedStorage = FindAppropriateStorage(resource);
        return _cachedStorage?.HasSpace() ?? false;
    }
    public void AddToSubstorage(ResourceBase resource) => _cachedStorage.Add(resource);

    public bool SubstorageHasResource(ResourceBase resource)
    {
        _cachedStorage = FindAppropriateStorage(resource);
        return _cachedStorage?.HasItems() ?? false;
    }
    public void RemoveFromSubstorage(ResourceBase resource)
    {
        FindAppropriateStorage(resource).Retrieve();
    }

    private WarehouseStorage FindAppropriateStorage(ResourceBase resource)
    {
        int size = _warehouseStorages.Length;
        for (int i = 0; i < size; i++)
        {
            if (_warehouseStorages[i].StoredResourceType == resource)
                return _warehouseStorages[i];
        }
        return null;
    }
}
