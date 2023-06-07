using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldObjects
{
    [CreateAssetMenu(fileName = "placableItems", order = 51)]
    public class WorldObjects : ScriptableObject
    {

        public GameObject CoalPlant;
        public GameObject OilPlant;
        public GameObject PotatoPlant;
        public GameObject SolarPanel;
        public GameObject WindMill;
        public GameObject StoryFactory1;
        //public GameObject StoryFactory2;
       // public GameObject StoryFactory3;

        public GameObject DataCable;
        public GameObject PowerCable;
        public GameObject PowerProcessor;
        public GameObject PowerStorageModule;

        public GameObject LeadAcidBattery;
        public GameObject LithiumIonBattery;
        public GameObject SodiumSulphurBattery;
        public GameObject WaterStorage;

        public GameObject WaterFilterCenter;
        public GameObject WaterPipeExtractor;
        public GameObject WaterTower;

        public GameObject PowerAmplifier;
        public GameObject PowerDemper;
        public GameObject PowerLine;
        public GameObject PowerMerger;
        public GameObject PowerSplitter;

        public GameObject DataCenter;
        public GameObject DataStockCenter;
        public GameObject SatelliteDish;

        public List<GameObject> AllObjects { get => AllObjs(); }

        private List<GameObject> AllObjs()
        {
            return new List<GameObject>()
            {
                CoalPlant,
                OilPlant,
                PotatoPlant,
                SolarPanel,
                WindMill,
                StoryFactory1,
                //StoryFactory2,
                //StoryFactory3,
                DataCable,
                PowerCable,
                PowerProcessor,
                PowerStorageModule,
                LeadAcidBattery,
                LithiumIonBattery,
                SodiumSulphurBattery,
                WaterStorage, 
                WaterFilterCenter,
                WaterPipeExtractor,
                WaterTower,
                PowerAmplifier,
                PowerDemper,
                PowerLine,
                PowerMerger,
                PowerSplitter,
                DataCenter,
                DataStockCenter,
                SatelliteDish
            };
        }

        public Dictionary<string, GameObject> GetPowerSources()
        {
            Dictionary<string, GameObject> keyValuePairs = new Dictionary<string, GameObject>
                {
                    { "CoalPlant", CoalPlant },
                    { "OilPlant", OilPlant },
                    { "PotatoPlant", PotatoPlant },
                    { "SolarPanel", SolarPanel },
                    { "WindMill", WindMill }
                };
            return keyValuePairs;
        }

        public Dictionary<string, GameObject> GetStorages()
        {
            Dictionary<string, GameObject> keyValuePairs = new Dictionary<string, GameObject>
                {
                    { "LeadAcidBattery", LeadAcidBattery },
                    { "LithiumIonBattery", LithiumIonBattery },
                    { "SodiumSulphurBattery", SodiumSulphurBattery },
                    { "WaterStorage", WaterStorage }
                };

            return keyValuePairs;
        }

        public Dictionary<string, GameObject> GetWater()
        {
            return new Dictionary<string, GameObject>
                {
                    { "WaterFilterCenter", WaterFilterCenter },
                    { "WaterPipeExtractor", WaterPipeExtractor },
                    { "WaterTower", WaterTower }
                };
        }

        public Dictionary<string, GameObject> GetElectrical()
        {
            return new Dictionary<string, GameObject>
                {
                    { "PowerAmplifier", PowerAmplifier },
                    { "PowerDemper", PowerDemper },
                    { "PowerLine", PowerLine },
                    { "PowerMerger", PowerMerger },
                    { "PowerSplitter", PowerSplitter }
                };

        }

        public Dictionary<string, GameObject> GetData()
        {
            return new Dictionary<string, GameObject>
                {
                    { "DataCenter", DataCenter },
                    { "DataStockCenter", DataStockCenter },
                    { "SatelliteDish", SatelliteDish }
                };

        }

        public Dictionary<string, GameObject> GetFactory()
        {
            return new Dictionary<string, GameObject>
                {
                    { "SmallFactory", StoryFactory1 },
                    //{ "MediumFactory", StoryFactory2 },
                    //{ "LargeFactory", StoryFactory3 },
                    { "DataCable", DataCable },
                    { "PowerCable", PowerCable },
                    { "PowerProcessor", PowerProcessor },
                    { "PowerStorageModule", PowerStorageModule }
                };
        }
    }

    public enum Order
    {
        CoalPlant,//
        OilPlant,//
        PotatoPlant,//
        SolarPanel,//
        WindMill,//
        StoryFactory1,//
        DataCable, //old model
        PowerCable, //rebase old model
        PowerProcessor, //old generator
        PowerStorageModule, //
        LeadAcidBattery, // 
        LithiumIonBattery, // 
        SodiumSulphurBattery, //
        WaterStorage, //
        WaterFilterCenter, //
        WaterPipeExtractor, // (water extractor)
        WaterTower, //
        WaterPipe,
        PowerAmplifier, //
        PowerDemper, // (PowerLineresistor)
        PowerLine,//
        PowerMerger, //
        PowerSplitter, //
        DataCenter, //
        DataStockCenter, // (dataStockManager)
        SatelliteDish //25
    }
}
