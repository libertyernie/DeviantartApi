﻿using DeviantartApi.Attributes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeviantartApi.Requests.User
{
    public class WhoAmIRequest : Request<Objects.User>
    {
        public enum UserExpand
        {
            Details,
            Geo,
            Profile,
            Stats
        }

        [Parameter("user")]
        [Expands]
        public HashSet<UserExpand> UserExpands { get; set; } = new HashSet<UserExpand>();

        public override Task<Response<Objects.User>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var values = new Dictionary<string, string>();
            values.AddHashSetParameter(() => UserExpands);
            cancellationToken.ThrowIfCancellationRequested();
            return ExecuteDefaultGetAsync("user/whoami?" + values.ToGetParameters(), cancellationToken);
        }
    }
}
