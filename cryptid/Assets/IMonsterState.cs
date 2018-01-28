using UnityEngine;
using System.Collections;

public interface IMonsterState
{
    void Start();
    void Update();
    void End();
    void DoScare();
    void DoLove();
}
