﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RabbitMQ.Management.Http
{
    internal sealed class ExchangeResponse
    {
        [JsonPropertyName("page")] public int Page { get; set; }
        [JsonPropertyName("page_count")] public int PageCount { get; set; }
        [JsonPropertyName("page_size")] public int PageSize { get; set; }
        [JsonPropertyName("total_count")] public int TotalCount { get; set; }
        [JsonPropertyName("items")] public List<ExchangeInfo> Items { get; set; }
    }
}

//{
//  "filtered_count": 1,
//  "item_count": 1,
//  "items": [
//    {
//      "arguments": { },
//      "auto_delete": false,
//      "durable": true,
//      "internal": false,
//      "message_stats": {
//            "publish_in": 3,
//        "publish_in_details": { "rate": 0.0 },
//        "publish_out": 3,
//        "publish_out_details": { "rate": 0.0 }
//        },
//      "name": "РИБ.MAIN.N001",
//      "type": "direct",
//      "user_who_performed_action": "guest",
//      "vhost": "/"
//    }
//  ],
//  "page": 1,
//  "page_count": 1,
//  "page_size": 1,
//  "total_count": 14
//}