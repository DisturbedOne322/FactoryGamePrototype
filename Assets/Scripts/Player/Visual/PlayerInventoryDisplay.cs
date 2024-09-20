using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryDisplay : MonoBehaviour
{
    [SerializeField]
    private Transform _inventoryParent;

    [SerializeField]
    private GameObject _invItemPrefab;

    private Dictionary<ResourceBase, InventoryItemDisplay> _resourceItemDict = new();

    public void AddItem(ResourceBase resource, int count)
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

    public void RemoveItem(ResourceBase resource, int count)
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
