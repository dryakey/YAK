﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Autin.Model;
//
//    var nfgLimit = NfgLimit.FromJson(jsonString);

namespace Autin.Model
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class NfgLimit
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nfg_id")]
        public long NfgId { get; set; }

        [JsonProperty("period_id")]
        public string PeriodId { get; set; }

        [JsonProperty("nfg_limit")]
        public double NfgLimitValue { get; set; }

        [JsonProperty("nfg_load")]
        public long NfgLoad { get; set; }

        [JsonProperty("client_id")]
        public long ClientId { get; set; }

        [JsonProperty("case_id")]
        public string CaseId { get; set; }

        [JsonProperty("settlement_date")]
        public DateTimeOffset SettlementDate { get; set; }

    }

    public partial class NfgLimit
    {
        public static List<NfgLimit> FromJson(string json) => JsonConvert.DeserializeObject<List<NfgLimit>>(json, Autin.Model.NfgLimitConverter.Settings);
    }

    public static class NfgLimitSerialize
    {
        public static string ToJson(this List<NfgLimit> self) => JsonConvert.SerializeObject(self, Autin.Model.NfgLimitConverter.Settings);
    }

    internal static class NfgLimitConverter
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
