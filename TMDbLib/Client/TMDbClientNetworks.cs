﻿using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.TvShows;
using TMDbLib.Rest;

namespace TMDbLib.Client
{
    public partial class TMDbClient
    {
        /// <summary>
        /// Retrieves a network by it's TMDb id. A network is a distributor of media content ex. HBO, AMC
        /// </summary>
        /// <param name="networkId">The id of the network object to retrieve</param>
        /// <param name="cancellationToken">A cancellation token</param>
        public async Task<Network> GetNetworkAsync(int networkId, CancellationToken cancellationToken = default(CancellationToken))
        {
            RestRequest req = _client.Create("network/{networkId}");
            req.AddUrlSegment("networkId", networkId.ToString(CultureInfo.InvariantCulture));

            RestResponse<Network> response = await req.ExecuteGet<Network>(cancellationToken).ConfigureAwait(false);

            return response;
        }

        /// <summary>
        /// Gets the logos of a network given a TMDb id
        /// </summary>
        /// <param name="networkId">The TMDb id of the network</param>
        /// <param name="cancellationToken">A cancellation token</param>
        public async Task<NetworkLogos> GetNetworkImagesAsync(int networkId, CancellationToken cancellationToken = default(CancellationToken))
        {
            RestRequest req = _client.Create("network/{networkId}/images");
            req.AddUrlSegment("networkId", networkId.ToString(CultureInfo.InvariantCulture));

            RestResponse<NetworkLogos> response = await req.ExecuteGet<NetworkLogos>(cancellationToken).ConfigureAwait(false);

            return response;
        }

        /// <summary>
        /// Gets the alternative names of a network given a TMDb id
        /// </summary>
        /// <param name="networkId">The TMDb id of the network</param>
        /// <param name="cancellationToken">A cancellation token</param>
        public async Task<AlternativeNames> GetNetworkAlternativeNamesAsync(int networkId, CancellationToken cancellationToken = default(CancellationToken))
        {
            RestRequest req = _client.Create("network/{networkId}/alternative_names");
            req.AddUrlSegment("networkId", networkId.ToString(CultureInfo.InvariantCulture));

            RestResponse<AlternativeNames> response = await req.ExecuteGet<AlternativeNames>(cancellationToken).ConfigureAwait(false);

            return response;
        }
    }
}