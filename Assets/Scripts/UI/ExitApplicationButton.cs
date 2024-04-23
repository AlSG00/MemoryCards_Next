using UnityEngine;

public class ExitApplicationButton : MenuButton
{
    protected override void OnClickAction()
    {
        Application.Quit();
    }
}
