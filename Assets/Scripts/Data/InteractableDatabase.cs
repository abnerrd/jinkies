using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDatabase : MonoBehaviour
{

    public Dictionary<string, InteractableData> _assets = new Dictionary<string, InteractableData>();

    public static InteractableDatabase instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        foreach (InteractableData data in Resources.LoadAll("", typeof(InteractableData)))
        {
            if (!_assets.ContainsKey(data.Name))
                _assets.Add(data.Name, data);
        }
    }

    public T GetInteractableObject<T>() where T : InteractableData, new()
    {
        if (_assets.Any(a => a.Value.GetType().IsAssignableFrom(typeof(T))))
        {
            var assetsOfType = _assets.Where(a => a.Value.GetType().IsAssignableFrom(typeof(T))).Select(a => a.Value as T).ToList();
            return assetsOfType[Random.Range(0, assetsOfType.Count)];
        }
        return null;
     }
}