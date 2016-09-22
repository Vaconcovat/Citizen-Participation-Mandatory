using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class WinInterfaceManager : MonoBehaviour {

    public AutoType at;
    NoiseAndScratches ns;
    bool fin = false;

    // Use this for initialization
    void Start()
    {
        ns = FindObjectOfType<NoiseAndScratches>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fin)
        {
            ns.grainIntensityMin += Time.deltaTime;
            ns.grainIntensityMax += Time.deltaTime;
        }
    }

    public void Win()
    {
        at.StartType();
    }

    public void Finished()
    {
        fin = true;
    }
}
