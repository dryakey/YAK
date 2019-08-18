﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Autin.Model;
//
//    var loadDeferParam = LoadDeferParam.FromJson(jsonString);

namespace Autin.Model
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class LoadDeferParam
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("period_id")]
        public string PeriodId { get; set; }

        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("load")]
        public double Load { get; set; }

        [JsonProperty("loss")]
        public double Loss { get; set; }

        [JsonProperty("reserve")]
        public bool Reserve { get; set; }

        [JsonProperty("reserve_negative")]
        public bool ReserveNegative { get; set; }

        [JsonProperty("reserve_agc")]
        public bool ReserveAgc { get; set; }

        [JsonProperty("reserve_10min")]
        public bool Reserve10Min { get; set; }

        [JsonProperty("reserve_30min")]
        public bool Reserve30Min { get; set; }

        [JsonProperty("client_id")]
        public long ClientId { get; set; }

        [JsonProperty("case_id")]
        public string CaseId { get; set; }

        [JsonProperty("settlement_date")]
        public DateTimeOffset SettlementDate { get; set; }
    }

    public partial class LoadDeferParam
    {
        public static List<LoadDeferParam> FromJson(string json) => JsonConvert.DeserializeObject<List<LoadDeferParam>>(json, Autin.Model.LoadDeferParamConverter.Settings);
    }

    public static class LoadDeferParamSerialize
    {
        public static string ToJson(this List<LoadDeferParam> self) => JsonConvert.SerializeObject(self, Autin.Model.LoadDeferParamConverter.Settings);
    }

    internal static class LoadDeferParamConverter
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