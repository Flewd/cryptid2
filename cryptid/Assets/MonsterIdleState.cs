using UnityEngine;
using System.Collections;
using System;

public class MonsterIdleState : IMonsterState
{
    MonsterController monsterController;

    enum ScareState {awaitingDialog, scared, loved};
    ScareState currentState = ScareState.awaitingDialog;

    GameObject player;

    MonsterEyes monsterEyes;

    public MonsterIdleState(MonsterController _monsterController)
    {
        monsterController = _monsterController;
        player = GameObject.FindGameObjectWithTag("Player");
        monsterEyes = monsterController.GetComponentInChildren<MonsterEyes>();
    }

    void IMonsterState.Start()
    {
        monsterEyes.ChangeEyeColor(MonsterEyes.EyeColors.yellow);
    }

    void IMonsterState.Update()
    {
        float angle = 40; // should be a little less than field of view
        if (Vector3.Angle(player.transform.forward, monsterController.transform.position - player.transform.position) < angle)
        {
            if (currentState != ScareState.awaitingDialog)
            {
                monsterController.StartCoroutine(waitThenExecuteReaction());
            }
        }

        // debug inputs should be replaced by microphone detection
        if (Input.GetKeyDown(KeyCode.P))
        {
            this.monsterController.DoScare();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            this.monsterController.DoLove();
        }

        monsterController.CheckDistanceToPlayer();
    }

    IEnumerator waitThenExecuteReaction()
    {
        yield return new WaitForSeconds(0);
        ExecuteReaction();
    }

    void IMonsterState.End()
    {
    }

    void IMonsterState.DoScare()
    {
        currentState = ScareState.scared;
    }

    void IMonsterState.DoLove()
    {
        currentState = ScareState.loved;
    }

    public void ExecuteReaction()
    {
        switch (currentState)
        {
            case ScareState.loved:
                monsterController.SwitchState(new MonsterRelocationState(monsterController, true));
                break;
            case ScareState.scared:
                monsterController.SwitchState(new MonsterRelocationState(monsterController, false));
                break;
        }
    }
}
