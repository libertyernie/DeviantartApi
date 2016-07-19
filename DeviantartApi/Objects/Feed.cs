﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DeviantartApi.Objects
{
    public class Feed : Pageable
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        public class Item
        {
            [JsonProperty("ts")]
            [JsonConverter(typeof(IsoDateTimeConverter))]
            public DateTime TimeStamp { get; set; }
            [JsonProperty("type")]
            [JsonConverter(typeof(EventTypeEnumConverter))]
            public EventType Type { get; set; }
            [JsonProperty("by_user")]
            public User ByUser { get; set; }
            [JsonProperty("deviations")]
            public List<Deviation> Deviations { get; set; }
            [JsonProperty("bucketid")]
            public string BucketId { get; set; }
            [JsonProperty("bucket_total")]
            public int BucketTotal { get; set; }
            [JsonProperty("status")]
            public Status Status { get; set; }
            [JsonProperty("comment")]
            public Comment Comment { get; set; }
            [JsonProperty("comment_parent")]
            public Comment CommentParent { get; set; }
            [JsonProperty("comment_deviation")]
            public Deviation CommentDeviation { get; set; }
            [JsonProperty("comment_profile")]
            public User CommentProfile { get; set; }
            [JsonProperty("critique_text")]
            public string CritiqueText { get; set; }
            [JsonProperty("collection")]
            public CollectionClass Collection { get; set; }
            [JsonProperty("formerly")]
            public string Formerly { get; set; }
            [JsonProperty("added_count")]
            public int AddedCount { get; set; }
            [JsonProperty("poll")]
            public PollClass Poll { get; set; }

            public enum EventType
            {
                DeviationSubmitted,
                JournalSubmitted,
                UsernameChange,
                Status,
                CollectionUpdate,
                Unknown
            }

            public class CollectionClass
            {
                [JsonProperty("folderid")]
                public string FolderId { get; set; }
                [JsonProperty("name")]
                public string Name { get; set; }
                [JsonProperty("url")]
                public string Url { get; set; }
                [JsonProperty("size")]
                public int Size { get; set; }
            }

            public class PollClass
            {
                [JsonProperty("question")]
                public string Question { get; set; }
                [JsonProperty("total_votes")]
                public int TotalVotes { get; set; }
                [JsonProperty("answers")]
                public List<AnswerClass> Answers { get; set; }

                public class AnswerClass
                {
                    [JsonProperty("answer")]
                    public string Answer { get; set; }
                    [JsonProperty("votes")]
                    public int Votes { get; set; }
                }
            }

            private class EventTypeEnumConverter : JsonConverter
            {
                public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
                {
                    var eventType = (EventType)value;
                    var str = Regex.Replace(eventType.ToString(), @"(?<!_)([A-Z])", "_$1");
                    if (eventType.ToString()[0] != str[0])
                        str = str.Remove(0, 1);
                    writer.WriteValue(str.ToLower());

                }

                public override bool CanConvert(Type objectType)
                {
                    return objectType == typeof(string);
                }

                public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
                {
                    var enumString = (string)reader.Value;
                    var eventType = EventType.Unknown;

                    switch (enumString)
                    {
                        case "deviation_submitted":
                            eventType = EventType.DeviationSubmitted;
                            break;
                        case "journal_submitted":
                            eventType = EventType.JournalSubmitted;
                            break;
                        case "username_change":
                            eventType = EventType.UsernameChange;
                            break;
                        case "status":
                            eventType = EventType.Status;
                            break;
                        case "collection_update":
                            eventType = EventType.CollectionUpdate;
                            break;
                    }

                    return eventType;
                }
            }
        }
    }
}