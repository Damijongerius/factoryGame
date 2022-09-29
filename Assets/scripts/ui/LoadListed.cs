using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadListed : MonoBehaviour
{
    public GameObject Profile;
    public Vector3 nextPos = new Vector3(0, -100, 0);
    public Button button;
    public Button escape;

    private List<GameObject> profiles = new List<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        Master();
    }

    void Start()
    {
        escape.onClick.AddListener(call: ClearList);
    }


    private void Master()
    {
        JsonSaveLoad load = new();
        if (load.ReadListedProfiles() != null)
        foreach (string name in load.ReadListedProfiles().profiles)
        {
            profiles.Add(Instantiate(Profile));

            SetName(profiles[profiles.Count - 1], name);
            SetPos(profiles[profiles.Count - 1]);
            profiles[profiles.Count - 1].transform.Find("TimePlayed").GetComponent<TextMeshProUGUI>().text = "ooit";
            profiles[profiles.Count - 1].transform.Find("LastPlayed").GetComponent<TextMeshProUGUI>().text = "niet nu"; 
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

        void SetTime()
        {

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



    // Update is called once per frame
}
