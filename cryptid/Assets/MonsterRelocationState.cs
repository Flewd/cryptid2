using UnityEngine;
using System.Collections;
using System;

public class MonsterRelocationState : IMonsterState
{
    MonsterController monsterController;

    bool willMoveForward = false;

    HidingPoint currentDestination;

    MonsterEyes monsterEyes;

    public MonsterRelocationState(MonsterController _monsterController, bool moveForward )
    {
        monsterController = _monsterController;
        willMoveForward = moveForward;
        monsterEyes = monsterController.GetComponentInChildren<MonsterEyes>();
    }

    void IMonsterState.Start()
    {
        if (willMoveForward)
        {
            monsterEyes.ChangeEyeColor(MonsterEyes.EyeColors.green);
            currentDestination = MonsterHidingPointStorage.GetNextPointTowardsCenter(monsterController.transform.position);
            monsterController.monsterNavAgent.destination = currentDestination.transform.position;
        }
        else
        {
            monsterEyes.ChangeEyeColor(MonsterEyes.EyeColors.red);
            currentDestination = MonsterHidingPointStorage.GetNextPointTowardsCenter(monsterController.transform.position);
            monsterController.monsterNavAgent.destination = currentDestination.transform.position;
        }
        monsterController.monsterNavAgent.isStopped = false;
    }

    void IMonsterState.Update()
    {
        if (Vector3.Distance(monsterController.monsterNavAgent.destination, monsterController.transform.position) < 2.1f)
        {
            if (currentDestination.name != "Final") {
                monsterController.SwitchState(new MonsterIdleState(monsterController));
            }else{
                Constants.isEndGame = true;
                monsterController.SwitchState(new MonsterEndGameState(monsterController));
            }
        }
        // TODO Monster is still charging at the end of the game.
        if (currentDestination.name != "Final")
        {
            monsterController.CheckDistanceToPlayer();
        }
    }

    void IMonsterState.End()
    {
        monsterController.monsterNavAgent.Stop();
    }

    void IMonsterState.DoScare()
    {
    }

    void IMonsterState.DoLove()
    {
    }
}
