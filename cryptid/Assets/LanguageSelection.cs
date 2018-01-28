using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageSelection : MonoBehaviour {

    public enum SupportedLanugages { English, Spanish, Chinese, Italian, French, Korean, Japanese };

    public static SupportedLanugages SelectedLanguage = SupportedLanugages.English;

    public void SetLanguage(string newLanguage)
    {
        switch (newLanguage)
        {
            case "English": SelectedLanguage = SupportedLanugages.English; break;
            case "Spanish": SelectedLanguage = SupportedLanugages.Spanish; break;
            case "Chinese": SelectedLanguage = SupportedLanugages.Chinese; break;
            case "Italian": SelectedLanguage = SupportedLanugages.Italian; break;
            case "French": SelectedLanguage = SupportedLanugages.French; break;
            case "Korean": SelectedLanguage = SupportedLanugages.Korean; break;
            case "Japanese": SelectedLanguage = SupportedLanugages.Japanese; break;
        }
        print("language is now: " + SelectedLanguage);
        SceneManager.LoadScene("MainScene");
    }
}
