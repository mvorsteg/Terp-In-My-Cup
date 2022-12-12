using UnityEngine;

public class Fan : MonoBehaviour
{
    public float rotationsPerSecond = 2f;
    public float windForcePerSecond = 10f;
    [SerializeField]
    private Transform fanModel;
    [SerializeField]
    private Collider windTrigger;

    private void Update()
    {
        fanModel.Rotate(transform.right, rotationsPerSecond * Time.deltaTime * 360f);   
    }    

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            float forceMagnitude = windForcePerSecond * Time.deltaTime;
            Vector3 forceApplied = transform.forward * forceMagnitude;
            rb.AddForce(forceApplied, ForceMode.Force);
        }
    }
}