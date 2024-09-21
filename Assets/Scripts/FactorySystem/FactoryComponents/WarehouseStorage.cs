using System;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseStorage : MonoBehaviour
{
    [SerializeField]
    private WarehouseStorageType _storageType;
    public WarehouseStorageType StorageType => _storageType;

    [SerializeField, Min(1)]
    private int _storeCapacity = 10;
    public int StoreCapacity => _storeCapacity;
    private int _amountStored = 0;
    public int AmountStored => _amountStored;

    [SerializeField]
    private ResourceBase _resourceToStore;
    public ResourceBase StoredResourceType => _resourceToStore;

    [SerializeField]
    private WarehouseStoragePlacementArea _resourcesDisplay;

    private Stack<GameObject> _storedGOsStack = new();

    public bool IsEmpty() => _amountStored == 0;
    public bool HasSpace() => _amountStored < _storeCapacity;
    public bool HasItems(int amount) => _amountStored >= amount;

    private void Awake()
    {
        GetComponent<MeshRenderer>().material.color = _resourceToStore.ResourceColor;
    }

    public void Add()
    {
        GameObject go = ResourceFactorySingleton.Instance.GetResourceGO(_resourceToStore);
        _storedGOsStack.Push(go);
        _resourcesDisplay.PlaceResourceBox(_amountStored, go);
        _amountStored++;
    }

    public void Retrieve(int amount)
    {
        if (_storedGOsStack.Count - amount < 0)
            return;

        _amountStored -= amount;
        RetrieveResourceBox(_resourceToStore, amount);
    }

    private void RetrieveResourceBox(ResourceBase resource, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject result = _storedGOsStack.Pop();
            ResourceFactorySingleton.Instance.ReleaseResourceGO(resource, result);
        }
    }
}

public enum WarehouseStorageType
{
    ResourceStore,
    ProductionStore,
}

[Serializable]
public class WarehouseStoragePlacementArea
{
    [Min(1)]
    public int Rows = 5;
    [Min(1)]
    public int Columns = 5;
    [Min(1)]    
    public float CellSize = 1;

    public Transform CenterPosition;

    public GameObject PlaceResourceBox(int id, GameObject go)
    {
        go.transform.position = CenterPosition.position + CalculateOffset(id);
        go.transform.SetParent(CenterPosition);
        return go;  
    }

    private Vector3 CalculateOffset(int id)
    {
        int boxesPerLevel = Rows * Columns;
        int level = id / boxesPerLevel;

        int indexWithinLevel = id % boxesPerLevel;
        int row = indexWithinLevel / Rows;
        int col = indexWithinLevel % Columns;

        return new Vector3(
            (col * CellSize) - Rows / 2f + CellSize / 2,
            level * CellSize,
            (row * CellSize) - Columns / 2f + CellSize / 2
        );

    }
}
