using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int DesiredDeliveries;
    public int showDesiredDeliveries;
    public static int SecretDeliveries;
    public  int showSecretDeliveries;
    public static int TotalDeliveries;
    public  int showTotalDeliveries;
    
    // Start is called before the first frame update
    void Start()
    {
        DesiredDeliveries = 0;
        SecretDeliveries = 0;
        TotalDeliveries = 0;
    }

    private void Update()
    {
        showDesiredDeliveries = DesiredDeliveries;
        showSecretDeliveries = SecretDeliveries;
        showTotalDeliveries = TotalDeliveries;
    }
}
