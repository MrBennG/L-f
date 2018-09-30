using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public float stone;
    public float maxStone;
    public float wood;
    public float maxWood;
    public float wheat;
    public float maxWheat;

    public Text stoneDisp;
    public Text woodDisp;
    public Text wheatDisp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stoneDisp.text = "" + stone + "/" + maxStone;
        woodDisp.text = "" + wood + "/" + maxWood;
        wheatDisp.text = "" + wheat + "/" + maxWheat;
    }
}
