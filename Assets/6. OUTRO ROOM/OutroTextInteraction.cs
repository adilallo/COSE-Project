using COSE.Text;

[System.Serializable]
public class OutroTextInteraction : TextInteraction
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
