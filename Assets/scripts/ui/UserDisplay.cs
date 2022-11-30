using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserDisplay : MonoBehaviour
{
    public TextMeshProUGUI username;
    public TextMeshProUGUI guid;

    User user = User.GetInstance();
    private void Awake()
    {
        if (user.guid != null)
        {
            guid.text = user.guid.ToString();
         
        }
        if(user.UserName!= null)
        {
            username.text = user.UserName.ToString();
        }
    }
}
