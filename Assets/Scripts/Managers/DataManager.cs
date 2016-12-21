using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;

public class DataManager : SingletonUnity<DataManager> {

	private SQLiteHelper sqlite;

	void Start()
	{
		sqlite = new SQLiteHelper ("data source=offlinedata.db");
	}

	void OnDestroy()
	{
		sqlite.CloseConnection ();
	}

    public bool IsCollectionExist(Entity entity)
    {
        SqliteDataReader reader = sqlite.ReadTable ("collections", new string[]{ "id" }, new string[]{ "name" }, new string[]{ "=" }, new string[]{ entity.name });
        while (reader.Read())
        {
            if(reader.GetInt32(reader.GetOrdinal("id")) == entity.id)
            {
                return true;
            }
        } 
        return false;
    }

	public void SaveCollection(Entity entity)
	{
		sqlite.InsertValues ("collections", new string[] {
			entity.id.ToString (),
			"'" + entity.name + "'",
			entity.num.ToString (),
			entity.pmax.ToString (),
			entity.p1.ToString (),
			entity.e.ToString (),
			entity.p4.ToString (),
			entity.p5.ToString (),
			entity.k1.ToString (),
			entity.k21.ToString (),
			entity.k31.ToString (),
			entity.k01.ToString (),
			entity.pn1.ToString (),
			entity.type1.ToString (),
			entity.k22.ToString (),
			entity.k32.ToString (),
			entity.k02.ToString (),
			entity.pn2.ToString (),
			entity.type2.ToString (),
			entity.k23.ToString (),
			entity.k33.ToString (),
			entity.k03.ToString (),
			entity.pn3.ToString (),
			entity.type3.ToString (),
			entity.k24.ToString (),
			entity.k34.ToString (),
			entity.k04.ToString (),
			entity.pn4.ToString (),
			entity.type4.ToString (),
			entity.bRun.ToString (),
			entity.bStartBack.ToString (),
			entity.bWaterWork.ToString (),
			entity.bStop.ToString (),
		});
	}

    public void DeleteCollection(string name)
	{
		sqlite.DeleteValuesAND ("collections", new string[]{ "name" }, new string[]{ "=" }, new string[]{ name });
	}

	public void UpdateCollection(Entity entity)
	{
        DeleteCollection (entity.name);
		SaveCollection (entity);
	}

	public Entity[] GetCollections()
	{
		Entity[] entities = new Entity[200];
		SqliteDataReader reader = sqlite.ReadFullTable ("collections");
		int i = 0;
		while(reader.Read()) 
		{
			Entity entity = new Entity ();
			entity.id = reader.GetInt32(reader.GetOrdinal("id"));
			entity.name = reader.GetString(reader.GetOrdinal("name"));
			entity.num = reader.GetInt32(reader.GetOrdinal("num"));
			entity.pmax = reader.GetFloat(reader.GetOrdinal("pmax"));
			entity.p1 = reader.GetFloat(reader.GetOrdinal("p1"));
			entity.e = reader.GetFloat(reader.GetOrdinal("e"));
			entity.k21 = reader.GetFloat(reader.GetOrdinal("k21"));
			entity.k01 = reader.GetFloat(reader.GetOrdinal("k01"));
			entity.type1 = reader.GetInt32(reader.GetOrdinal("type1"));
			entity.k22 = reader.GetFloat(reader.GetOrdinal("k22"));
			entity.k02 = reader.GetFloat(reader.GetOrdinal("k02"));
			entity.type2 = reader.GetInt32(reader.GetOrdinal("type2"));
			entity.k23 = reader.GetFloat(reader.GetOrdinal("k23"));
			entity.k03 = reader.GetFloat(reader.GetOrdinal("k03"));
			entity.type3 = reader.GetInt32(reader.GetOrdinal("type3"));
			entity.k24 = reader.GetFloat(reader.GetOrdinal("k24"));
			entity.k04 = reader.GetFloat(reader.GetOrdinal("k04"));
			entity.type4 = reader.GetInt32(reader.GetOrdinal("type4"));

			entity.p4 = reader.GetFloat(reader.GetOrdinal("p4"));
			entity.p5 = reader.GetFloat(reader.GetOrdinal("p5"));
			entity.k1 = reader.GetFloat(reader.GetOrdinal("k1"));
			entity.k31 = reader.GetFloat(reader.GetOrdinal("k31"));
			entity.pn1 = reader.GetFloat(reader.GetOrdinal("pn1"));
			entity.k32 = reader.GetFloat(reader.GetOrdinal("k32"));
			entity.pn2 = reader.GetFloat(reader.GetOrdinal("pn2"));
			entity.k33 = reader.GetFloat(reader.GetOrdinal("k33"));
			entity.pn3 = reader.GetFloat(reader.GetOrdinal("pn3"));
			entity.k34 = reader.GetFloat(reader.GetOrdinal("k34"));
			entity.pn4 = reader.GetFloat(reader.GetOrdinal("pn4"));

			entity.bRun = reader.GetBoolean(reader.GetOrdinal("bRun"));
			entity.bStartBack = reader.GetBoolean(reader.GetOrdinal("bStartBack"));
			entity.bWaterWork = reader.GetBoolean(reader.GetOrdinal("bWaterWork"));
			entity.bStop = reader.GetBoolean(reader.GetOrdinal("bStop"));

			entities [i] = entity;
			i++;
		}

		return (Entity[])Algorithm.Redim (entities, i);
	}


