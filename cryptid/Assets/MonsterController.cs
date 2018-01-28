using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour {

    IMonsterState CurrentState;

    [System.NonSerialized]
    public NavMeshAgent monsterNavAgent;

    GameObject player;

    public int currentIndex = 0;
    public AudioClip[] parrotAudio = new AudioClip[15];

    public TheCall theCall;

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

    public void StartRecording(float time)
    {
        StartCoroutine(WaitForRecording(time));
    }

    IEnumerator WaitForRecording(float time)
    {
        Debug.Log("waiting for recording...");
        AudioClip recording = Microphone.Start("Built-in Microphone", false, (int)time, TheCall.SAMPLE_RATE);

        yield return new WaitForSeconds(time);

        parrotAudio[currentIndex] = recording;
        currentIndex++;

        if(currentIndex >= parrotAudio.Length)
        {
            currentIndex = 0;
        }
        float average = theCall.AnalyzeLoudness(recording);

        if (average > 0.02f)
        {
            print("monster scared");
            DoScare();
        }
        else if (average > 0.004)
        {
            print("monster loved");
            DoLove();
        }
    }
}
