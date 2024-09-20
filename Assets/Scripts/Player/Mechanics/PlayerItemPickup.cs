using UnityEngine;

[RequireComponent (typeof(PlayerInventory), typeof(PlayerItemInteractionProgres))]
public class PlayerItemPickup : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory _inventory;

    [SerializeField]
    private PlayerItemInteractionProgres _itemInteractionProgress;

    private WarehouseStorage _cachedStorage;

    private float _pickupTimer = 0;
    private void Awake()
    {
        _itemInteractionProgress.ToggleActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        _cachedStorage = null;
        _itemInteractionProgress.ToggleActive(false);
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

        bool interactive = true;

        switch (_cachedStorage.StorageType)
        {
            case WarehouseStorageType.ProductionStore:
                interactive = TryPickUpItem();
                break;
            default:
                interactive = TryPutItem();
                break;
        }

        _itemInteractionProgress.ToggleActive(interactive);

        if (!interactive)
            return;

        _pickupTimer += Time.deltaTime;
        _itemInteractionProgress.UpdateProgress(_pickupTimer / _cachedStorage.StoredResourceType.TimeToPick);
    }

    private bool TryPickUpItem()
    {
        if (_cachedStorage.IsEmpty())
            return false;

        if (_inventory.WillExceedCapacity(_cachedStorage.StoredResourceType))
            return false;

        if (_pickupTimer > _cachedStorage.StoredResourceType.TimeToPick)
        {
            _cachedStorage.Retrieve();
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
