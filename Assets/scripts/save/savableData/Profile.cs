using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class Profile
{
    [JsonProperty("Name")]
    public string Name { get; set; }

    [JsonProperty("DateMade")]
    public DateTime DateMade { get; set; }

    [JsonProperty("DateSeen")]
    public DateTime DateSeen { get; set; }

    [JsonProperty("TimePlayed")]
    public TimeSpan TimePlayed { get; set; }

    [JsonProperty("Statistics")]
    public Statistics Statistics { get; set; } = new Statistics();
}

public partial class Statistics
{
    [JsonProperty("networth")]
    public long Networth { get; set; }

    [JsonProperty("money")]
    public long Money { get; set; }

    [JsonProperty("data")]
    public long Data { get; set; }
}




