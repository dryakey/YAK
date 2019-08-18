using System;
using System.Collections.Generic;

namespace Autin.Enum
{
    public class Route
    {
        public static string SERVER_PATH = "http://59.110.173.242/";
        public static string API_PATH = "api/";
        public static Dictionary<string, string> URLDictionary = new Dictionary<string, string>() {
            { "api.login",                          "login" },
            { "api.logout",                         "logout" },
            { "api.forget_password",                "password/forget-password" },
            { "api.reset_password",                 "password/reset-password" },
            { "api.get_control_info",               "get/get-control-info" },
            { "api.get_unit_param",                 "get/get-unit-param" },
            { "api.get_unit_initial_param",         "get/get-unit-init-param" },
            { "api.get_unit_price",                 "get/get-unit-price" },
            { "api.get_unit_start_price",           "get/get-unit-time-start-price" },
            { "api.get_unit_node",                  "get/get-unit-node-list" },
            { "api.get_element_list",               "get/get-element-list" },
            { "api.get_energy_cost",                "get/get-energy-cost" },
            { "api.get_analysis_case",              "get/get-analysis-case"},
            { "api.get_nfg_limit",                  "get/get-nfg-limit"},
            { "api.get_system_load_predict",        "get/get-system-load-predict"},
            { "api.set_unit_config_data",           "set/set-unit-config-data" },
            { "api.set_unit_price_data",            "set/set-unit-price-data" },
            { "api.set-bus-load-predict-data",      "set/set-bus-load-predict-data" },
            { "api.set_consumer_load",              "set/set-consumer-load" },
            { "api.set_system_load_predict",        "set/set-system-load-predict" },
            { "api.prepare_settlement",             "exe/prepare-settlement"},
            { "api.start_settlement",               "exe/start-settlement"},
            { "api.post_settlement",                "exe/post-settlement"}
        };
    }
}
