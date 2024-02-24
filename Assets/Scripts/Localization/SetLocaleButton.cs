using UnityEngine;
using UnityEngine.Localization.Settings;

public class SetLocaleButton : MenuButton
{
    [SerializeField] private Locale _locale;

    public static event System.Action OnChooseLocale;

    private enum Locale
    {
        rus,
        eng
    }

    // Rework
    protected override void OnClickAction()
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
