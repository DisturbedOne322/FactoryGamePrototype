using UnityEngine;

public class WarehouseFacility : MonoBehaviour
{
    [SerializeField]
    private WarehouseStorage[] _warehouseStorages;

    public bool SubstorageHasSpace(ResourceBase resource) => FindAppropriateStorage(resource)?.HasSpace() ?? false;
    public void AddToSubstorage(ResourceBase resource) => FindAppropriateStorage(resource).Add();
    public bool SubstorageHasResource(ResourceBase resource) => FindAppropriateStorage(resource)?.HasItems() ?? false;
    public void RemoveFromSubstorage(ResourceBase resource) => FindAppropriateStorage(resource).Retrieve();   

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
