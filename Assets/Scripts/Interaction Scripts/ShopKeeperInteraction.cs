using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeperInteraction : Interactable
{
    public override string GetDescription()
    {
        string output = "Press [E] to open shop."; //interaction hint
        return output;
    }

    public override void Interact()
    {
        Debug.Log("Interacted with the shop keeper");
    }
}
