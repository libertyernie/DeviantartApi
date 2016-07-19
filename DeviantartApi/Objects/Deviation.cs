﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DeviantartApi.Objects
{
    public class Deviation : BaseObject
    {
        [JsonProperty("deviationid")]
        public string DeviationId { get; set; }
        [JsonProperty("printId")]
        public string PrintId { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("category_path")]
        public string CategoryPath { get; set; }
        [JsonProperty("is_favourited")]
        public bool IsFavourited { get; set; }
        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; set; }
        [JsonProperty("author")]
        public User Author { get; set; }
        [JsonProperty("stats")]
        public StatsClass Stats { get; set; }
        [JsonProperty("published_time")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime PublishedTime { get; set; }
        [JsonProperty("allows_comments")]
        public bool AllowCommennts { get; set; }
        [JsonProperty("preivew")]
        public ImageClass Preview { get; set; }
        [JsonProperty("content")]
        public ImageClass Content { get; set; }
        [JsonProperty("thumbs")]
        public List<ImageClass> Thumbs { get; set; }
        [JsonProperty("videos")]
        public List<VideoClass> Videos { get; set; }
        [JsonProperty("flash")]
        public FlashClass Flash { get; set; }
        [JsonProperty("daily_deviation")]
        public DailyDeviationClass DailyDeviation { get; set; }
        [JsonProperty("excerpt")]
        public string Excerpt { get; set; }
        [JsonProperty("is_mature")]
        public bool IsMature { get; set; }
        [JsonProperty("id_downloadable")]
        public bool IsDownloadable { get; set; }
        [JsonProperty("download_filesize")]
        public int DownloadFilesize { get; set; }
        [JsonProperty("challenge")]
        public ChallengeClass Challenge { get; set; }
        [JsonProperty("challenge_entry")]
        public ChallengeEntryClass ChallengeEntry { get; set; }
        [JsonProperty("motion_book")]
        public MotionBookClass MotionBook { get; set; }


        public class StatsClass
        {
            [JsonProperty("comments")]
            public int Comments { get; set; }
            [JsonProperty("favourites")]
            public int Favourites { get; set; }
        }

        public class ImageClass
        {
            [JsonProperty("src")]
            public string Src { get; set; }
            [JsonProperty("height")]
            public int Height { get; set; }
            [JsonProperty("width")]
            public int Width { get; set; }
            [JsonProperty("transparency")]
            public bool Transparency { get; set; }
            [JsonProperty("filesize")]
            public int Filesize { get; set; }
        }

        public class VideoClass
        {
            [JsonProperty("src")]
            public string Src { get; set; }
            [JsonProperty("quality")]
            public string Quality { get; set; }
            [JsonProperty("filesize")]
            public int Filesize { get; set; }
            [JsonProperty("duration")]
            public int Duration { get; set; }
        }

        public class FlashClass
        {
            [JsonProperty("src")]
            public string Src { get; set; }
            [JsonProperty("width")]
            public int Width { get; set; }
            [JsonProperty("height")]
            public int Height { get; set; }
        }

        public class DailyDeviationClass
        {
            [JsonProperty("body")]
            public string Body { get; set; }
            [JsonProperty("time")]
            public string Time { get; set; }
            [JsonProperty("giver")]
            public User Giver { get; set; }
            [JsonProperty("suggester")]
            public User Suggester { get; set; }
        }

        public class ChallengeClass
        {
            //todo: find out wtf is going on
        }

        public class ChallengeEntryClass
        {
            [JsonProperty("challengeid")]
            public string ChallengeId { get; set; }
            [JsonProperty("challenge_title")]
            public string ChallengeTitle { get; set; }
            [JsonProperty("challenge")]
            public Deviation Challenge { get; set; }
            [JsonProperty("timed_duration")]
            public int TimedDuration { get; set; }
            [JsonProperty("submission_time")]
            public string SubmissionTime { get; set; }
        }

        public class MotionBookClass
        {
            [JsonProperty("embed_url")]
            public string EmbedUrl { get; set; }
        }

        private class UnixDateTimeConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var dateTime = (DateTime) value;
                writer.WriteValue((long)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(long);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var unixTime = (long) reader.Value;
                var dateTime =
                    new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(unixTime).ToLocalTime();
                return dateTime;
            }
        }
    }
}