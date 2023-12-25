using ExordiumGames.MVC.Data.DbModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class UserAccount : MonoBehaviour
{
    public List<Item> dbItems = new List<Item>();

    public void GetItems()
    {
        StartCoroutine(CallGetItems("https://localhost:7029/api/unity/items"));
    }

    public IEnumerator CallGetItems(string url)
    {
        //"https://localhost:7029/api/unity/items"
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string data = www.downloadHandler.text;

            dbItems = JsonConvert.DeserializeObject<List<Item>>(data, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });
            yield return new WaitUntil(() => dbItems.Count < 0);
        }
    }
}
