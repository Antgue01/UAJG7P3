using Models;
using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace UAJ
{

    public class ServerPersistance : IPersistance
    {
        public ServerPersistance(string server, ISerializer serializer)
        {

            _server = server;
            _serializer = serializer;
            _events = new Queue<TrackerEvent>();
        }
        public void Flush()
        {
            Thread t = new Thread(auxFlush);
            t.Start();
        }
        void auxFlush()
        {

            var api = "http://uaj.fdi.ucm.es/c2122/telemetry/grupo07";

            string message = "{[";
            lock (_events)
            {
                while (_events.Count > 0)
                {
                    TrackerEvent e = _events.Dequeue();
                    message += _serializer.Serialize(e) + ",";


                }
            }
            message = message.Remove(message.Length - 1);
            message += "]}";
            RestClient.PostArray<Post>(api + "/posts",message).Then(response => { Debug.Log(response); });



        }





        public void Send(TrackerEvent trackerEvent)
        {
            lock (_events)
            {
                _events.Enqueue(trackerEvent);
            }
        }
        Queue<TrackerEvent> _events;
        string _server;
        ISerializer _serializer;
        const string post = "POST http://uaj.fdi.ucm.es/c2122/telemetry/grupo07";

    }
}
