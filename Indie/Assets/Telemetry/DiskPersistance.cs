using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

namespace UAJ
{

    public class DiskPersistance : IPersistance
    {
        public DiskPersistance(string path,ISerializer serializer)
        {
            if (path == "default")
                path = Application.persistentDataPath;
            _path = path;
            _serializer = serializer;
            _events = new Queue<TrackerEvent>();
        }
        public void Flush()
        {
            Thread t = new Thread(auxFlush);
            t.Start();


        }

        public void Send(TrackerEvent trackerEvent)
        {
            lock (_events)
            {
                _events.Enqueue(trackerEvent);
            }

        }
        private void auxFlush()
        {
            TrackerEvent e;
            string eventData = "";
            lock (_events)
            {
                while (_events.Count > 0)
                {
                    e = _events.Dequeue();
                    eventData = _serializer.Serialize(e);

                    try
                    {
                        //el true es del append
                        StreamWriter writer = new StreamWriter(_path,true);
                        writer.WriteLine(eventData);
                        writer.Close();
                    }
                    catch (Exception except)
                    {
                        Debug.LogError(except.Message);
                    }
                }
            }
        }

        Queue<TrackerEvent> _events;
        ISerializer _serializer;
        string _path;
    }
}
