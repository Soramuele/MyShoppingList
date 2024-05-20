using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class GPS : MonoBehaviour
{
    [SerializeField] private TMP_Text info;
    
    //North to South
    public double latitude;
    //West to East
    public double longitude;

    [SerializeField] bool isActive = false;

    // Update is called once per frame
    void Update()
    {
        if(!isActive) {
            StartCoroutine(GetLocation());
            isActive = true;
        }
    }

    IEnumerator GetLocation()
    {
        //Ask user for permissions before using the app
        if(!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }

        //Check if user has location service enabled
        if(!Input.location.isEnabledByUser)
            yield return new WaitForSeconds(4);
        
        //Start service before querying location
        Input.location.Start();

        //Wait until service initialize
        int maxWait = 3;
        while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        //Check if service initialize and connection is stable
        if(maxWait < 1) {
            Debug.LogError("Timed out");
            info.text = "Timed out";
            yield break;
        }
        
        if(Input.location.status == LocationServiceStatus.Failed) {
            Debug.LogError("Unable to determinate device location");
            info.text = "Unable to determinate device location";
            yield break;
        } else {
            longitude = Input.location.lastData.longitude;
            latitude = Input.location.lastData.latitude;
            
            info.text = latitude + "\n\n" + longitude;
        }

        isActive = false;
    }
}
