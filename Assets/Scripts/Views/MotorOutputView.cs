using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MotorOutputView : MonoBehaviour
{
    private int num = 3;

    public Text T_Num;
    public Text T_P;
    public Text T_H1;
    public Text T_H2;
    public Text T_EH;
    public Text T_J1;
    public Text T_J2;
    public Text T_EJ;
    public Text T_S1;
    public Text T_S2;
    public Text T_ES;
    public Text T_T1;
    public Text T_T2;
    public Text T_ET;

    void OnEnable()
    {
        UserView.Instance.SetProfilePosition (true);
        UserView.Instance.Button_Back.SetActive (true);
        UserView.Instance.Button_Back.GetComponent<Button> ().onClick.RemoveAllListeners ();
        UserView.Instance.Button_Back.GetComponent<Button> ().onClick.AddListener (delegate {
            OnBackClick();
        });
    }

    private void OnBackClick ()
    {
        ViewManager.Instance.BackToBeforeView ();
    }

    public void SetView(MotorOutput motorOutput)
    {
        T_Num.text = num.ToString();
        T_P.text = motorOutput.pd.ToString ();
        T_H1.text = motorOutput.H1.ToString ();
        T_H2.text = motorOutput.H2.ToString ();
        T_EH.text = motorOutput.EH.ToString ();
        T_J1.text = motorOutput.J1.ToString ();
        T_J2.text = motorOutput.J2.ToString ();
        T_EJ.text = motorOutput.EJ.ToString ();
        T_S1.text = motorOutput.S1.ToString ();
        T_S2.text = motorOutput.S2.ToString ();
        T_ES.text = motorOutput.ES.ToString ();
        T_T1.text = motorOutput.T1.ToString ();
        T_T2.text = motorOutput.T2.ToString ();
        T_ET.text = motorOutput.ET.ToString ();
    }

}

