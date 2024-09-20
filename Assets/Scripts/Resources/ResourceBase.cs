using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceBase : ScriptableObject
{
    public List<ResourceBase> RequiredResourceMaterials;

    [SerializeField]
    private Color _resourceColor = Color.yellow;
    public Color ResourceColor => _resourceColor;

    [SerializeField, Min(0.1f)]
    private float _timeToProduce = 1.0f;
    public float TimeToProduce => _timeToProduce;

    [SerializeField, Min(0.01f)]
    private float _timeToPick = 1.0f;
    public float TimeToPick => _timeToPick;

    private void OnValidate()
    {
        for (int i = RequiredResourceMaterials.Count - 1; i >= 0; i--)
        {
            if (RequiredResourceMaterials[i]?.GetType() == GetType())
                RequiredResourceMaterials.RemoveAt(i);
        }
    }
}
