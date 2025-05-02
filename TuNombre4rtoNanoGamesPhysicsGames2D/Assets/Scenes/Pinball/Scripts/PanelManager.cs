using UnityEngine;
using static Gavryk.Physics.BlackHole.MovementOVNI;
using static InputPlayer;

public class PanelManager : MonoBehaviour {

    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLose;

    #region Victory&LosePanel
    public void VictoryPanel() {
        panelWin.SetActive(true);
        panelLose.SetActive(false);
    }
    public void LosePanel() {
        panelLose.SetActive(true);
        panelWin.SetActive(false);
    }

    #endregion Victory&LosePanel
}
