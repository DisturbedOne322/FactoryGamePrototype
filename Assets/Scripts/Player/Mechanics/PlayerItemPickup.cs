using UnityEngine;

[RequireComponent (typeof(PlayerInventory))]
public class PlayerItemPickup : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory _inventory;

    private WarehouseStorage _cachedStorage;

    private float _pickupTimer = 0;
    private float _pickupTimeNormalized = 0;
    public float PickupTimerNormalized => _pickupTimeNormalized;

    private bool _interacting = false;
    public bool Interacting => _interacting;


    private void OnTriggerExit(Collider other)
    {
        _cachedStorage = null;
        _interacting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        _cachedStorage = other.GetComponent<WarehouseStorage>();
        _inventory.TryInitialize(_cachedStorage.StoredResourceType);
    }

    private void Update()
    {
        if (_cachedStorage == null)
            return;

        switch (_cachedStorage.StorageType)
        {
            case WarehouseStorageType.ProductionStore:
                _interacting = TryPickUpItem();
                break;
            default:
                _interacting = TryPutItem();
                break;
        }

        if (!_interacting)
            return;

        _pickupTimer += Time.deltaTime;
        _pickupTimeNormalized = _pickupTimer / _cachedStorage.StoredResourceType.TimeToPick;
    }

    private bool TryPickUpItem()
    {
        if (_cachedStorage.IsEmpty())
            return false;

        if (_inventory.WillExceedCapacity(_cachedStorage.StoredResourceType))
            return false;

        if (_pickupTimer > _cachedStorage.StoredResourceType.TimeToPick)
        {
            _cachedStorage.Retrieve(1);
            _pickupTimer = 0;
            _inventory.AddItem(_cachedStorage.StoredResourceType);
        }
        return true;
    }

    private bool TryPutItem()
    {
        if (!_cachedStorage.HasSpace())
            return false;

        int storesItemsAmount = _inventory.GetAmountStored(_cachedStorage.StoredResourceType);
        if (storesItemsAmount == 0)
            return false;

        if (_pickupTimer > _cachedStorage.StoredResourceType.TimeToPick)
        {
            _cachedStorage.Add();
            _pickupTimer = 0;
            _inventory.RemoveItem(_cachedStorage.StoredResourceType);
        }

        return true;
    }
}
