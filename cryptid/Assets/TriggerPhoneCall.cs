using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPhoneCall : MonoBehaviour {

    public TheCall theCall;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        print("PHONE CALL");
        if(Constants.phoneCallTriggered == false)
        {
            theCall.Call();
            //play phone
            Constants.phoneCallTriggered = true;
        }
    }
}
