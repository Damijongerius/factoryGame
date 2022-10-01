using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadListed : MonoBehaviour
{
    public GameObject Profile;
    public Vector3 nextPos = new Vector3(0, -50, 0);
    public Button button;
    public Button escape;

    private static LoadListed instance;
    private List<GameObject> profiles = new List<GameObject>();

    private Scrollbar sb;
    private float speed = 100;
    private float lastP = 0;
    private RectTransform trans;
    // Start is called before the first frame update
    private void Awake()
    {
        sb = gameObject.GetComponentInParent<Scrollbar>();
        Event e = new Event();

        trans = gameObject.GetComponent<RectTransform>();

        sb.onValueChanged.AddListener(Dragg);
        
           

        instance = this;
        Master();
    }

    public void UpdateList()
    {
        ClearList();

        Master();

    }


    private void Master()
    {
        JsonSaveLoad load = new();
        if (load.ReadListedProfiles().profiles != null)
        {
            foreach (string name in load.ReadListedProfiles().profiles)
            {
                Debug.Log(name);
                profiles.Add(Instantiate(Profile));

                SetName(profiles[profiles.Count - 1], name);
                SetPos(profiles[profiles.Count - 1]);
                profiles[profiles.Count - 1].transform.Find("TimePlayed").GetComponent<TextMeshProUGUI>().text = "ooit";
                profiles[profiles.Count - 1].transform.Find("LastPlayed").GetComponent<TextMeshProUGUI>().text = "niet nu";
                speed = nextPos.y - 500;
            }
        }


        void SetName(GameObject profile, string name)
        {
            profile.transform.Find("ProfileName").GetComponent<TextMeshProUGUI>().text = name;
            //profile.transform.parent = this.gameObject.transform;
            profile.transform.SetParent(this.gameObject.transform, false);
        }

        void SetPos(GameObject profile)
        {
            RectTransform RTransform = profile.GetComponent<RectTransform>();
            RTransform.position = new Vector3(553, 447, 0);
            RTransform.position -= nextPos;



            nextPos -= new Vector3(0, -100, 0);
        }
    }

    private void ClearList()
    {
        foreach(GameObject profile in profiles)
        {
            Destroy(profile);
        }
        nextPos = new Vector3(0, 0, 0);
    }

    private void Dragg(float pos)
    {
        if(nextPos.y <= 500)
        {
            sb.value = 1;
        }
        else
        {
            trans.anchoredPosition = new Vector2(trans.anchoredPosition.x + speed * (lastP - pos), trans.anchoredPosition.y);
            lastP = pos;

        }
 
    }


    public static LoadListed GetInstance()
    {
        return instance;
    }
}
