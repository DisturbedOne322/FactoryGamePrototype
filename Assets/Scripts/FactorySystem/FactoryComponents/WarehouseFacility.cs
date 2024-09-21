using UnityEngine;

public class WarehouseFacility : MonoBehaviour
{
    [SerializeField]
    private WarehouseStorage[] _warehouseStorages;

    public bool SubstorageHasSpace(ResourceBase resource) => FindAppropriateStorage(resource)?.HasSpace() ?? false;
    public void AddToSubstorage(ResourceBase resource) => FindAppropriateStorage(resource).Add();
    public bool SubstorageHasResources(ResourceBase resource, int amount) => FindAppropriateStorage(resource)?.HasItems(amount) ?? false;
    public void RetrieveFromSubstorage(ResourceBase resource, int amount) => FindAppropriateStorage(resource).Retrieve(amount);   

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
