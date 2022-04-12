using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UAJ;
using UnityEngine;

namespace UAJ
{
    public class Tracker
    {
        [Serializable]
        public class TrackerConfig
        {
            [Serializable]
            public class PersistanceConfig
            {
                public string type;
                public string param;
                public string serializer;
            }

            [Serializable]
            public class ITrackerConfig
            {
                public string type;
                public List<string> enabled;
            }
            
            public string timeType;

            public List<PersistanceConfig> persistence;

            public List<ITrackerConfig> activeTrackers;
        }
        
        private static Tracker instance;

        private List<ITrackerAsset> _activeTrackers = new List<ITrackerAsset>();

        private List<IPersistance> _persistance = new List<IPersistance>();

        private static string timeType;

        private static float startTime;


        public static void Init()
        {
            string configPath = Application.persistentDataPath + "/configTracker.json";

            instance = new Tracker();
            if (!instance.InitTracker(configPath))
            {
                Debug.LogWarning("Couldn't open config file at " + configPath);
                instance.DefaultInit();
            }

            startTime = Time.time;
            instance.TrackEvent(new StartGameEvent());
        }


        private bool InitTracker(string path)
        {
            if (!File.Exists(path))
                return false;

            string json = File.ReadAllText(path);
            
            TrackerConfig config = JsonUtility.FromJson<TrackerConfig>(json);

            timeType = config.timeType;
            
            Debug.Log(timeType);

            foreach (TrackerConfig.PersistanceConfig pC in config.persistence)
            {
                IPersistance persistance;
                ISerializer serializer;
                
                Debug.Log(pC.type + " " + pC.serializer);
                
                switch (pC.serializer)
                {
                    case "Json":
                        serializer = new JSONSerializer();
                        break;
                    case "Bson":
                        serializer = new BSONSerializer();
                        break;
                    default:
                        Debug.LogWarning("Wrong serializer type " + pC.serializer);
                        serializer = new JSONSerializer(); 
                        break;
                }
                
                switch (pC.type)
                {
                    case "Server":
                        persistance = new ServerPersistance(pC.param, serializer);
                        break;
                    case "Disk":
                        persistance = new DiskPersistance(pC.param, serializer);
                        break;
                    default:
                        Debug.LogWarning("Wrong Persistence type " + pC.type);
                        persistance = new DiskPersistance("default", serializer);
                        break;
                }
                
                _persistance.Add(persistance);
            }

            foreach (TrackerConfig.ITrackerConfig tC in config.activeTrackers)
            {
                ITrackerAsset tracker;
                Debug.Log(tC.type);
                switch (tC.type)
                {
                    case "Progrression":
                        tracker = new ProgressionTracker();
                        break;
                    default:
                        tracker = new ProgressionTracker();
                        break;
                }

                tracker.init(tC.enabled.ToArray());
                
                _activeTrackers.Add(tracker);
            }

            return true;
        }

        private void DefaultInit()
        {
            timeType = "POSIX";
            _persistance.Add(new ServerPersistance("http://192.168.1.44/post", new JSONSerializer()));
            _persistance.Add(new DiskPersistance(Application.persistentDataPath, new BSONSerializer()));
            ProgressionTracker pTr = new ProgressionTracker();
            pTr.init(new []{""});
            _activeTrackers.Add(pTr);
        }

        public static Tracker Instance()
        {
            if (instance == null)
            {
                Init();
            }

            return instance;
        }

        public static void End()
        {
            instance.TrackEvent(new EndGameEvent());
            foreach (IPersistance p in instance._persistance)
            {
                p.Flush();
            }
        }


        public bool TrackEvent(TrackerEvent e)
        {
            foreach (ITrackerAsset t in _activeTrackers)
            {
                if (t.accept(e))
                {
                    foreach (IPersistance p in _persistance)
                    {
                        p.Send(e);
                    }

                    return true;
                }
            }

            return false;
        }

        public static string GetCurrentTelemetryTime()
        {
            string ret = "";
            switch (timeType)
            {
                case "POSIX":
                    ret = "POSIX: "+ DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
                    break;
                case "UNITY":
                    ret = "UNITY" + (Time.time - startTime).ToString() + " " + DateTime.Now.ToString();
                    break;
            }

            return ret;
        }
    }
}