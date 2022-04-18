using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.IO;
using UnityEngine;


namespace UAJ
{
    public abstract class TrackerEvent
    {
        public string _sessionId { get; private set; }
        public string _timestamp { get; private set; }
        public string _eventName { get; private set; }

        public TrackerEvent(string name)
        {
            _timestamp = Tracker.GetCurrentTelemetryTime();
            _sessionId = Tracker.GetSessionId();
            _eventName = name;
        }

        public string toJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string toBSON()
        {
            string res;
            using (MemoryStream ms = new MemoryStream())
            using (Newtonsoft.Json.Bson.BsonWriter datawriter = new Newtonsoft.Json.Bson.BsonWriter(ms))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(datawriter, this);
                res = Convert.ToBase64String(ms.ToArray());
            }

            return res;
        }
    }

    //Factory.instance().createDeath()
    class StartRoundEvent : TrackerEvent
    {
        public string _level;
        public StartRoundEvent(string level) : base("StartRoundEvent")
        {
            _level = level;
        }
    }

    class EndRoundEvent : TrackerEvent
    {
        public string _level;
        public EndRoundEvent(string level) : base("EndRoundEvent")
        {
            _level = level;
        }
    }

    class StartGameEvent : TrackerEvent
    {
        public StartGameEvent() : base("StartGameEvent")
        {
        }
    }

    class EndGameEvent : TrackerEvent
    {
        public EndGameEvent() : base("EndGameEvent")
        {
        }
    }

    class DeathEvent : TrackerEvent
    {
        public float _x;
        public float _y;
        public float _z;
        public string _id;

        public DeathEvent(Vector3 position, string id) : base("DeathEvent")
        {
            _x = position.x;
            _y = position.y;
            _z = position.z;
            _id = id;
        }
    }

    class UseObjectEvent : TrackerEvent
    {
        public string _objectID { get; set; }

        public UseObjectEvent(string id) : base("UseObjectEvent")
        {
            _objectID = id;
        }
    }

    class PlayerPositionEvent : TrackerEvent
    {
        public float _x { get; set; }
        public float _y { get; set; }
        public float _z { get; set; }

        public PlayerPositionEvent(Vector3 position) : base("PlayerPositionEvent")
        {
            _x = position.x;
            _y = position.y;
            _z = position.z;
        }
    }
}