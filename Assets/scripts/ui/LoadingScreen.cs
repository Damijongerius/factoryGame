
using UnityEngine;


public class LoadingScreen : MonoBehaviour
{
    RectTransform backgroundTrans;
    RectTransform childTrans;
    private void Awake()
    {
        backgroundTrans = gameObject.GetComponent<RectTransform>();
        if (!ProfileManager.playing)
        {
            InvokeRepeating(nameof(StartAnim), 0.00002f, 0.00002f);
        }

        foreach(Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            if(child.gameObject.name == "Progress")
            {
                childTrans = child.GetComponent<RectTransform>();
                InvokeRepeating(nameof(LoadAnim), 0.001f, 0.001f);
            }
        }
    }

    private void StartAnim()
    {
        if(backgroundTrans.offsetMin.y > 0)
        {
            backgroundTrans.offsetMin = new Vector2(backgroundTrans.offsetMin.x, backgroundTrans.offsetMin.y - 0.02f);
            backgroundTrans.anchoredPosition = new Vector2(backgroundTrans.offsetMin.x, backgroundTrans.offsetMin.y - 0.02f);
        }
        else
        {
            CancelInvoke(nameof(StartAnim));
            ProfileManager.getObject().ActualLoad();
        }
            
    }

    private void EndAnim()
    {
        if (backgroundTrans.offsetMin.y < 600)
        {
            backgroundTrans.offsetMin = new Vector2(backgroundTrans.offsetMin.x, backgroundTrans.offsetMin.y + 0.1f);
            backgroundTrans.anchoredPosition = new Vector2(backgroundTrans.offsetMin.x, backgroundTrans.offsetMin.y + 0.1f);
        }
        else
        {
            CancelInvoke(nameof(EndAnim));
        }

    }

    private void LoadAnim()
    {
        if (childTrans.anchoredPosition.x < 0)
        {
            childTrans.anchoredPosition = new Vector2(childTrans.anchoredPosition.x + 0.1f, childTrans.anchoredPosition.y);
        }
        else
        {
            CancelInvoke(nameof(LoadAnim));
            InvokeRepeating(nameof(EndAnim), 0.00002f, 0.00002f);
        }
    }
}
