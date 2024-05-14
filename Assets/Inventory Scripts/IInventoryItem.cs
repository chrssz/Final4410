using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IInventoryItem{
    string Name { get; }
    Sprite Image { get; }
    GunData GunData { get; }

    void OnPickup();
}

public class InventorEventArgs : EventArgs
{
    public InventorEventArgs(IInventoryItem item)
    {
        Item = item;
    }
    public IInventoryItem Item;

}
