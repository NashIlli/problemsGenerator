using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class FormatableTextController : MonoBehaviour {

    private static FormatableTextController formatableTextController;

    void Awake()
    {
        if (formatableTextController == null) formatableTextController = this;
        else if (this != formatableTextController) Destroy(this);
    }

    [SerializeField]
    private List<Toggle> formatToggles;



    internal FormatType GetCurrentFormat()
    {
        for (int i = 0; i < formatToggles.Count; i++)
        {
            if (formatToggles[i].isOn)
            {
                return (FormatType)i;

            }
        }
        return FormatType.NoOne;
    }

    internal static Color GetSelectedColor()
    {
        return Color.red;
    }

    public static FormatableTextController GetController()
    {
        return formatableTextController;
    }


}
