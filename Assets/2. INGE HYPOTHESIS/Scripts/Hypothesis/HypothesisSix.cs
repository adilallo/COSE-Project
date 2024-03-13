using UnityEngine;
using UnityEngine.Video;
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

        private static VideoPlayer videoPlayer;

        private readonly string[] heroTexts = new string[]
        {
            @"The tab field complex is attributed a flipping or turning window because on the one hand it is a window to the internet as it harbours the website content. On the other hand, the browser seems to provide a back-side view of websites in two different ways. The first of these is always provided, the second can be reached by interacting with it. Normally, the HTML code of a website is occluded from the users, who need to go an extra step to have it displayed in their commercial browser. In .com, the HTML-code fills what the users will interpret as the tab field complex in a rather large and easy to read font. It seems to have been brought closer for inspection than its formatted-text version.
            The affordance of these two elements also underlines that reversal. The user can edit the HTML code text, but cannot even mark the formatted-text. Thus, JODI seem to swap surface and ‘subface’ (to use Frieder Nake’s expression), that is, the GUI level and the code level. However, the HTML code can only be reworked if a second instance of flipping hasn’t turned it away from the user’s sight. So, what could turn the HTML-text-field ‘away’?",
            @"The pentagon complex is shown here as two elements coupled, but it is optically complete only with all five pentagon complexes. As it also turns or flips parts of the website, it shares the same attribute as the tab field complex. coordinate value, but is position-sensitive and thus variable. Drawing a horizontal and a vertical line through this point, cuts the viewport plane into four sectors that have different ‘properties’ for this specific tab field complex.",
            @"The search facilitating complex is depicted by the red carpet it is rolling out for the users. Although not obvious at first glance, .com browser offers the user four different ways to start the web search for each website tab
Interpretation: 
Contrary to the general impression of bulkiness, in several ways .com surpasses conventional browsers in paving a way into the internet. First, each URL is present twice and both can be used to start a search. A search can be initiated by typing in a URL either in the URL-text-field or in the middle-bar-URL-text-field and pressing enter, or by double-clicking on the URL-square or on the middle-bar-URL-ball respectively. Thus, the opportunities to go and explore a website by chance or by intention are heightened/elevated.
",
            @"The search performing complex symbolically waves a red flag as it seeks to gather all the user’s attention. Even if users may be overwhelmed and have the impression that they need to fight to get to the internet content, the browser is actually emphasizing the core business of any browser, namely the event of the search process.
Here, multiple instances indicate that something is happening. The URL calling process is accompanied by a hectic horizontal movement pushing around the corresponding URL, by sound, by a colour change of the URL and by various fields popping up: grey and the ‘own’ colour, a field behind the URL. All these signals indicate that something is ‘coming’ from the internet. It is reminiscent of the good old letter shoot. While the website data parcel is underway, a specific sound accompanies its journey and a different gong advertises its arrival. The box can contain something or can be empty. In this light, the horizontal middle-bar containing the balls also mimics the old medium of postal transmission. The difference is only that their hectic back and forth movements symbolize the requirement in the networked world that the various bits and pieces of information must be gathered from different sites. The middle-bar indicates prominently that a search is taking place by serving as a highway for a nervous race. This is a rather different picture than the floating, orbiting and circling in outer space shown by other throbber animations.
And all this performed excitement has yet another effect: the long waiting time that was a major part of the early days of the internet, here, is filled with action. Users can lean back and enjoy the spectacle. This kind of jingle-entertainment is performed by six elements. Isn’t that again contributing to an improved user experience?
"
        };


        private void OnEnable()
        {
            SphereInteraction.SphereTriggered += OnSphereTriggered;
            HypothesisSixClickEvent.OnHeroModelClicked += UpdateLocalizedText;
            AnimationClickEvent.OnAnimationClicked += PlayVideoClip;
        }

        private void OnDisable()
        {
            SphereInteraction.SphereTriggered -= OnSphereTriggered;
            HypothesisSixClickEvent.OnHeroModelClicked -= UpdateLocalizedText;
            AnimationClickEvent.OnAnimationClicked -= PlayVideoClip;
        }

        private void Start()
        {
            if (videoPlayer == null)
            {
                videoPlayer = FindObjectOfType<VideoPlayer>();
            }
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

        private void PlayVideoClip(VideoClip clip)
        {
            if (videoPlayer != null)
            {
                videoPlayer.clip = clip;
                videoPlayer.Play();
            }
        }

    }
}
