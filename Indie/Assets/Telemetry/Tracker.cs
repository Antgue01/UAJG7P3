﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UAJ;
using UnityEngine;

namespace UAJ
{
    public static class Tracker
    {
        [Serializable]
        public class TrackerConfig
        {
            [Serializable]
            public class PersistanceConfig
            {
                public string type;
                public List<string> paramList; 
                public string serializer;
            }

            [Serializable]
            public class ITrackerConfig
            {
                public string type;
                public List<string> enabled;
            }
            
            public string timeType;

            public float flushTime;

            public List<PersistanceConfig> persistence;

            public List<ITrackerConfig> activeTrackers;
        }
        
        private static List<ITrackerAsset> _activeTrackers = new List<ITrackerAsset>();

        private static List<IPersistance> _persistance = new List<IPersistance>();

        private static string timeType;

        private static float startTime;

        private static float flushTime;

        private static bool running = false;

        public static void Init()
        {
            string configPath = Application.persistentDataPath + "/configTracker.json";
            
            if (!InitTracker(configPath))
            {
                Debug.LogWarning("Couldn't open config file at " + configPath);
                DefaultInit();
            }

            startTime = Time.time;
            running = true;
            TrackEvent(new StartGameEvent());
             
        }


        private static bool InitTracker(string path)
        {
            if (!File.Exists(path))
                return false;

            string json = File.ReadAllText(path);
            
            TrackerConfig config = JsonUtility.FromJson<TrackerConfig>(json);

            timeType = config.timeType;

            flushTime = config.flushTime;
            
            Debug.Log(timeType);
            
            Debug.Log(flushTime.ToString());

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
                        persistance = new ServerPersistance(pC.paramList[0], pC.paramList[1], serializer);
                        break;
                    case "Disk":
                        persistance = new DiskPersistance(pC.paramList[0], serializer);
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

        public static IEnumerator FlushEvents()
        {
            while (running)
            {
                yield return new WaitForSeconds(flushTime);
                Debug.Log("Flush");
                foreach (var p in _persistance)
                {
                    p.Flush();
                }
            }
        }

        private static void DefaultInit()
        {
            timeType = "POSIX";
            flushTime = 2f;
            _persistance.Add(new ServerPersistance("http://192.168.1.44/post", new JSONSerializer()));
            _persistance.Add(new DiskPersistance(Application.persistentDataPath, new BSONSerializer()));
            ProgressionTracker pTr = new ProgressionTracker();
            pTr.init(new []{""});
            _activeTrackers.Add(pTr);
        }

        public static void End()
        {
            Debug.Log("Tracker End");
            running = false;
            TrackEvent(new EndGameEvent());
            foreach (IPersistance p in _persistance)
            {
                p.Flush();
            }
        }


        public static bool TrackEvent(TrackerEvent e)
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
                    ret = "POSIX: "+ DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + " " + DateTime.Now.ToString();
                    break;
                case "UNITY":
                    ret = "UNITY " + (Time.time - startTime).ToString() + " " + DateTime.Now.ToString();
                    break;
            }

            return ret;
        }
    }
}