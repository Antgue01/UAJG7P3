using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UAJ
{

    public class JSONSerializer : ISerializer
    {
        public string getFormatName()
        {
            return "JSON";
        }

        public string Serialize(TrackerEvent e)
        {
            return e.toJSON();
        }
    }
}
