using System;
using System.CodeDom.Compiler;
using System.Collections;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]
public class UniqueID : MonoBehaviour
{
    [ReadOnly, SerializeField] private string _id = Guid.NewGuid().ToString();
    private static SerializableDictionary<string, GameObject> idDatabase = new();

    public string ID => _id;

    public void OnValidate()
    {
        if (idDatabase.ContainsKey(_id)) Generate();
        else idDatabase.Add(_id, this.gameObject);
    }

    private void OnDestroy()
    {
        if (idDatabase.ContainsKey(_id)) idDatabase.Remove(_id);
    }

    private void Generate()
    {
        _id = Guid.NewGuid().ToString();
        idDatabase.Add(_id, this.gameObject);
    }
}
