using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusioc : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
