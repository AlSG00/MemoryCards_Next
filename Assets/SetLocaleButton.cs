using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

[RequireComponent(typeof(MenuButton))]
public class SetLocaleButton : MonoBehaviour, IButtonAction
{
    private enum Locale
    {
        rus,
        eng
    }

    [SerializeField] private Locale _locale;

    public static event System.Action OnChooseLocale;

    public void OnClickAction()
    {
        switch (_locale)
        {
            case Locale.rus:
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
                break;

            case Locale.eng:
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
                break;
        }

        OnChooseLocale?.Invoke();
    }
}
