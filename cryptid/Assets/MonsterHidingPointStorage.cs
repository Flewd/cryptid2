using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHidingPointStorage : MonoBehaviour {

    static HidingPoint[] HidingPoints;

    static GameObject player;

	// Use this for initialization
	void Start () {
        MonsterHidingPointStorage.HidingPoints = GetComponentsInChildren<HidingPoint>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public static HidingPoint GetNearestPoint(Vector3 monsterPos)
    {
        float lowestDist = Vector3.Distance(HidingPoints[0].transform.position, monsterPos);
        int lowestDistIndex = 0;
        for (int i = 1; i < HidingPoints.Length; i++)
        {
            float dist = Vector3.Distance(HidingPoints[i].transform.position, monsterPos);
            if (dist < lowestDist)
            {
                lowestDist = dist;
                lowestDistIndex = i;
            }
        }
        return HidingPoints[lowestDistIndex];
    }
    
    public static HidingPoint GetRandomPoint()
    {
        HidingPoint point = HidingPoints[Random.Range(0, HidingPoints.Length)];
        while (point.name == "Final")
        {
            point = HidingPoints[Random.Range(0, HidingPoints.Length)];
        }
        Debug.Log(point.transform.position);
        return point;
    }

    public static HidingPoint GetPointFurthestFromPlayer()
    {
        float highestDistance = Vector3.Distance(HidingPoints[0].transform.position, player.transform.position);
        int highestDistIndex = 0;
        for (int i = 1; i < HidingPoints.Length; i++)
        {
            if (HidingPoints[i].name != "Final")
            {
                float dist = Vector3.Distance(HidingPoints[i].transform.position, player.transform.position);
                if (dist > highestDistance)
                {
                    highestDistance = dist;
                    highestDistIndex = i;
                }
            }
        }
        return HidingPoints[highestDistIndex];
    }

    public static HidingPoint GetNextPointTowardsCenter(Vector3 monsterPos)
    {
        HidingPoint neareastPoint = GetNearestPoint(monsterPos);

        if(neareastPoint.forwardPoints.Length > 0)
        {
            return neareastPoint.forwardPoints[Random.Range(0, neareastPoint.forwardPoints.Length)];  // make random
        }
        else if( neareastPoint.radiusPoints.Length > 0)
        {
            return neareastPoint.radiusPoints[Random.Range(0, neareastPoint.radiusPoints.Length)]; // make random
        }
        else if(neareastPoint.backwardsPoints.Length > 0)
        {
            return neareastPoint.backwardsPoints[Random.Range(0, neareastPoint.backwardsPoints.Length)];
        }

        // if for some reason your point has no registered connected points. just return a random point so game doesn't break
        print(neareastPoint.name + " has no connected nodes, make sure to hook them up in the scene, Monster went to a random point");
        return HidingPoints[Random.Range(0, HidingPoints.Length)];
    }

    public static HidingPoint GetNextPointAwayFromCenter(Vector3 monsterPos)
    {
        HidingPoint neareastPoint = GetNearestPoint(monsterPos);

        if (neareastPoint.backwardsPoints.Length > 0)
        {
            return neareastPoint.backwardsPoints[Random.Range(0, neareastPoint.backwardsPoints.Length)];
        }
        else if (neareastPoint.radiusPoints.Length > 0)
        {
            return neareastPoint.radiusPoints[Random.Range(0, neareastPoint.radiusPoints.Length)]; // make random
        }
        else if (neareastPoint.forwardPoints.Length > 0)
        {
            return neareastPoint.forwardPoints[Random.Range(0, neareastPoint.forwardPoints.Length)];  // make random
        }

        // if for some reason your point has no registered connected points. just return a random point so game doesn't break
        print(neareastPoint.name + " has no connected nodes, make sure to hook them up in the scene, Monster went to a random point");
        return HidingPoints[Random.Range(0, HidingPoints.Length)];
    }
}
