﻿using IMDbApiLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IMDbApiLib
{
    public partial class ApiLib
    {
        public async Task DownloadSubtitleAsync(string movieRootDir, SubtitleData subtitleData, ProgressData progress = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var allSubtitles = new List<SubtitleDataDetail>();

                if (subtitleData.Subtitles != null)
                    allSubtitles.AddRange(subtitleData.Subtitles);

                Utils.CreateSubtitleDataDirectoris(movieRootDir, subtitleData);

                movieRootDir = Path.Combine(movieRootDir, "Subtitles");

                int total = allSubtitles.Count;
                int currentIndex = 1;

                if (subtitleData.Subtitles != null)
                {
                    foreach (var sub in subtitleData.Subtitles)
                    {
                        if (cancellationToken.IsCancellationRequested)
                            throw new TaskCanceledException("Operation canceled by user.");
                        progress?.Report(currentIndex, total);
                        currentIndex++;

                        string fileName = Utils.RenameToPhisicalName($"{sub.Title}-{sub.Id}.zip");
                        string fileDir = Path.Combine(movieRootDir, "Fa");
                        if (sub.SeasonNumber.HasValue)
                            fileDir = Path.Combine(fileDir, $"Season {sub.SeasonNumber.Value}");

                        fileName = Path.Combine(fileDir, fileName);
                        await Utils.DownloadFileAsync(fileName, sub.Link, _webProxy);
                    }
                }

            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SubtitleData> SubtitleDataAsync(string id, Language language, int? seasonNumber = null)
        {
            try
            {
                string initUrl = $"{language}/API/Subtitles/{_apiKey}/{id}";
                if (seasonNumber.HasValue)
                    initUrl = $"{language}/API/Subtitles/{_apiKey}/{id}/{seasonNumber}";

                string url = $"{BaseUrl}/{initUrl}";
                string json = await Utils.DownloadJsonAsync(url, _webProxy);
                if (string.IsNullOrEmpty(json))
                    return new SubtitleData() { ErrorMessage = "Server Not Founded" };

                var data = JsonConvert.DeserializeObject<SubtitleData>(json);
                return data;
            }
            catch (Exception ex)
            {
                return new SubtitleData() { ErrorMessage = ex.Message };
            }
        }

        public async Task<SubtitleData> SubtitleDataAsync(string id, int? seasonNumber = null)
        {
            return await SubtitleDataAsync(id, Language.ar, seasonNumber);
        }

    }
}
