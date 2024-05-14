using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    AudioSource audiosource;
    [SerializeField] CharacterController characterController;

    [SerializeField] float speed = 6f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 1f;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] AudioClip jumping;
    
    Vector3 velocity;
    bool isGrounded;
    
    
    public Inventory inventory;
    //Function to see if player collided with a pickable item
    private void OnTriggerEnter(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            inventory.AddItem(item);
        }
    }
    
    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //creates or casts a sphere to check if the Player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0 )
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z  ;
        characterController.Move(move * speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(jumping);
            }
            else
            {
                audiosource.Stop();
            }
        }
        

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
        //For inventory equip
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.EquipItem(0); // Slot 0 for key 1
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventory.EquipItem(1); // Slot 1 for key 2
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventory.EquipItem(2); // Slot 2 for key 3
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            inventory.EquipItem(3); // Slot 3 for key 4
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            inventory.EquipItem(4); // Slot 4 for key 5
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            inventory.EquipItem(1); // Slot 5 for key 6 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            inventory.EquipItem(1); // Slot 6 for key 7
        }
        //
    

    }
}