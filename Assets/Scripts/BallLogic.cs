using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null || other.gameObject == null)
            return;

        if(other.gameObject.name.Equals("Gateway"))
        {
            GameManager.Instance.OnBallExitGateway();
        }

        if (other.gameObject.name.Equals("Wall_DeadZone"))
        { 
            GameManager.Instance.OnBallExitsGameArea();
        }
    }
}
