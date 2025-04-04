using UnityEngine;

public abstract class WindowPanel : MonoBehaviour
{
    public void Enable() =>
        gameObject.SetActive(true);

    public void Disable() =>
        gameObject.SetActive(false);
}