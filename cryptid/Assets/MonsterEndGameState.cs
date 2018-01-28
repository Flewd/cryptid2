using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
        monsterController.StartCoroutine(SwitchSceneAfterSeconds(3));
    }

    IEnumerator SwitchSceneAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("ComicBookEnding");
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
