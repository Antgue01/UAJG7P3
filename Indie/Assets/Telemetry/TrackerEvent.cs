using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.IO;


namespace UAJ
{
    public abstract class TrackerEvent{
        public ulong _timestamp { get; set; }
        public string _eventName { get; set; }

        public TrackerEvent(ulong timestamp, string name)
        {
            _timestamp = timestamp;
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
                res =  Convert.ToBase64String(ms.ToArray());
            }
            return res;
        }
    }
    //Factory.instance().createDeath()
    class StartRoundEvent :TrackerEvent{
        public StartRoundEvent(ulong timestamp) : base(timestamp, "StartRoundEvent"){}
    }

    class EndRoundEvent :TrackerEvent{
        public EndRoundEvent(ulong timestamp) : base(timestamp, "EndRoundEvent"){}
    }

    class StartGameEvent :TrackerEvent{
        public StartGameEvent(ulong timestamp) : base(timestamp, "StartGameEvent"){}

    }

    class EndGameEvent :TrackerEvent{
        public EndGameEvent(ulong timestamp) : base(timestamp, "EndGameEvent"){}

    }

    class DeathEvent : TrackerEvent
    {
        public int _enemyID { get; set; }
        public DeathEvent(ulong timestamp) : base(timestamp, "DeathEvent")
        {
        }
        public DeathEvent withID(int id){
            _enemyID=id;
            return this;
        }

        
    }
    class UseObjectEvent : TrackerEvent
    {
        public int _objectID { get; set; }
        public UseObjectEvent(ulong timestamp) : base(timestamp, "UseObjectEvent")
        {
        }
        public UseObjectEvent withID(int id){
            _objectID = id;
            return this;
        }

    }

    class PlayerPositionEvent : TrackerEvent
    {

        public float _x { get; set; }
        public float _y { get; set; }
        public float _z { get; set; }

        public PlayerPositionEvent(ulong timestamp) : base(timestamp, "PlayerPositionEvent"){}

        PlayerPositionEvent withPosition(float x,  float y,float z){
            _x=x;
            _y=y;
            _z=z;
            return this;
        }

    }

}