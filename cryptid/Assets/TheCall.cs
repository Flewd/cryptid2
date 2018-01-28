using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCall : MonoBehaviour {

	private AudioClip[] playerRecordings;
    private AudioClip[] CurrentLanguagePrompts;
	public AudioClip[] callPrompts; // set in the editor
    public AudioClip[] callPromptsKorean;
	private float[] answerTimings; 
	private AudioClip[] tooQuiet; // set in the editor
	private AudioClip[] tooLoud; // set in the editor
	private float[] playerLoudness; 

	private AudioSource ace; 
	private float[] clipSampleData; // an array used in the "loudness" calculation

	private const float VOLUME = 8.0f; 

	public static int SAMPLE_RATE = 44100;

    [SerializeField]
    MonsterController monsterController;

	// Use this for initialization
	void Start () {
		ace = GetComponent<AudioSource>(); 
		ace.volume = VOLUME; 
		answerTimings = new float[]{4, 5.0f, 4.0f, 3.0f, 3.0f, 1.0f, 1f};

        print(LanguageSelection.SelectedLanguage);
        switch (LanguageSelection.SelectedLanguage)
        {
            case LanguageSelection.SupportedLanugages.English:
                CurrentLanguagePrompts = callPrompts;
                break;
            case LanguageSelection.SupportedLanugages.Korean:
                CurrentLanguagePrompts = callPromptsKorean;
                break;
            default:
                CurrentLanguagePrompts = callPrompts;
                break;
        }
       


        playerLoudness = new float[CurrentLanguagePrompts.Length];
		playerRecordings = new AudioClip[CurrentLanguagePrompts.Length];
		playerRecordings = new AudioClip[CurrentLanguagePrompts.Length];
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void Call() {

		StartCoroutine(WaitForClip(CurrentLanguagePrompts[0].length, 0));
	}

	// waits for the audio to finish playing before beginning recording
	IEnumerator WaitForClip(float time, int index)
    {
		Debug.Log("question " + index);
		ace.clip = CurrentLanguagePrompts[index]; 
		ace.Play(); 

        yield return new WaitForSeconds(time);
        Debug.Log("wait time completed after " + time + " seconds");

		StartCoroutine(WaitForRecording(answerTimings[index], index));

    }

    // waits for the player response before looping back into the WaitForClip IEnumerator
    // to play the next recording
    IEnumerator WaitForRecording(float time, int index) 
    {
    	Debug.Log("waiting for recording...");
		AudioClip recording = Microphone.Start("Built-in Microphone", false, (int)time, SAMPLE_RATE);

		yield return new WaitForSeconds(time);

		playerRecordings[index] = recording; 
		playerLoudness[index] = AnalyzeLoudness(playerRecordings[index]);

        if (index < CurrentLanguagePrompts.Length - 1)
        {
            StartCoroutine(WaitForClip(CurrentLanguagePrompts[index + 1].length, index + 1));
        }
        else
        {
           float average = GetBaseline();
            if(average > 0.02f)
            {
                print("monster scared");
                monsterController.DoScare();
            }
            else if(average > 0.000001)
            {
                print("monster loved");
                monsterController.DoLove();
            }
            Constants.phoneCallOver = true;
        }
    }

    // for debugging 
    void PlayResult(int index) {
    	ace.clip = playerRecordings[index]; 
    	ace.Play(); 
    	Debug.Log("Playing clip " + index); 
    }

    // calculates the average loudness of the given clip
	public float AnalyzeLoudness(AudioClip clip) {
		float clipLength = ace.clip.length; 

		clipSampleData = new float[(SAMPLE_RATE * 5)];
		clip.GetData(clipSampleData, 0);

		float clipLoudness = 0f;

	    foreach (var sample in clipSampleData) {
	         clipLoudness += Mathf.Abs(sample);
	     }
	     clipLoudness /= (SAMPLE_RATE * clipLength);

        Debug.Log("Clip loudness assessment: " + clipLoudness);

        return clipLoudness; 
	}

	// calculates and returns the baseline loudness
	float GetBaseline() {
		float average = 0.0f; 
		for(int i = 0; i < playerLoudness.Length; i++)
		{
			average += playerLoudness[i]; 
		}

		average /= playerLoudness.Length; 

		return average; 
	}
}

