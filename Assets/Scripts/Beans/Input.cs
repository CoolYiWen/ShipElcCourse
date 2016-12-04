using System.Collections;

public class Input
{
    public int id;
    public string name;
    public int num;
    public float pmax;
    public float p1;
    public float e;
    public float k21;
    public float k01;
	public int type1;
    public float k22;
    public float k02;
	public int type2;
    public float k23;
    public float k03;
	public int type3;
    public float k24;
    public float k04;
	public int type4;

    public Input()
    {}

	public Input(int id,string name,int num,float pmax,float p1,float e,
		float k21,float k01,int type1,
		float k22,float k02,int type2,
		float k23,float k03,int type3,
		float k24,float k04,int type4)
	{
		this.id = id;
		this.name = name;
		this.num = num;
		this.pmax = pmax;
		this.p1 = p1;
		this.e = e;
		this.k21 = k21;
		this.k01 = k01;
		this.type1 = type1;
		this.k22 = k22;
		this.k02 = k02;
		this.type2 = type2;
		this.k23 = k23;
		this.k03 = k03;
		this.type3 = type3;
		this.k24 = k24;
		this.k04 = k04;
		this.type4 = type4;
	}
}

