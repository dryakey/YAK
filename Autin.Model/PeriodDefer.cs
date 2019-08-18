﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Autin.Model;
//
//    var periodDefer = PeriodDefer.FromJson(jsonString);

namespace Autin.Model
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class PeriodDefer
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("period_id")]
        public string PeriodId { get; set; }

        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("period_timespan")]
        public long PeriodTimespan { get; set; }

        [JsonProperty("sensitivity_file")]
        public string SensitivityFile { get; set; }

        [JsonProperty("daytime_flag")]
        public bool DaytimeFlag { get; set; }

        [JsonProperty("sensitivity_flag")]
        public bool SensitivityFlag { get; set; }

        [JsonProperty("uc_flag")]
        public bool UcFlag { get; set; }

        [JsonProperty("day_number")]
        public long DayNumber { get; set; }

        [JsonProperty("client_id")]
        public long ClientId { get; set; }

        [JsonProperty("case_id")]
        public string CaseId { get; set; }

        [JsonProperty("settlement_date")]
        public DateTimeOffset SettlementDate { get; set; }
    }

    public partial class PeriodDefer
    {
        public static List<PeriodDefer> FromJson(string json) => JsonConvert.DeserializeObject<List<PeriodDefer>>(json, Autin.Model.PeriodDeferConverter.Settings);
    }

    public static class PeriodDeferSerialize
    {
        public static string ToJson(this List<PeriodDefer> self) => JsonConvert.SerializeObject(self, Autin.Model.PeriodDeferConverter.Settings);
    }

    internal static class PeriodDeferConverter
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