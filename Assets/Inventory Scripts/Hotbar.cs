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
    }

    private void InventoryScript_ItemAdded(object sender, InventorEventArgs e)
    {
        //Transform hotbar = transform.Find("Hotbar");
        foreach(Transform slot in transform.GetChild(0).GetChild(0).transform)
        {
            
            
            UnityEngine.UI.Image image = slot.GetChild(0).GetComponent<UnityEngine.UI.Image>();
            
            if(!image.enabled)
            {
                
                image.enabled = true;
                image.sprite = e.Item.Image;
                Debug.Log("Item Added in Hotbar!");
               break;
            }
            
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
