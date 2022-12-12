using UnityEngine;
using TMPro;

public class Pickup : MonoBehaviour
{
    public float distance;
    public float smooth;
    
    public TextMeshProUGUI pickupText;

    public GameObject mainCamera;
    private Transform carriedObject;
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
                    pickupText.text = "[E] Pick Up " + hit.transform.name;
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

    public void Throw()
    {
        if (carrying)
        {
            
        }
    }

    private void Carry(Transform obj)
    {
        obj.position = Vector3.Lerp(obj.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
        //obj.GetComponent<Rigidbody>().MovePosition(mainCamera.transform.position + mainCamera.transform.forward * distance);
        //Debug.Log(Vector3.Distance(mainCamera.transform.position, obj.transform.position));
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
                carriedObject = hit.transform;
                carriedObject.GetComponent<Rigidbody>().useGravity = false;
                //carriedObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                pickupText.text = "[E] Drop " + carriedObject.name;
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