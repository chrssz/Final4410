using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Ak47Inventory : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get { return "ak47"; }
    }

    public GunData GunData
    {
        get { return gunData; }
        set { gunData = value; }
    }

    public Sprite Image => image;

    public Sprite image = null;
    public GunData gunData; // Public reference to GunData ScriptableObject
    
  
  


  
  public void OnPickup()
  {
    gameObject.SetActive(false); // Deactivate after notifying inventory
  }
    
    
}
