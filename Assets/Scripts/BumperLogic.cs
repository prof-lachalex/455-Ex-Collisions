using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperLogic : MonoBehaviour
{
    [SerializeField]
    private float _minBumpingForce = 10f;
    [SerializeField]
    private float _maxBumpingForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null || collision.gameObject == null || collision.contactCount <= 0) 
            return;

        GameObject obj = collision.gameObject;
        if (obj.CompareTag("GameBall"))
        {
            // Rigidbody de la balle
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if(rb != null) 
            {
                float bumpingForce = Random.Range(_minBumpingForce, _maxBumpingForce);
                rb.AddForce(-collision.GetContact(0).normal * bumpingForce, ForceMode.Impulse);
            }
        }
    }
}
