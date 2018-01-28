using UnityEngine;
using System.Collections;
using System;

public class MonsterEndGameState : IMonsterState
{
    MonsterController monsterController;

    GameObject player;

    MonsterEyes monsterEyes;

    public MonsterEndGameState(MonsterController _monsterController)
    {
        monsterController = _monsterController;
        player = GameObject.FindGameObjectWithTag("Player");
        monsterEyes = monsterController.GetComponentInChildren<MonsterEyes>();
    }

    void IMonsterState.Start()
    {
        monsterEyes.ChangeEyeColor(MonsterEyes.EyeColors.green);
    }

    void IMonsterState.Update()
    {
    }

    void IMonsterState.End()
    {
    }

    void IMonsterState.DoScare()
    {
    }

    void IMonsterState.DoLove()
    {
    }
}
