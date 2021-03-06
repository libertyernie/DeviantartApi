﻿using DeviantartApi.Attributes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeviantartApi.Requests.Browse
{
    /// <summary>
    /// Fetch MoreLikeThis result for a seed deviation
    /// </summary>
    public class MoreLikeThisRequest : PageableRequest<Objects.Browse>
    {
        public enum Error
        {
            InvalidPrintRequested = 0
        }

        public enum UserExpand
        {
            Watch
        }

        [Parameter("user")]
        [Expands]
        public HashSet<UserExpand> UserExpands { get; set; } = new HashSet<UserExpand>();

        [Parameter("mature_content")]
        public bool MatureContent { get; set; }

        /// <summary>
        /// The deviationid to fetch more like
        /// </summary>
        [Parameter("seed")]
        public string Seed { get; set; }

        /// <summary>
        /// The category to filter results. Default path: "/"
        /// </summary>
        [Parameter("category")]
        public string Category { get; set; } = "/";

        public MoreLikeThisRequest(string seed)
        {
            Seed = seed;
        }

        public override Task<Response<Objects.Browse>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var values = new Dictionary<string, string>();
            values.AddParameter(() => Seed);
            values.AddParameter(() => Category);
            values.AddParameter(() => Offset);
            values.AddParameter(() => Limit);
            values.AddHashSetParameter(() => UserExpands);
            values.AddParameter(() => MatureContent);
            cancellationToken.ThrowIfCancellationRequested();
            return ExecuteDefaultGetAsync("browse/morelikethis?" + values.ToGetParameters(), cancellationToken);
        }
    }
}
