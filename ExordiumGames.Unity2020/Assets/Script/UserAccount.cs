using ExordiumGames.MVC.Data.DbModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;
using UnityEditor;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using Assets.Models;
using System.Security.Cryptography;
using System;

public class UserAccount : MonoBehaviour
{
    public List<Item> dbItems = new List<Item>();
    public UnityCertificateHandler certificateHandler;

    private string pubkey { get; set; }
    private string thumbprint = "2f90a27695e2eb30c8e104b59b0b8ca0ba85ec10";
    private void Start()
    {
        X509Store st0re = new X509Store();
        st0re.Open(OpenFlags.ReadOnly);

        int certsCount = st0re.Certificates.Count;
        st0re.Close();

        // Generate PublicKey for authentication
        TextAsset tx = Resources.Load<TextAsset>("Certificates/eShopCert");
        byte[] by = Encoding.UTF8.GetBytes(tx.ToString());

        X509Certificate2 certificate = new X509Certificate2(by);
        pubkey = certificate.GetPublicKeyString();
    }
    public void GetItems()
    {
        StartCoroutine(CallGetItems("https://localhost:7029/api/unity/items"));
    }

    public IEnumerator CallGetItems(string url)
    {
        //"https://localhost:7029/api/unity/items"
        UnityWebRequest request = UnityWebRequest.Get(url);
        {
            request.method = "GET";
            request.certificateHandler = new UnityCertificateHandler { PUB_KEY = pubkey };

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
                yield return new WaitUntil(() => dbItems.Count < 0);
            }
        }
    }
}
