using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class AccessPrysme : MonoBehaviour {

    public Button btn_Prysme;
    public Button btn_Caste;

    public GameObject bck_Caste;
    public GameObject bck_Prysme;

    // Use this for initialization
    void Start () {

        Button btnCaste= btn_Caste.GetComponent<Button>();
        Button btnPrysme = btn_Prysme.GetComponent<Button>();

        btnCaste.onClick.AddListener(AfficheCaste);
        btnPrysme.onClick.AddListener(AffichePrysme);

    }

    void AfficheCaste()
    {
        bck_Caste.gameObject.SetActive(true);
        bck_Prysme.gameObject.SetActive(false);
 
    }

    void AffichePrysme()
    {
        bck_Caste.gameObject.SetActive(false);
        bck_Prysme.gameObject.SetActive(true);

    }
    // Update is called once per frame
    void Update () {
		
	}
}
