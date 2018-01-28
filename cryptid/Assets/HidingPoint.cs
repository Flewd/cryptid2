using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPoint : MonoBehaviour {

    public HidingPoint[] forwardPoints;
    public HidingPoint[] backwardsPoints;
    public HidingPoint[] radiusPoints;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

#if UNITY_EDITOR

    void OnDrawGizmos()
    {
        if (forwardPoints != null)
        {
            foreach (HidingPoint block in forwardPoints)
            {
                if (block != null)
                {
                    Gizmos.DrawLine(transform.position, block.transform.position);
                }
            }
        }
        if (backwardsPoints != null)
        {
            foreach (HidingPoint block in backwardsPoints)
            {
                if (block != null)
                {
                    Gizmos.DrawLine(transform.position, block.transform.position);
                }
            }
        }
        if (radiusPoints != null)
        {
            foreach (HidingPoint block in radiusPoints)
            {
                if (block != null)
                {
                    Gizmos.DrawLine(transform.position, block.transform.position);
                }
            }
        }
    }
#endif
    }
