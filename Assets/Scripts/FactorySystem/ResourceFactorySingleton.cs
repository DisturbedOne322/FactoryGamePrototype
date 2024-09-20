using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ResourceFactorySingleton
{
    private static ResourceFactorySingleton _instance;
    public static ResourceFactorySingleton Instance
    {
        get
        {
            if(_instance == null)
                _instance = new ResourceFactorySingleton();

            return _instance;
        }
        private set => _instance = value;
    }

    private Dictionary<ResourceBase, ObjectPool<GameObject>> _pooledResourceGOs = new();

    private const int DEFAULT_SIZE = 10;
    private const int MAX_SIZE = 1000;
    private bool _collectionChecks = true;

    public GameObject GetResourceGO(ResourceBase resource)
    {
        if(!_pooledResourceGOs.ContainsKey(resource))
        {
            _pooledResourceGOs.Add(resource, new ObjectPool<GameObject>(() => CreateResourceGO(resource), OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, _collectionChecks, DEFAULT_SIZE, MAX_SIZE));
        }

        return _pooledResourceGOs[resource].Get();
    }

    public void ReleaseResourceGO(ResourceBase resource, GameObject resourceObj)
    {
        _pooledResourceGOs[resource].Release(resourceObj);  
    }

    private void OnReturnedToPool(GameObject resourceGO) => resourceGO.gameObject.SetActive(false);
    private void OnTakeFromPool(GameObject resourceGO) => resourceGO.gameObject.SetActive(true);
    private void OnDestroyPoolObject(GameObject resourceGO) => GameObject.Destroy(resourceGO);
    private GameObject CreateResourceGO(ResourceBase resource)
    {
        GameObject result = GameObject.CreatePrimitive(PrimitiveType.Cube);
        result.GetComponent<MeshRenderer>().material.color = resource.ResourceColor;
        return result;
    }
}
