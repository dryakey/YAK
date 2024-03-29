﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Autin.Model;
//
//    var unitParam = UnitParam.FromJson(jsonString);

namespace Autin.Model
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class UnitParam
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("unit_id")]
        public string UnitId { get; set; }

        [JsonProperty("unit_type")]
        public string UnitType { get; set; }

        [JsonProperty("unit_type_name")]
        public string UnitTypeName { get; set; }

        [JsonProperty("unit_capacity")]
        public double UnitCapacity { get; set; }

        [JsonProperty("unit_pmin")]
        public double UnitPmin { get; set; }

        [JsonProperty("unit_pmax")]
        public double UnitPmax { get; set; }

        [JsonProperty("unit_loss_fact")]
        public double UnitLossFact { get; set; }

        [JsonProperty("unit_self_use_fact")]
        public double UnitSelfUseFact { get; set; }

        [JsonProperty("unit_min_offtime")]
        public double UnitMinOfftime { get; set; }

        [JsonProperty("unit_min_ontime")]
        public double UnitMinOntime { get; set; }

        [JsonProperty("unit_ramp_up")]
        public double UnitRampUp { get; set; }

        [JsonProperty("unit_ramp_down")]
        public double UnitRampDown { get; set; }

        [JsonProperty("unit_ramp_start")]
        public double UnitRampStart { get; set; }

        [JsonProperty("unit_ramp_stop")]
        public double UnitRampStop { get; set; }

        [JsonProperty("unit_ontime_start")]
        public double UnitOntimeStart { get; set; }

        [JsonProperty("unit_ontime_stop")]
        public double UnitOntimeStop { get; set; }

        [JsonProperty("unit_cost_cold_start")]
        public double UnitCostColdStart { get; set; }

        [JsonProperty("unit_cost_warm_start")]
        public double UnitCostWarmStart { get; set; }

        [JsonProperty("unit_cost_hot_start")]
        public double UnitCostHotStart { get; set; }

        [JsonProperty("unit_time_to_warm")]
        public double UnitTimeToWarm { get; set; }

        [JsonProperty("unit_time_to_cold")]
        public double UnitTimeToCold { get; set; }

        [JsonProperty("unit_co2_emission")]
        public double UnitCo2Emission { get; set; }

        [JsonProperty("unit_so2_emission")]
        public double UnitSo2Emission { get; set; }

        [JsonProperty("unit_nox_emission")]
        public double UnitNoxEmission { get; set; }

        [JsonProperty("unit_soot_emission")]
        public double UnitSootEmission { get; set; }

        [JsonProperty("unit_offtime_eco_min")]
        public double UnitOfftimeEcoMin { get; set; }

        [JsonProperty("unit_ontime_eco_min")]
        public double UnitOntimeEcoMin { get; set; }

        [JsonProperty("unit_cost_average")]
        public double UnitCostAverage { get; set; }

        [JsonProperty("client_id")]
        public long ClientId { get; set; }

        [JsonProperty("case_id")]
        public string CaseId { get; set; }

        [JsonProperty("settlement_date")]
        public DateTimeOffset SettlementDate { get; set; }
    }

    public partial class UnitParam
    {
        public static List<UnitParam> FromJson(string json) => JsonConvert.DeserializeObject<List<UnitParam>>(json, Autin.Model.UnitParamConverter.Settings);
    }

    public static class UnitParamSerialize
    {
        public static string ToJson(this List<UnitParam> self) => JsonConvert.SerializeObject(self, Autin.Model.UnitParamConverter.Settings);
    }

    internal static class UnitParamConverter
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
