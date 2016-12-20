using UnityEngine;
using System.Collections;

public class MotorOutputController : SingletonUnity<MotorOutputController>
{

    public MotorOutputView view;

    public void StartMotorOutputView(Entity[] entities)
    {
        MotorOutput output = new MotorOutput ();
        output = Algorithm.CalculateMotor (entities);

        ViewManager.Instance.StartViewByPanelName (Constant.MotorOutputPanel);
        view.SetView (output);
    }

}

