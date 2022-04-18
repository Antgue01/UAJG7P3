using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UAJ
{
    public interface IPersistance
    {
        void Send(TrackerEvent trackerEvent);
        void Flush();
    }
}
