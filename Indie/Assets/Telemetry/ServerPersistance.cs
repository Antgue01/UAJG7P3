using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
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
            //Thread t = new Thread(auxFlush);
            //t.Start();
            auxFlush();
        }
        void auxFlush()
        {

            var api = _server;

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
            
            var url = api;

            var request = WebRequest.Create(url);
            request.Method = "POST";

            request.ContentType = "application/x-www-form-urlencoded";
            using (var stream = request.GetRequestStream())
            {
                stream.Write(Encoding.UTF8.GetBytes(message), 0, message.Length);
            }

            var webResponse = request.GetResponse();
            
            
            var webStream = webResponse.GetResponseStream();

            var reader = new StreamReader(webStream);
            var data = reader.ReadToEnd();

            Debug.Log(data);

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
