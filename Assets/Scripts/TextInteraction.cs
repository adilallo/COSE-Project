using UnityEngine;
using TMPro;
using COSE.Sphere;
using COSE.Coin;
using UnityEngine.Localization.Settings;

namespace COSE.Text
{
    public class TextInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject _textUI;
        [SerializeField] protected TextMeshProUGUI _text;

        protected virtual void OnEnable()
        {
            SphereTrigger.OnSphereTriggered += ActivateText;
            CoinTrigger.OnCoinTriggered += ActivateText;
        }

        protected virtual void OnDisable()
        {
            SphereTrigger.OnSphereTriggered -= ActivateText;
            CoinTrigger.OnCoinTriggered -= ActivateText;
        }

        protected void Start()
        {
            DeactivateAllTexts();
        }

        protected void ActivateText(string text)
        {
            if (_textUI.activeSelf == false)
            {
                _textUI.SetActive(true);
            }

            this._text.text = LocalizationSettings.StringDatabase.GetLocalizedString(text);
           // Invoke(nameof(DeactivateAllTexts), 100f);
        }

        protected virtual void DeactivateAllTexts()
        {
            _textUI.SetActive(false);
            _text.text = default;
        }
    }
}
