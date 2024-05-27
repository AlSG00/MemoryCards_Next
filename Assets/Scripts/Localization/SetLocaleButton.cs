using UnityEngine;
using UnityEngine.Localization.Settings;

public class SetLocaleButton : MenuButton
{
    [SerializeField] private Locale _locale;

    public static event System.Action OnChooseLocale;

    private enum Locale
    {
        eng,
        rus
    }

    protected override void OnClickAction()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)_locale];

        OnChooseLocale?.Invoke();
    }
}
