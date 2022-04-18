using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UAJ
{

    public interface ISerializer
    {
        string Serialize(TrackerEvent e);
        string getFormatName();
    }

}