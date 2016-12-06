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

    static public MotorOutput CalculateMotor(Entity[] entities)
    {
        MotorOutput output = new MotorOutput();





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

			while(low < high)
			{
				while(low < high && entities[high].id > id)
				{
					high--;
				}
				entities[low] = entities[high];

				while(low < high && entities[low].id < id)
				{
					low++;
				}
				entities[high] = entities[low];
			}

			entities[low] = entities[left];
			QuickSortEntities(entities,left,low-1);
			QuickSortEntities(entities,low+1,right);
		}
	}

}

