using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UAJ
{

    public class JSONSerializer : ISerializer
    {
        public string Serialize(TrackerEvent e)
        {
            return e.toJSON();
        }
    }
}
