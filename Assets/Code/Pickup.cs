using UnityEngine;
using TMPro;

public class Pickup : MonoBehaviour
{
    public float distance;
    public float smooth;
    
    public TextMeshProUGUI pickupText;

    public GameObject mainCamera;
    private GameObject carriedObject;
    private bool carrying = false;
    
    private void Update()
    {
        if (carrying)
        {
            Carry(carriedObject);
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward * distance, out hit, distance))
            {
                if (hit.transform.tag == "Pickup")
                {
                    pickupText.text = "[E] Pickup " + hit.transform.name;
                }
            }
            else
            {
                pickupText.text = "";
            }
        
        }
    }

    public void DropPickup()
    {
        if (carrying)
        {
            DropObject();
        }
        else
        {
            PickupObject();
        }
    }

    private void Carry(GameObject obj)
    {
        obj.transform.position = Vector3.Lerp(obj.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
        //obj.GetComponent<Rigidbody>().MovePosition(mainCamera.transform.position + mainCamera.transform.forward * distance);
    }

    private void PickupObject()
    {
        RaycastHit hit;
        // look for pickupable object
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward * distance, out hit, distance))
        {
            if (hit.transform.tag == "Pickup")
            {
                carrying = true;
                carriedObject = hit.transform.gameObject;
                carriedObject.GetComponent<Rigidbody>().useGravity = false;
                //carriedObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                pickupText.text = "[E] Drop " + carriedObject.transform.name;
            }
        }
    }

    private void DropObject()
    {
        if (!carrying)
            return;
        carrying = false;
        carriedObject.GetComponent<Rigidbody>().useGravity = true;
        //carriedObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
        carriedObject = null;
    }
}