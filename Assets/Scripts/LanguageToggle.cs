using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageToggle : MonoBehaviour
{
    public GameObject en;
    public GameObject pl;

    public void OnToggle()
    {
        if (en.activeSelf)
        {
            en.SetActive(false);
            pl.SetActive(true);
            SettingManager.instance.ChangeLocale("pl");
        }
        else
        {
            en.SetActive(true);
            pl.SetActive(false);
            SettingManager.instance.ChangeLocale("en");
        }
    }

    public void RefreshStatus()
    {
        Locale currentLocale = LocalizationSettings.SelectedLocale;
        if (currentLocale == LocalizationSettings.AvailableLocales.GetLocale("en"))
        {
            en.SetActive(true);
            pl.SetActive(false);
        }
        else
        {
            en.SetActive(false);
            pl.SetActive(true);
        }
    }
}
