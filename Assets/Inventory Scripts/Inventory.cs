using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   
    private const int SLOTS = 7;
    private List<IInventoryItem> items = new List<IInventoryItem>();
    public EventHandler<InventorEventArgs> ItemAdded;
    
    public Transform hotbarCanvas;

    public GameObject weaponHolder; //Reference WeaponHolder
    public Sprite equipped; //equipped item border
    public Sprite default_border; //default border
    private int selected_slot = -1;
    GameObject selected_gun = null;
    public void EquipItem(int slot)
    {
        if (slot >= 0 && slot < SLOTS && slot < items.Count) // Check for valid slot index and existing item
        {
            IInventoryItem item = items[slot];

            //Equip Gun
            if(item is Ak47Inventory || item is GlockInventory)
            {
            
                GunData gunData = item.GunData;
            
                if(gunData != null)
                {
                    
                    if (gunData.GunPrefab == selected_gun)
                    {
                        Debug.LogWarning(item.Name + " is already equipped");

                    }
                    else
                    {
                        
                        Destroy(selected_gun);
                        //Disable EquippedBorder at selectedSlot
                        if (selected_slot != -1)
                        {
                            disableEquipBorder(selected_slot);
                        }

                        float x = weaponHolder.transform.position.x;
                        float y = weaponHolder.transform.position.y;
                        float z = weaponHolder.transform.position.z;
                        GameObject gunInstance = Instantiate(gunData.GunPrefab, new Vector3(x + 0.27f,y- 0.18f,z+0.30f) ,weaponHolder.transform.rotation);

                        gunInstance.transform.parent = weaponHolder.transform;
                    
        

                        selected_slot = slot;
                        selected_gun = gunInstance;

                        //Enable blue border
                        EquipBorder(slot);
                        Debug.Log(item.Name + " has been equipped");
                    }
                    
                }

            }

        }
        else
        {
            Debug.Log("Invalid slot or no item in slot");
        }
    }
    private void disableEquipBorder(int slot)
    {
    
        Transform border = hotbarCanvas.GetChild(0).GetChild(slot).GetChild(0).transform;
        UnityEngine.UI.Image image = border.GetComponent<UnityEngine.UI.Image>();
        image.sprite = default_border;
    }
    private void EquipBorder(int slot)
    {
        Transform border = hotbarCanvas.GetChild(0).GetChild(slot).GetChild(0).transform;
        UnityEngine.UI.Image image = border.GetComponent<UnityEngine.UI.Image>();
        image.sprite = equipped;
    }
    public void AddItem(IInventoryItem item)
    {
        Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
        if(items.Count < SLOTS)
        {
            ItemAdded(this, new InventorEventArgs(item));
            if (collider.enabled)
            {
                collider.enabled = false;
                items.Add(item);
                Debug.Log(item.Name + " has been added to inventory");
                
                item.OnPickup();
                
                //ItemAdded(this, new InventorEventArgs(item));
                
            }
        }
        else
        {
            Debug.Log("inventory is full cannot add: " + item.Name);
        }
        
    }

 
}
