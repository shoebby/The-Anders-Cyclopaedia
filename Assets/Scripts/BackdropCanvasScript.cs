using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackdropCanvasScript : Singleton<BackdropCanvasScript>
{
    [SerializeField] private Image backdropImage;

    private void Start()
    {
        //start backdrop as transparent
        backdropImage.color = new Color(1f, 1f, 1f, 0f);
    }

    public void ChangeBackdrop(Sprite sprite)
    {
        backdropImage.sprite = sprite;
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

        if (scene != null)
            SceneManager.LoadScene(scene);
    }
}
