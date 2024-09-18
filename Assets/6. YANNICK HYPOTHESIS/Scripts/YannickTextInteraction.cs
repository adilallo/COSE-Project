using COSE.Text;

public class YannickTextInteraction : TextInteraction
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
