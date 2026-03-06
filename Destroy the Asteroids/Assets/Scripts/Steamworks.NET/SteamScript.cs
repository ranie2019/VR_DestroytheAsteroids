using UnityEngine;
using Steamworks;

public class SteamScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(SteamManager.Initialized) {
			string name = SteamFriends.GetPersonaName();
			Debug.Log(name);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
