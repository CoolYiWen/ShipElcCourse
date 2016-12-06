using System.Collections;

public class MotorOutput
{
    public float pd;//单台机组功率
    public int H1;//航行状态运行发电机台数
    public int H2;//航行状态备用发电机台数
    public float EH;//航行状态发电机负荷率
    public int J1;//进出港状态运行发电机台数
    public int J2;//进出港状态备用发电机台数
    public float EJ;//进出港状态发电机负荷率
    public int S1;//水上作业运行发电机台数
    public int S2;//水上作业备用发电机台数
    public float ES;//水上作业发电机负荷率
    public int T1;//停泊状态运行发电机台数
    public int T2;//停泊状态备用发电机台数
    public float ET;//停泊状态发电机负荷率

	public MotorOutput()
	{
	}
}

