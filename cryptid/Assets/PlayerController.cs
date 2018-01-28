using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    float playerSpeed = 2;

    [SerializeField]
    float rotationSpeed = 2;

    [SerializeField]
    GameObject forwardPoint;

    Rigidbody rigidbody;

    [SerializeField]
    GameObject monster;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Constants.isEndGame == false)
        {
            Vector3 dir = (this.transform.position - forwardPoint.transform.position).normalized;

            if (Input.GetKey(KeyCode.W))
            {
                rigidbody.velocity = dir * -playerSpeed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rigidbody.velocity = dir * playerSpeed;
            }

            if (Input.GetKey(KeyCode.A))
            {
                gameObject.transform.eulerAngles = gameObject.transform.eulerAngles + new Vector3(0, -rotationSpeed, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                gameObject.transform.eulerAngles = gameObject.transform.eulerAngles + new Vector3(0, rotationSpeed, 0);
            }
        }
        else
        {
            gameObject.transform.LookAt(monster.transform);
        }
    }
}
