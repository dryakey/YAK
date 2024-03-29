﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Autin.Model;
//
//    var powerCost = PowerCost.FromJson(jsonString);

namespace Autin.Model
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class PowerCost
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("unit_id")]
        public string UnitId { get; set; }

        [JsonProperty("period_id")]
        public string PeriodId { get; set; }

        [JsonProperty("gen_output")]
        public double GenOutput { get; set; }

        [JsonProperty("declare_price")]
        public double DeclarePrice { get; set; }

        [JsonProperty("case_id")]
        public string CaseId { get; set; }

        [JsonProperty("client_id")]
        public long ClientId { get; set; }

        [JsonProperty("settlement_date")]
        public DateTimeOffset SettlementDate { get; set; }
    }

    public partial class PowerCost
    {
        public static List<PowerCost> FromJson(string json) => JsonConvert.DeserializeObject<List<PowerCost>>(json, Autin.Model.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<PowerCost> self) => JsonConvert.SerializeObject(self, Autin.Model.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
