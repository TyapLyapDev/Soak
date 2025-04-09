using UnityEngine;

public abstract class TabContainer : MonoBehaviour
{
    public void Show() =>
        gameObject.SetActive(true);

    public void Hide() =>
        gameObject.SetActive(false);
}