using System.Collections;

public class Algorithm 
{
	//设备计算算法
	static public Output NeedCoefficientMethod(Input input)
	{
		Output output = new Output();
		output.p4 = input.p1 / input.e;
		output.p5 = input.num * output.p4;
		output.k1 = input.pmax / input.p1;

		output.k31 = output.k1 * input.k21;
		output.pn1 = output.k31 * input.k01 * output.p5;
		output.bRun=true;

		output.k32 = output.k1 * input.k22;
		output.pn2 = output.k32 * input.k02 * output.p5;
		output.bStartBack=true;

		output.k33 = output.k1 * input.k23;
		output.pn3 = output.k33 * input.k03 * output.p5;
		output.bWaterWork=true;

		output.k34 = output.k1 * input.k24;
		output.pn4 = output.k34 * input.k04 * output.p5;
		output.bStop=true;
		//jisuan
		if (output.p4 <= 0 && output.p5 <= 0 && output.k1 <= 0)
			return null;
		else
		{
			if (output.k31 <= 0 || output.pn1 <= 0)
			{
				output.k31 = -1;
				output.pn1 = -1;
				output.bRun = false;
			}
			if (output.k32 <= 0 || output.pn2 <= 0)
			{
				output.k32 = -1;
				output.pn2 = -1;
				output.bStartBack = false ;
			}
			if (output.k33 <= 0 || output.pn3 <= 0)
			{
				output.k33 = -1;
				output.pn3 = -1;
				output.bWaterWork = false;
			}
			if (output.k34 <= 0 || output.pn4 <= 0)
			{
				output.k34 = -1;
				output.pn4 = -1;
				output.bStop = false ;
			}

			return output;
		}
	}

    static public MotorOutput CalculateMotor(Entity[] entityList)
    {
        MotorOutput output = new MotorOutput();

		float[] arr1D=new float[50];
		int[] arr2D=new int[50];
		int[] Yun = new int[4];
		int[] Bei = new int[4];
		float[] P=new float[4];

		float ph = 0;
		float pj = 0;
		float ps = 0;
		float pt = 0;

		float max=0;
		float nf=3;
		float Pd=0;
		int H1 = 0;//航行状态运行发电机台数
		int H2 = 0;//航行状态备用发电机台数
		float EH=0;//航行状态发电机负荷率
		int J1 = 0;//进出港状态运行发电机台数
		int J2 = 0;//进出港状态备用发电机台数
		float EJ=0;//进出港状态发电机负荷率
		int S1 = 0;//水上作业运行发电机台数
		int S2 = 0;//水上作业备用发电机台数
		float ES=0;//水上作业发电机负荷率
		int T1 = 0;//停泊状态运行发电机台数
		int T2 = 0;//停泊状态备用发电机台数
		float ET=0;//停泊状态发电机负荷率

		for(int j=1;j<5;j++)
		{
			float PH1 = 0;
			float PH2 = 0;
			float PH3 = 0;
			float Ph1 = 0;
			float Ph2 = 0;
			float PH = 0;

			if(j==1)
			{
				for (int i = 0; i < entityList.Length; i++)
				{ 
					arr2D[i]=entityList[i].type1;
					arr1D[i]=entityList[i].pn1;
				}
			}
			if(j==2)
			{
				for (int i = 0; i < entityList.Length; i++)
				{ 
					arr2D[i]=entityList[i].type2;
					arr1D[i]=entityList[i].pn2;
				}
			}
			if(j==3)
			{
				for (int i = 0; i < entityList.Length; i++)
				{ 
					arr2D[i]=entityList[i].type3;
					arr1D[i]=entityList[i].pn3;
				}
			}
			if(j==4)
			{
				for (int i = 0; i < entityList.Length; i++)
				{ 
					arr2D[i]=entityList[i].type4;
					arr1D[i]=entityList[i].pn4;
				}
			}
			for (int i = 0; i < 50; i++)
			{
				if (arr2D[i] == 1)
				{
					PH1 = PH1 + arr1D[i];
				}
				if (arr2D[i] == 2)
				{
					PH2 = PH2 + arr1D[i];
				}
				if (arr2D[i] == 3)
				{
					PH3 = PH3 + arr1D[i];
				}
			}
			Ph1 = 0.9f * PH1;
			Ph2 = 0.6f * PH2;
			PH = Ph1 + Ph2;
			if(j==1)ph = PH * 1.05f;
			if(j==2)pj = PH * 1.05f;
			if(j==3)ps = PH * 1.05f;
			if(j==4)pt = PH * 1.05f;

		}//for循环算出ph,pj,ps,pt;


		if(ph>pj)max=ph;
		else max=pj;
		if(ps>max)max=ps;
		if(pt>max)max=pt;//求出ph,pj,ps,pt中的最大值

		Pd=max/(nf-1);//算出pd

		if (Pd <= 15) Pd = 15;
		else if (Pd > 15 && Pd <= 20) Pd = 20;
		else if (Pd > 20 && Pd <= 30) Pd = 30;
		else if (Pd > 30 && Pd <= 50) Pd = 50;
		else if (Pd >50 && Pd <= 90) Pd = 90;
		else Pd = 200;//将pd向上圆整为常用值

		P[0]=ph;
		P[1]=pj;
		P[2]=ps;
		P[3]=pt;//将ph,pj,ps,pt放在P【】数组中

		for(int i=0;i<4;i++)
		{
			if(Pd<P[i] && P[i]<(2*Pd))
			{
				Yun[i]=2;
				Bei[i]=1;
			}
			else
			{
				Yun[i]=1;
				Bei[i]=2;
			}
			if(i==0)EH=P[i]/(Yun[i]*Pd);
			if(i==1)EJ=P[i]/(Yun[i]*Pd);
			if(i==2)ES=P[i]/(Yun[i]*Pd);
			if(i==3)ET=P[i]/(Yun[i]*Pd);
		}//计算出每种状态下的运行发电机台数、备用发电机台数、发电机负荷率

		H1=Yun[0];
		H2=Bei[0];

		J1=Yun[1];
		J2=Bei[1];

		S1=Yun[2];
		S2=Bei[2];

		T1=Yun[3];
		T2=Bei[3];

		output.pd = Pd;
		output.H1 = H1;
		output.H2 = H2;
		output.EH = EH;
		output.J1 = J1;
		output.J2 = J2;
		output.EJ = EJ;
		output.S1 = S1;
		output.S2 = S2;
		output.ES = ES;
		output.T1 = T1;
		output.T2 = T2;
		output.ET = ET;
        return output;
    }

	//快速排序算法
	static public void QuickSortEntities(Entity[] entities, int left, int right)
	{
		if(left < right)
		{
			int id = entities[left].id;
			int low = left;
			int high = right;
			Entity entity = entities [left];

			while(low < high)
			{
				while(low < high && entities[high].id >= id)
				{
					high--;
				}
				entities[low] = entities[high];

				while(low < high && entities[low].id <= id)
				{
					low++;
				}
				entities[high] = entities[low];
			}

			entities[low] = entity;
			QuickSortEntities(entities,left,low-1);
			QuickSortEntities(entities,low+1,right);
		}
	}

}

