using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerInventoryDisplay : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory _inventory;

    [SerializeField]
    private Transform _inventoryParent;

    [SerializeField]
    private GameObject _invItemPrefab;

    private Dictionary<ResourceBase, InventoryItemDisplay> _resourceItemDict = new();

    private void Awake()
    {
        _inventory.OnPickedUp += _inventory_OnPickedUp;
        _inventory.OnPutDown += _inventory_OnPutDown;
    }

    private void OnDestroy()
    {
        _inventory.OnPickedUp += _inventory_OnPickedUp;
        _inventory.OnPutDown += _inventory_OnPutDown;
    }

    private void _inventory_OnPickedUp(ResourceBase resource, int count) => AddItem(resource, count);
    private void _inventory_OnPutDown(ResourceBase resource, int count) => RemoveItem(resource, count);


    private void AddItem(ResourceBase resource, int count)
    {
        if(!_resourceItemDict.ContainsKey(resource))
        {
            var item = Instantiate(_invItemPrefab).GetComponent<InventoryItemDisplay>();
            _resourceItemDict.Add(resource, item.SetColor(resource.ResourceColor).SetCount(count));
            item.transform.SetParent(_inventoryParent, false);
        }
        else
        {
            _resourceItemDict[resource].SetCount(count);
        }
    }

    private void RemoveItem(ResourceBase resource, int count)
    {
        if (count == 0)
        {
            Destroy(_resourceItemDict[resource].gameObject);
            _resourceItemDict.Remove(resource);
            return;
        }
        _resourceItemDict[resource].SetCount(count);
    }
}
