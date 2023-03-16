using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeInteraction : Interactable
{
    private ResourceNode _resourceNode;
    private void Awake()
    {
        _resourceNode = GetComponent<ResourceNode>();
    }

    public override string GetDescription()
    {
        var displayName = _resourceNode.ResourceItemData.DisplayName;
        var toolType = _resourceNode.ToolTypeRequired;
        string output = $"{displayName}";
        
        if (toolType == "none")
        {
            return output;
        }
        output += $" (Use {toolType} tool type)";
        return output;
    }

    public override void Interact()
    {
        
        _resourceNode.Interact();
    }
}
