using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackdropCanvasScript : Singleton<BackdropCanvasScript>
{
    [SerializeField] private Image backdropImage;
    [SerializeField] private Image secondaryBackdropImage;

    private bool isCrossfading = false;

    private new void Awake()
    {
        base.Awake();

        secondaryBackdropImage.enabled = false;
    }

    public void ChangeBackdrop(Sprite sprite)
    {
        backdropImage.sprite = sprite;
    }

    public void CrossfadeBackdrop(Sprite sprite)
    {
        secondaryBackdropImage.sprite = sprite;
        secondaryBackdropImage.enabled = true;
        isCrossfading = true;

        StartCoroutine(Fade(null));
    }

    public void FadeBackdrop(string targetScene)
    {
        StartCoroutine(Fade(targetScene));
    }

    private IEnumerator Fade(string scene)
    {
        Color initialColor = backdropImage.color;
        Color targetColor = new Color();
        float elapsedTime = 0f;
        float fadeDuration = 3f;

        if (isCrossfading)
            fadeDuration = 1.5f;

        if (initialColor.a > 0f )
            targetColor = new Color(backdropImage.color.r, backdropImage.color.g, backdropImage.color.b, 0f);
        else if (initialColor.a == 0f)
            targetColor = new Color(backdropImage.color.r, backdropImage.color.g, backdropImage.color.b, 1f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            backdropImage.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }

        if (elapsedTime > fadeDuration && isCrossfading)
        {
            backdropImage.sprite = secondaryBackdropImage.sprite;
            backdropImage.color = new Color(1f, 1f, 1f, 1f);
            secondaryBackdropImage.enabled = false;
            isCrossfading = false;
        }

        if (scene != null)
        {
            Helpers.LoadScene(scene);
        }
    }
}
