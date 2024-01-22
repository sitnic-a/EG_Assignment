using ExordiumGames.MVC.Data.DbModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayItems : MonoBehaviour
{
    public GameObject itemTemplate;

    private void Start()
    {
        ShowItems();
    }

    public void ShowItems()
    {
        for (int i = 0; i < 17; i++)
        {
            Instantiate(itemTemplate, transform.position, transform.rotation, transform);
        }
    }
}
