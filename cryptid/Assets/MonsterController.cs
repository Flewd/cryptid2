using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour {

    IMonsterState CurrentState;

    [System.NonSerialized]
    public NavMeshAgent monsterNavAgent;

    GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        monsterNavAgent = GetComponent<NavMeshAgent>();
        CurrentState = new MonsterRelocationState(this, true);
        CurrentState.Start();
    }
	
	// Update is called once per frame
	void Update () {
        CurrentState.Update();
    }

    public void SwitchState(IMonsterState newState)
    {
        CurrentState.End();
        CurrentState = newState;
        CurrentState.Start();
    }

    public void CheckDistanceToPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 15)
        {
            SwitchState(new MonsterChargeState(this));
        }
    }

    public void DoScare()
    {
        CurrentState.DoScare();
    }

    public void DoLove()
    {
        CurrentState.DoLove();
    }
}
