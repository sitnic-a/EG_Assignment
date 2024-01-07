using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Assets.Models;
using ExordiumGames.MVC.Data.DbModels;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class ItemScript : MonoBehaviour
{
    public List<Item> dbItems = new List<Item>();

    public void GetItems()
    {
        StartCoroutine(CallGetItems("https://localhost:7029/api/unity/items"));
        SceneManager.LoadScene("Items");
    }

    public IEnumerator CallGetItems(string url)
    {
        //"https://localhost:7029/api/unity/items"
        UnityWebRequest request = UnityWebRequest.Get(url);
        {
            request.method = "GET";
            request.certificateHandler = new UnityCertificateHandler();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                string data = request.downloadHandler.text;

                dbItems = JsonConvert.DeserializeObject<List<Item>>(data, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            }
        }
    }

    
}
