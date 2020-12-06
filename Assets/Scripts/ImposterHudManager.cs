using UnityEngine;
using UnityEngine.UI;

public class ImposterHudManager : MonoBehaviour
{
    GameObject myplayer;

    private KillTrigger killtrigger;
    private ReportTrigger reportTrigger;
    private VentTrigger ventTrigger;

    public Button killBtn;
    public Button reportBtn;


    private void OnEnable() => Player.OnThisPlayerDead += Player_OnThisPlayerDead;

    private void OnDisable() => Player.OnThisPlayerDead -= Player_OnThisPlayerDead;

    void Start()
    {
        myplayer = PlayersList.GetMyPlayer();
        killtrigger = myplayer.GetComponent<KillTrigger>();
        reportTrigger = myplayer.GetComponent<ReportTrigger>();
        ventTrigger = myplayer.GetComponent<VentTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        //If someone is near make killBtn interactable
        //Dont show the killBtn when inside Vent.
        if (killtrigger.canKill && !ventTrigger.insideVent)
            killBtn.interactable = true;
        else
            killBtn.interactable = false;

        if (reportTrigger.canReport)
            reportBtn.interactable = true;
        else
            reportBtn.interactable = false;
    }

    public void OnClick_KillBtn() => myplayer.GetComponent<KillTrigger>().OnKill();

    public void OnClick_VentBtn() => myplayer.GetComponent<VentTrigger>().OnVent();

    public void OnClick_ReportBtn() => myplayer.GetComponent<ReportTrigger>().OnReport();

    private void Player_OnThisPlayerDead()
    {
        killBtn.gameObject.SetActive(false);
        reportBtn.gameObject.SetActive(false);
    }

}
