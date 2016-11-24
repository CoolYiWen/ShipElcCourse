using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JsonFx.Json;

public class ReadFileTool
{

    /// <summary>  
    /// 根据一个JSON，得到一个类  
    /// </summary>  
    static public T JsonToClass<T>(string json) where T : class  
    {  
        T t = JsonReader.Deserialize<T>(json);  
        return t;  
    }  

    /// <summary>  
    /// 根据一个JSON的文件地址，得到一个类  
    /// </summary>  
    static public T AddressToClass<T>(string txtAddress) where T : class  
    {  
        TextAsset jsonData = Resources.Load(txtAddress) as TextAsset;  
        return JsonToClass<T>(jsonData.text);  
    }  

    /// <summary>  
    /// 将JSON转换为一个类数组  
    /// </summary>  
    static public T[] JsonToClasses<T>(string json) where T : class  
    {  
        //Debug.Log(json);  
        T[] list = JsonReader.Deserialize<T[]>(json);  
        return list;  
    }  

    /// <summary>  
    /// 给Json文件的地址。转换为一个类数组  
    /// </summary>  
    static public T[] AddressToClasses<T>(string txtAddress) where T : class  
    {  
        TextAsset jsonData = Resources.Load(txtAddress) as TextAsset;  
        return JsonToClasses<T>(jsonData.text);  
    }  
}

