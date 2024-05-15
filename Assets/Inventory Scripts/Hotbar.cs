using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public Inventory Inventory;
    // Start is called before the first frame update
    void Start()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;
        InitializeHotbarSlots();
    }
    void InitializeHotbarSlots()
    {
        for (int i = 0; i < transform.GetChild(0).GetChild(0).childCount; i++)
        {
            Transform slot = transform.GetChild(0).GetChild(0).GetChild(i);
            UnityEngine.UI.Image image = slot.GetChild(0).GetComponent<UnityEngine.UI.Image>();
            image.enabled = false; // Disable image initially
        }
    }
    private void InventoryScript_ItemAdded(object sender, InventorEventArgs e)
    {
        Debug.Log("Item Added Event Fired!");
        
        foreach(Transform slot in transform.GetChild(0).transform)
        {
            
            
            UnityEngine.UI.Image image = slot.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>();
            
            if(!image.enabled)
            {
                Debug.Log("Found available slot: " + slot.GetSiblingIndex());
                image.enabled = true;
                image.sprite = e.Item.Image;
                Debug.Log("Item Added in Hotbar!");
                return;
            }
            else
            {
                Debug.Log("Occupied slot at " + slot.GetSiblingIndex() + " is occupied by" + image.sprite.name);
                
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
