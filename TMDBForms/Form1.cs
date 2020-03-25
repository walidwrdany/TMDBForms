using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Net;
using TMDbLib.Objects.Collections;
using TMDBForms.ViewModels;

namespace TMDBForms
{
    public partial class Form1 : Form
    {

        private TMDbClient client;
        private DBVM dBVM;
        private bool shouldSave;
        private SearchContainer<SearchMovie> results;

        public Form1()
        {
            InitializeComponent();
            client = new TMDbClient("c6b31d1cdad6a56a23f0c913e2482a31", true);
            dBVM = new DBVM();
            shouldSave = false;
        }
        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.RestoreDirectory = true;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                textBox1.Clear();
                tb.Clear();
                textBox1.AppendText(dialog.FileName);
                await Db(dialog.FileName);
            }
        }
        private async Task Db(string directory)
        {

            FileInfo DB = new FileInfo("DB.json");
            button1.Enabled = false;
            checkBox1_poster.Enabled = false;
            checkBox1_backdrop.Enabled = false;


            try
            {
                dBVM = FetchDB(DB); //Fill Model from DataBase if Exist

                await FetchConfig(client);

                string[] extensions = new[] { ".MP4", ".MKV", ".TS" };
                var listOfFiles = GetAllFilesByType(directory, extensions);


                tb.AppendLine("");
                tb.AppendLine("found " + listOfFiles.Count.ToString() + " movie in \"" + directory + "\"");
                tb.AppendLine("---------------------------------------------------------------------------");

                foreach (var _movie in listOfFiles)
                {
                    try
                    {
                        results = new SearchContainer<SearchMovie>();

                        tb.AppendLine("Searched for movies: '" + Path.GetFileNameWithoutExtension(_movie) + "'");

                        results = await GetMovieByNameAsync(_movie);


                        if (results.Results.Count > 0)
                        {
                            tb.AppendText(", found " + results.TotalResults + " results in " + results.TotalPages + " pages");

                            SearchMovie searchMovie = results.Results.FirstOrDefault();
                            PrintResult(searchMovie);
                            Movie movie = await GetMovieByIdAsync(searchMovie.Id, _movie);

                            if (checkBox1_poster.Checked)
                                await DownloadDataAsync(movie.PosterPath, client.Config.Images.PosterSizes.ElementAtOrDefault(client.Config.Images.PosterSizes.Count - 2), _movie);

                            if (checkBox1_backdrop.Checked)
                                await DownloadDataAsync(movie.BackdropPath, client.Config.Images.BackdropSizes.ElementAtOrDefault(client.Config.Images.BackdropSizes.Count - 2), _movie);
                        }
                        else
                            tb.AppendText(", No results found");


                        tb.AppendLine("");
                        tb.AppendLine("");
                        tb.AppendLine("-----------------------");
                    }
                    catch (Exception ex)
                    {
                        tb.AppendError(ex.Message);
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                tb.AppendError(ex.Message, ex.ToString());
            }
            finally
            {
                if (shouldSave)
                {
                    tb.AppendLine("Save Changes");
                    string json = JsonConvert.SerializeObject(dBVM);
                    File.WriteAllText(DB.FullName, json, Encoding.UTF8);
                }

                File.WriteAllText("Log.txt", tb.Text, Encoding.UTF8);
                MessageBox.Show("Done.");

                button1.Enabled = true;
                checkBox1_poster.Enabled = true;
                checkBox1_backdrop.Enabled = true;
            }
        }
        private async Task DownloadDataAsync(string imagePath, string size, string item)
        {
            if (!String.IsNullOrWhiteSpace(imagePath))
            {
                var dir = Path.GetDirectoryName(item);
                if (!new FileInfo(dir + imagePath).Exists)
                {
                    Uri imageUri = client.GetImageUrl(size, imagePath);
                    tb.AppendLine(" " + imageUri);

                    using (WebClient wc = new WebClient())
                    {
                        wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                        await wc.DownloadFileTaskAsync(new Uri(imageUri.ToString()), dir + imagePath);
                    }
                }
            }
        }
        private async Task<SearchContainer<SearchMovie>> GetMovieByNameAsync(string stPath)
        {

            int year = 0;
            string name = "";
            string[] str2 = Regex.Replace(Path.GetFileNameWithoutExtension(stPath), @"[.()\:\!\-\[\]]", " ")
                .Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            var K = str2.Where(a => a.Length == 4 && int.TryParse(a, out int _temp)).LastOrDefault();
            if (K != null)
            {
                var Q = Array.IndexOf(str2, K);
                name = string.Join(" ", str2.Take(Q));
                int.TryParse(str2[Q], out year);
            }
            else
            {
                for (int i = 0; i < str2.Length; i++)
                {
                    name = string.Join(" ", str2.Take(i + 1));
                    results = await client.SearchMovieAsync(name);
                    if (results.Results.Count > 0)
                        continue;
                    else
                    {
                        name = string.Join(" ", str2.Take(i));
                        break;
                    }
                }
            }

            results = await client.SearchMovieAsync(name, 0, false, year);
            results = CheckResults(results, name);

            return results;
        }
        private SearchContainer<SearchMovie> CheckResults(SearchContainer<SearchMovie> results, string name)
        {
            if (results.Results.Count > 1)
                results.Results = results.Results.Where(a => a.Title.Like(name)).ToList();
            return results;
        }
        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        private async Task<Movie> GetMovieByIdAsync(int id, string moviePath)
        {
            Movie movie = new Movie();
            Location location = new Location();

            if (!dBVM.Movies.Any(a => a.Id == id))
            {
                movie = await client.GetMovieAsync(id, MovieMethods.Credits | MovieMethods.ReleaseDates | MovieMethods.Videos | MovieMethods.Keywords);
                dBVM.Movies.Add(movie);
                dBVM.Locations.Add(new Location() { Id = movie.Id, MoviePath = moviePath });

                shouldSave = true;

                return movie;
            }

            movie = dBVM.Movies.FirstOrDefault(s => s.Id == id);
            location = dBVM.Locations.FirstOrDefault(a => a.Id == id);

            if (location.MoviePath != moviePath)
            {
                location.MoviePath = moviePath;
                shouldSave = true;
            }

            return movie;
        }
        private async Task FetchConfig(TMDbClient client)
        {
            FileInfo configJson = new FileInfo("config.json");

            tb.AppendLine("Config file: " + configJson.FullName + ", Exists: " + configJson.Exists);

            if (configJson.Exists && configJson.LastWriteTimeUtc >= DateTime.UtcNow.AddHours(-1))
            {
                tb.AppendLine("Using stored config");
                string json = File.ReadAllText(configJson.FullName, Encoding.UTF8);

                client.SetConfig(JsonConvert.DeserializeObject<TMDbConfig>(json));
            }
            else
            {
                tb.AppendLine("Getting new config");
                var config = await client.GetConfigAsync();

                tb.AppendLine("Storing config");
                string json = JsonConvert.SerializeObject(config);
                File.WriteAllText(configJson.FullName, json, Encoding.UTF8);
            }
        }
        private DBVM FetchDB(FileInfo DB)
        {
            tb.AppendLine("DB file: " + DB.FullName + ", Exists: " + DB.Exists);

            dBVM.Movies = new List<Movie>();
            dBVM.Collections = new List<Collection>();
            dBVM.Locations = new List<Location>();

            if (DB.Exists)
            {
                tb.AppendLine("Using stored DB");
                string json = File.ReadAllText(DB.FullName, Encoding.UTF8);
                dBVM = JsonConvert.DeserializeObject<DBVM>(json);
            }
            else
                tb.AppendLine("Creating new DB");

            return dBVM;
        }
        private List<string> GetAllFilesByType(string directory, string[] types)
        {
            return Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories)
                .Where(file => types.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToList();
        }
        private void PrintResult(SearchMovie result)
        {
            tb.AppendLine(" ID : " + result.Id);
            tb.AppendLine(" Title : " + result.Title);
            tb.AppendLine(" Original Title : " + result.OriginalTitle);

            if (result.ReleaseDate.HasValue)
                tb.AppendLine(" Release date : " + result.ReleaseDate.Value.ToString("dd-MM-yyyy"));

            tb.AppendLine(" Popularity : " + result.Popularity);
            tb.AppendLine(" Vote Average : " + result.VoteAverage);
            tb.AppendLine(" Vote Count : " + result.VoteCount);
        }
    }
}
