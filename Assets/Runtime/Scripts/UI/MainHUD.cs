using UnityEngine;

public class MainHUD: MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private UiOverlay[] overlays;
    public UiOverlay CurrentOverlay {get; private set;}

    public void ShowOverlay<T>() where T : UiOverlay
    {
        UiOverlay uiOverlay = FindOverlayByType<T>();

        if (uiOverlay != null && uiOverlay != CurrentOverlay)
        {
            if (CurrentOverlay != null) CurrentOverlay.gameObject.SetActive(false);

            CurrentOverlay = uiOverlay;
            CurrentOverlay.gameObject.SetActive(true);
        }


    }

    private UiOverlay FindOverlayByType<T>() where T : UiOverlay
    {
        foreach (var overlay in overlays)
        {
            if(overlay.GetType() == typeof(T))
            {
                return overlay;

            }
        }

        Debug.LogError($"can't find the type of the overlay: {typeof(T)} ");
        return null;
    }


}
