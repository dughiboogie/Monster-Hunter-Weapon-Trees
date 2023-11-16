using UnityEngine;

public class DetailsView : MonoBehaviour
{
    [SerializeField]
    private PreviewPanelView previewPanelView;

    [SerializeField]
    private StatsPanelView statsPanelView;

    public void UpdateView(Weapon weapon)
    {
        ResetView();

        previewPanelView.UpdateView(weapon);
        statsPanelView.UpdateView(weapon);
    }

    private void ResetView()
    {
        previewPanelView.ResetView();
        statsPanelView.ResetView();
    }
}
