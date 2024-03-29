﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Autin.Model;
//
//    var nfgSensitivity = NfgSensitivity.FromJson(jsonString);

namespace Autin.Model
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class NfgSensitivity
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nfg_id")]
        public long NfgId { get; set; }

        [JsonProperty("node_id")]
        public string NodeId { get; set; }

        [JsonProperty("period_id")]
        public string PeriodId { get; set; }

        [JsonProperty("nfg_sensitivity")]
        public double NfgSensitivityNfgSensitivity { get; set; }

        [JsonProperty("client_id")]
        public long ClientId { get; set; }

        [JsonProperty("case_id")]
        public string CaseId { get; set; }

        [JsonProperty("settlement_date")]
        public DateTimeOffset SettlementDate { get; set; }
    }

    public partial class NfgSensitivity
    {
        public static List<NfgSensitivity> FromJson(string json) => JsonConvert.DeserializeObject<List<NfgSensitivity>>(json, Autin.Model.NfgSensitivityConverter.Settings);
    }

    public static class NfgSensitivitySerialize
    {
        public static string ToJson(this List<NfgSensitivity> self) => JsonConvert.SerializeObject(self, Autin.Model.NfgSensitivityConverter.Settings);
    }

    internal static class NfgSensitivityConverter
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
