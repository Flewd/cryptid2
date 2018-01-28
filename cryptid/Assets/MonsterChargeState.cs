using UnityEngine;
using System.Collections;
using System;

public class MonsterChargeState : IMonsterState
{
    MonsterController monsterController;
    GameObject player;

    float chargeSpeed = 1.65f;

    Vector3 directionToPlayer = Vector3.zero;

    float previousDistance = 999999999999;

    MonsterEyes monsterEyes;

    public MonsterChargeState(MonsterController _monsterController)
    {
        monsterController = _monsterController;
        player = GameObject.FindGameObjectWithTag("Player");
        monsterEyes = monsterController.GetComponentInChildren<MonsterEyes>();
    }

    void IMonsterState.Start()
    {
        monsterEyes.ChangeEyeColor(MonsterEyes.EyeColors.red);
        Vector3 heading = player.transform.position - monsterController.transform.position;
        float distance = heading.magnitude;
        directionToPlayer = heading / distance; 
    }

    void IMonsterState.Update()
    {
        monsterController.gameObject.transform.position += directionToPlayer * chargeSpeed;
        float distance = Vector3.Distance(monsterController.transform.position, player.transform.position);
        if (previousDistance < distance)
        {
            monsterController.SwitchState(new MonsterIdleState(monsterController));
        }

        previousDistance = distance;
    }

    void IMonsterState.End()
    {
        // Teleport to new pos
        Vector3 pos = MonsterHidingPointStorage.GetRandomPoint().transform.position;
        monsterController.transform.position = pos;
    }

    void IMonsterState.DoScare()
    {
    }

    void IMonsterState.DoLove()
    {
    }
}
