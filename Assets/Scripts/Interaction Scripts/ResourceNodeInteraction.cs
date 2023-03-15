using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeInteraction : Interactable
{
    public override string GetDescription()
    {
        string output = "Press [E] harvest."; //interaction hint
        return output;
    }

    public override void Interact()
    {
        ResourceNode resourceNode = GetComponent<ResourceNode>();
        resourceNode.Interact();
    }
}
