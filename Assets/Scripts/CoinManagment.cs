using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManagment : MonoBehaviour
{
    public int gold;
    public Text money;
    // Start is called before the first frame update
    void Start()
    {
        gold = PlayerPrefs.GetInt("Money" , 0);
    }

    // Update is called once per frame
    void Update()
    {
        money.text = PlayerPrefs.GetInt("Money" , 0).ToString();
    }

    public void Addmoney() {
        gold++;
        PlayerPrefs.SetInt("Money" , gold);
    }
}
