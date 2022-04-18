using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
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
            _key = "";
        }
        public ServerPersistance(string server, string key, ISerializer serializer)
        {
            _serializer = serializer;
            _server = server;
            _key = key;
            _events = new Queue<TrackerEvent>();
        }
        public void Flush()
        {
            Thread t = new Thread(auxFlush);
            t.Start();
        }
        
        void auxFlush()
        {
            int i = 0;
            string message = "[{";
            lock (_events)
            {
                //como el server espera un array en formato JSON se pasa lo que sea que venga (cualquier formato) a JSON con el formato
                //[
                //  {0:"lo que sea en el formato que sea"}
                //  {1:"lo que sea en el formato que sea"}
                //]
                while (_events.Count > 0)
                {
                    TrackerEvent e = _events.Dequeue();
                    string serializedEvent = _serializer.Serialize(e);

                    message += "\"" + i.ToString() + "\":\"" + serializedEvent.Replace("\"", "\\\"") + "\",";
                    ++i;
                }
            }
            message = message.Remove(message.Length - 1);
            message += "}]";


            WebRequest request = WebRequest.Create(_server);
            request.Method = "POST";

            if (_key != null && _key != "")
                request.Headers.Add("X-API-Key", _key);
            request.ContentType = "application/json";
            Stream stream = request.GetRequestStream();
            stream.Write(Encoding.UTF8.GetBytes(message), 0, message.Length);
            //mandamos el mensaje
            request.GetResponse();


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
        string _key;
        ISerializer _serializer;
        JSONSerializer _senderSerializer;

    }
}
