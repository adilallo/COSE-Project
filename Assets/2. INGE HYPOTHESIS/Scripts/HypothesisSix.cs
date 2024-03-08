using UnityEngine;
using COSE.Sphere;
using System.Collections;
using TMPro;
using COSE.Text;

namespace COSE.Hypothesis
{
    public class HypothesisSix : MonoBehaviour
    {
        [SerializeField] private TextInteraction textInteraction;
        [SerializeField] private GameObject heroModel1, heroModel2, heroModel3, heroModel4;
        [SerializeField] private TextMeshProUGUI localizedTextElement;
        [SerializeField] private GameObject scrollView;

        // Define the texts for each hero model directly in the script
        private readonly string[] heroTexts = new string[]
        {
            @"The tab field complex is attributed a flipping or turning window because on the one hand it is a window to the internet as it harbours the website content. On the other hand, the browser seems to provide a back-side view of websites in two different ways. The first of these is always provided, the second can be reached by interacting with it. Normally, the HTML code of a website is occluded from the users, who need to go an extra step to have it displayed in their commercial browser. In .com, the HTML-code fills what the users will interpret as the tab field complex in a rather large and easy to read font. It seems to have been brought closer for inspection than its formatted-text version.
            The affordance of these two elements also underlines that reversal. The user can edit the HTML code text, but cannot even mark the formatted-text. Thus, JODI seem to swap surface and ‘subface’ (to use Frieder Nake’s expression), that is, the GUI level and the code level. However, the HTML code can only be reworked if a second instance of flipping hasn’t turned it away from the user’s sight. So, what could turn the HTML-text-field ‘away’?",
            "Text for Hero 2",
            "Text for Hero 3",
            "Text for Hero 4"
        };


        private void OnEnable()
        {
            SphereInteraction.SphereTriggered += OnSphereTriggered;
            HypothesisSixClickEvent.OnHeroModelClicked += UpdateLocalizedText; // Listen to hero model click events
        }

        private void OnDisable()
        {
            SphereInteraction.SphereTriggered -= OnSphereTriggered;
            HypothesisSixClickEvent.OnHeroModelClicked -= UpdateLocalizedText; // Stop listening to hero model click events
        }

        private void OnSphereTriggered(int sphereIndex)
        {
            if (sphereIndex == 6)
            {
                StartCoroutine(ActivateHeroCoroutine(30f));
            }
        }

        private IEnumerator ActivateHeroCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            textInteraction.DeactivateAllTexts();
            // Optionally, activate the hero models here if needed
            heroModel1.SetActive(true);
            heroModel2.SetActive(true);
            heroModel3.SetActive(true);
            heroModel4.SetActive(true);
        }

        private void UpdateLocalizedText(int heroId)
        {
            scrollView.SetActive(true);
            if (heroId >= 1 && heroId <= heroTexts.Length)
            {
                localizedTextElement.text = heroTexts[heroId - 1];
            }
        }
    }
}
