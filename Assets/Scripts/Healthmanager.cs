using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthmanager : MonoBehaviour
{
    public float Health;
    public Slider slider;
    public TMP_Text text;
    void Start()
    {
        text.text = name +" : "+ Health;
    }
    public void takedamamge(int damageamount)
    {
        Health -= damageamount;
        slider.value = Health;
        text.text = name + " : " + Health;
    }
    void Update()
    {
        if(Health == 0)
        {
            Application.OpenURL("https://mehran13579.itch.io/boxing-unity-25d-2nd");
        }
    }
}
