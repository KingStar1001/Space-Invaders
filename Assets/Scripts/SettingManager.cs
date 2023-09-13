using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using SpaceInvaders;

public class SettingManager : MonoBehaviour
{
    public static SettingManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        StopCoroutine("LoadSavedLocale");
        StartCoroutine("LoadSavedLocale");
    }

    public void ChangeLocale(string localeString)
    {
        Locale locale = LocalizationSettings.AvailableLocales.GetLocale(localeString);

        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
            PlayerPrefs.SetString("language", localeString);
        }
    }

    IEnumerator LoadSavedLocale()
    {
        yield return LocalizationSettings.InitializationOperation;

        if (PlayerPrefs.HasKey("language"))
        {
            ChangeLocale(PlayerPrefs.GetString("language"));
        }

        Utils.SendMessage("LanguageToggle", "RefreshStatus", null);
    }
}
