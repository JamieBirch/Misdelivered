using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int DesiredDeliveries;
    public int showDesiredDeliveries;
    public static int SecretDeliveries;
    public  int showSecretDeliveries;
    
    // Start is called before the first frame update
    void Start()
    {
        DesiredDeliveries = 0;
        SecretDeliveries = 0;
    }

    private void Update()
    {
        showDesiredDeliveries = DesiredDeliveries;
        showSecretDeliveries = SecretDeliveries;
    }
}
