using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherLogic : MonoBehaviour
{
    GameObject _spring;
    Vector3 _springOriginalPos;

    [SerializeField]
    float _timeToFullCharge = 3f;
    [SerializeField]
    float _forceAtMaxCharge;
    float _springLoadingTime;

    // Start is called before the first frame update
    void Start()
    {
        _spring = GameObject.Find("Spring");
        _springOriginalPos = _spring.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.IsBallInPlay())
        {
            // [Start Pressing] Mouse Left Click
            if (Input.GetButtonDown("Fire1"))
            {
                _springLoadingTime = 0;
            }

            // [Hold] Mouse LeftClick
            if (Input.GetButton("Fire1"))
            {

                if (_springLoadingTime < _timeToFullCharge)
                {
                    _springLoadingTime += Time.deltaTime;
                    Vector3 bounds = _spring.GetComponent<Renderer>().bounds.size;
                    _spring.transform.Translate(0, -(bounds.x / _timeToFullCharge) * Time.deltaTime, 0);
                }
            }
            // [Release] Mouse LeftClick
            if (Input.GetButtonUp("Fire1"))
            {
                Vector3 bounds = _spring.GetComponent<Renderer>().bounds.size;
                _spring.transform.Translate(0, bounds.x * (_springLoadingTime / _timeToFullCharge) + (bounds.x * 0.2f), 0);
            }
        }
    }

    public void ResetLauncher()
    {
        _spring.transform.position = _springOriginalPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null && collision.gameObject.CompareTag("GameBall"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                // propulser la balle au prorata du temps de charge du lanceur
                rb.AddForce(Vector3.left * (_forceAtMaxCharge * (_springLoadingTime / _timeToFullCharge)), ForceMode.Impulse);
            }
        }
    }
}
