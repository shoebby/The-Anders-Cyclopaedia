using UnityEngine;
using UnityEngine.UI;

public class CyclopaediaController : MonoBehaviour
{
    [SerializeField] private KeyCode toggleKey, flipLeftKey, flipRightKey;

    [SerializeField] private AudioClip openBookClip, closeBookClip, flipPageClip;

    [SerializeField] private GameObject bookPanel;

    [SerializeField] private GameObject[] pages;
    [SerializeField] private Transform leftPageParent;
    [SerializeField] private Transform rightPageParent;

    private int currentLeftPage;
    private int currentRightPage;

    private void Awake()
    {
        bookPanel.SetActive(false);

        currentLeftPage = 0;
        currentRightPage = 1;

        Instantiate(pages[currentLeftPage], leftPageParent);
        Instantiate(pages[currentRightPage], rightPageParent);

        //leftImage.sprite = pageSprites[currentLeftPage];
        //rightImage.sprite = pageSprites[currentRightPage];
    }
     
    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleCyclopaedia();
        }

        if (bookPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                FlipPage(0);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                FlipPage(1);
            }
        }
    }

    private void ToggleCyclopaedia()
    {
        if (!bookPanel.activeSelf)
            AudioSystem.Instance.PlayAClip(openBookClip);
        else if (bookPanel.activeSelf)
            AudioSystem.Instance.PlayAClip(closeBookClip);

        bookPanel.SetActive(!bookPanel.activeSelf);

        if (!Interactor.Instance.isEngaged)
        {
            Helpers.ToggleMovements();
            Helpers.ToggleInteractor();
            Helpers.ToggleCursorLock();
        }
    }

    //direction == 0: left
    //direction == 1: right
    private void FlipPage(int direction)
    {
        currentLeftPage =   direction == 0 ? currentLeftPage - 2    : direction == 1 ? currentLeftPage + 2  : currentLeftPage;
        currentRightPage =  direction == 0 ? currentRightPage - 2   : direction == 1 ? currentRightPage + 2 : currentRightPage;

        if (currentRightPage > pages.Length)
        {
            currentLeftPage -= 2;
            currentRightPage -= 2;
            return;
        }
        
        if (currentRightPage < 0)
        {
            currentLeftPage += 2;
            currentRightPage += 2;
            return;
        }

        SetPages();
    }

    private void SetPages()
    {
        leftPageParent.DestroyChildren();
        rightPageParent.DestroyChildren();

        Instantiate(pages[currentLeftPage], leftPageParent);
        Instantiate(pages[currentRightPage], rightPageParent);

        AudioSystem.Instance.PlayAClip(flipPageClip);
    }

    public void GoToPage(int pageNumber)
    {
        currentLeftPage = pageNumber;
        currentRightPage = pageNumber + 1;

        SetPages();
    }
}
