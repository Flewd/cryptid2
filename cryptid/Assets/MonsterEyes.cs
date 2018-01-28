using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEyes : MonoBehaviour {

    GameObject Player;

    [SerializeField]
    float EyeLightIntensity = 2;

    Light[] eyeLights;

    public enum EyeColors { yellow, red, green }

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        eyeLights = GetComponentsInChildren<Light>();
        StartCoroutine(waitThenTurnOnEyes());
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Player.transform);
	}

    IEnumerator waitThenTurnOnEyes()
    {
        yield return new WaitForSeconds(15);
        StartCoroutine(turnEyesOn());
    }

    IEnumerator turnEyesOn()
    {
        while (eyeLights[0].intensity < EyeLightIntensity)
        {
            yield return new WaitForEndOfFrame();
            for (int i = 0; i < eyeLights.Length; i++)
            {
                eyeLights[i].intensity += 0.01f;
            }
        }
    }

    public void ChangeEyeColor(EyeColors newEyeColor)
    {
        switch (newEyeColor)
        {
            case EyeColors.green: SetEyeColors(Color.green); break;
            case EyeColors.red: SetEyeColors(Color.red); break;
            case EyeColors.yellow: SetEyeColors(Color.yellow); break;
        }
    }

    void SetEyeColors(Color newColor)
    {
        for (int i = 0; i < eyeLights.Length; i++)
        {
            eyeLights[i].color = newColor;
        }
    }
}
