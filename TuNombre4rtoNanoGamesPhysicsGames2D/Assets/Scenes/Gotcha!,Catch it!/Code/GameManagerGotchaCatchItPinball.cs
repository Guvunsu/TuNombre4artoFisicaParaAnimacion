using TMPro;
using UnityEngine;
using static Gavryk.Physics.BlackHole.MovementOVNI;
using static InputPlayer;
// agregar el sistema de puntos 
public class GameManagerGotchaCatchItPinball : MonoBehaviour
{
    #region ENUM
    public enum GameState
    {
        PLAYING,
        WIN,
        LOSE
    }
    public enum fsmPlayer
    {
        STOP_IT
    }

    #endregion ENUM

    #region Variables

    [SerializeField] GameState gameFSM;
    [SerializeField] fsmPlayer playerFSM;

    //[SerializeField] GameObject panelWin;
    //[SerializeField] GameObject panelLose;

    [SerializeField] Transform basket8x;
    [SerializeField] Transform basket4x;
    [SerializeField] Transform basket2x;

    [SerializeField] TMP_Text scoreUpdate;
    [SerializeField] TMP_Text ballCount;

    #endregion Variables

    void Start()
    {

    }

    void Update()
    {

    }

    #region CanastaThings

    public void MultipliquerBasket()
    {

    }

    #endregion CanastaThings

    #region Pelotas
    public void BallCounts()
    {

    }

    #endregion Pelotas

    #region Score
    public void UpdateScore()
    {

    }

    public void OnTriggerEnter(Collider other)
    {

        UpdateScore();
    }
    #endregion Score

    //#region Victory&LosePanel
    //public void VictoryPanel()
    //{
    //    if (gameFSM != GameState.PLAYING)
    //    {
    //        return;
    //    }
    //    gameFSM = GameState.WIN;
    //    panelWin.SetActive(true);
    //    playerFSM = fsmPlayer.STOP_IT;
    //    panelLose.SetActive(false);
    //}
    //public void LosePanel()
    //{
    //    if (gameFSM != GameState.PLAYING)
    //    {
    //        return;
    //    }
    //    gameFSM = GameState.LOSE;
    //    panelLose.SetActive(true);
    //    playerFSM = fsmPlayer.STOP_IT;
    //    panelWin.SetActive(false);
    //}

    //#endregion Victory&LosePanel
}
