﻿using System.Threading;
using System.Threading.Tasks;
using TMDbLib.Objects.Authentication;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;
using TMDbLib.Rest;

namespace TMDbLib.Client
{
    public partial class TMDbClient
    {
        public async Task<SearchContainer<SearchMovieWithRating>> GetGuestSessionRatedMoviesAsync(int page = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetGuestSessionRatedMoviesAsync(DefaultLanguage, page, cancellationToken).ConfigureAwait(false);
        }

        public async Task<SearchContainer<SearchMovieWithRating>> GetGuestSessionRatedMoviesAsync(string language, int page = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            RequireSessionId(SessionType.GuestSession);

            RestRequest request = _client.Create("guest_session/{guest_session_id}/rated/movies");

            if (page > 0)
                request.AddParameter("page", page.ToString());

            if (!string.IsNullOrEmpty(language))
                request.AddParameter("language", language);

            AddSessionId(request, SessionType.GuestSession, ParameterType.UrlSegment);

            RestResponse<SearchContainer<SearchMovieWithRating>> resp = await request.ExecuteGet<SearchContainer<SearchMovieWithRating>>(cancellationToken).ConfigureAwait(false);

            return resp;
        }

        public async Task<SearchContainer<SearchTvShowWithRating>> GetGuestSessionRatedTvAsync(int page = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetGuestSessionRatedTvAsync(DefaultLanguage, page, cancellationToken).ConfigureAwait(false);
        }

        public async Task<SearchContainer<SearchTvShowWithRating>> GetGuestSessionRatedTvAsync(string language, int page = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            RequireSessionId(SessionType.GuestSession);

            RestRequest request = _client.Create("guest_session/{guest_session_id}/rated/tv");

            if (page > 0)
                request.AddParameter("page", page.ToString());

            if (!string.IsNullOrEmpty(language))
                request.AddParameter("language", language);

            AddSessionId(request, SessionType.GuestSession, ParameterType.UrlSegment);

            RestResponse<SearchContainer<SearchTvShowWithRating>> resp = await request.ExecuteGet<SearchContainer<SearchTvShowWithRating>>(cancellationToken).ConfigureAwait(false);

            return resp;
        }

        public async Task<SearchContainer<TvEpisodeWithRating>> GetGuestSessionRatedTvEpisodesAsync(int page = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetGuestSessionRatedTvEpisodesAsync(DefaultLanguage, page, cancellationToken).ConfigureAwait(false);
        }

        public async Task<SearchContainer<TvEpisodeWithRating>> GetGuestSessionRatedTvEpisodesAsync(string language, int page = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            RequireSessionId(SessionType.GuestSession);

            RestRequest request = _client.Create("guest_session/{guest_session_id}/rated/tv/episodes");

            if (page > 0)
                request.AddParameter("page", page.ToString());

            if (!string.IsNullOrEmpty(language))
                request.AddParameter("language", language);

            AddSessionId(request, SessionType.GuestSession, ParameterType.UrlSegment);

            RestResponse<SearchContainer<TvEpisodeWithRating>> resp = await request.ExecuteGet<SearchContainer<TvEpisodeWithRating>>(cancellationToken).ConfigureAwait(false);

            return resp;
        }
    }
}