	public void SaveHistory(Entity entity)
	{
		sqlite.InsertValues ("history", new string[] {
			entity.id.ToString (),
			"'" + entity.name + "'",
			entity.num.ToString (),
			entity.pmax.ToString (),
			entity.p1.ToString (),
			entity.e.ToString (),
			entity.p4.ToString (),
			entity.p5.ToString (),
			entity.k1.ToString (),
			entity.k21.ToString (),
			entity.k31.ToString (),
			entity.k01.ToString (),
			entity.pn1.ToString (),
			entity.type1.ToString (),
			entity.k22.ToString (),
			entity.k32.ToString (),
			entity.k02.ToString (),
			entity.pn2.ToString (),
			entity.type2.ToString (),
			entity.k23.ToString (),
			entity.k33.ToString (),
			entity.k03.ToString (),
			entity.pn3.ToString (),
			entity.type3.ToString (),
			entity.k24.ToString (),
			entity.k34.ToString (),
			entity.k04.ToString (),
			entity.pn4.ToString (),
			entity.type4.ToString (),
			entity.bRun.ToString (),
			entity.bStartBack.ToString (),
			entity.bWaterWork.ToString (),
			entity.bStop.ToString (),
			TimeTool.ConvertDateTimeToInt(System.DateTime.Now).ToString()
		});
	}

	public History[] GetHistories()
	{
		History[] histories = new History[200];
		SqliteDataReader reader = sqlite.ReadFullTable ("collections");
		int i = 0;
		while(reader.Read()) 
		{
			History history = new History ();
			history.id = reader.GetInt32(reader.GetOrdinal("id"));
			history.name = reader.GetString(reader.GetOrdinal("name"));
			history.num = reader.GetInt32(reader.GetOrdinal("num"));
			history.pmax = reader.GetFloat(reader.GetOrdinal("pmax"));
			history.p1 = reader.GetFloat(reader.GetOrdinal("p1"));
			history.e = reader.GetFloat(reader.GetOrdinal("e"));
			history.k21 = reader.GetFloat(reader.GetOrdinal("k21"));
			history.k01 = reader.GetFloat(reader.GetOrdinal("k01"));
			history.type1 = reader.GetInt32(reader.GetOrdinal("type1"));
			history.k22 = reader.GetFloat(reader.GetOrdinal("k22"));
			history.k02 = reader.GetFloat(reader.GetOrdinal("k02"));
			history.type2 = reader.GetInt32(reader.GetOrdinal("type2"));
			history.k23 = reader.GetFloat(reader.GetOrdinal("k23"));
			history.k03 = reader.GetFloat(reader.GetOrdinal("k03"));
			history.type3 = reader.GetInt32(reader.GetOrdinal("type3"));
			history.k24 = reader.GetFloat(reader.GetOrdinal("k24"));
			history.k04 = reader.GetFloat(reader.GetOrdinal("k04"));
			history.type4 = reader.GetInt32(reader.GetOrdinal("type4"));

			history.p4 = reader.GetFloat(reader.GetOrdinal("p4"));
			history.p5 = reader.GetFloat(reader.GetOrdinal("p5"));
			history.k1 = reader.GetFloat(reader.GetOrdinal("k1"));
			history.k31 = reader.GetFloat(reader.GetOrdinal("k31"));
			history.pn1 = reader.GetFloat(reader.GetOrdinal("pn1"));
			history.k32 = reader.GetFloat(reader.GetOrdinal("k32"));
			history.pn2 = reader.GetFloat(reader.GetOrdinal("pn2"));
			history.k33 = reader.GetFloat(reader.GetOrdinal("k33"));
			history.pn3 = reader.GetFloat(reader.GetOrdinal("pn3"));
			history.k34 = reader.GetFloat(reader.GetOrdinal("k34"));
			history.pn4 = reader.GetFloat(reader.GetOrdinal("pn4"));

			history.bRun = reader.GetBoolean(reader.GetOrdinal("bRun"));
			history.bStartBack = reader.GetBoolean(reader.GetOrdinal("bStartBack"));
			history.bWaterWork = reader.GetBoolean(reader.GetOrdinal("bWaterWork"));
			history.bStop = reader.GetBoolean(reader.GetOrdinal("bStop"));

			history.time = reader.GetInt64(reader.GetOrdinal("time"));

			histories [i] = history;
			i++;
		}

		return (History[])Algorithm.Redim (histories, i);
	}

	public void DeleteAllHistory()
	{
		sqlite.DeleteValuesOR ("history", new string[]{ "id" }, new string[]{ ">" }, new string[]{"'-1'"});
	}


}
