using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.IO;


namespace UAJ
{
    public abstract class TrackerEvent
    {
        public string _timestamp { get; private set; }
        public string _eventName { get; private set; }

        public TrackerEvent(string name)
        {
            _timestamp = Tracker.GetCurrentTelemetryTime();
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
        public StartRoundEvent() : base("StartRoundEvent")
        {
        }
    }

    class EndRoundEvent : TrackerEvent
    {
        public EndRoundEvent() : base("EndRoundEvent")
        {
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
        public int _enemyID { get; set; }

        public DeathEvent() : base("DeathEvent")
        {
        }

        public DeathEvent withID(int id)
        {
            _enemyID = id;
            return this;
        }
    }

    class UseObjectEvent : TrackerEvent
    {
        public int _objectID { get; set; }

        public UseObjectEvent() : base("UseObjectEvent")
        {
        }

        public UseObjectEvent withID(int id)
        {
            _objectID = id;
            return this;
        }
    }

    class PlayerPositionEvent : TrackerEvent
    {
        public float _x { get; set; }
        public float _y { get; set; }
        public float _z { get; set; }

        public PlayerPositionEvent() : base("PlayerPositionEvent")
        {
        }

        PlayerPositionEvent withPosition(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
            return this;
        }
    }
}