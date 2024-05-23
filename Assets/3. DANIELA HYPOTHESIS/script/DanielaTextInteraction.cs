using COSE.Text;

[System.Serializable]
public class DanielaTextInteraction : TextInteraction
{
    public void ActivateLayerText(string textKey)
    {
            ActivateText(textKey);
    }

    public void DeactivateAllTexts()
    {
        base.DeactivateAllTexts();
    }
}
