using COSE.Text;

public class JiawenTextInteraction : TextInteraction
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
