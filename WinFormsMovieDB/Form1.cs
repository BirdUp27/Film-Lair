using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Drawing;

namespace WinFormsMovieDB
{
    public partial class Form1 : Form
    {
        private IConfiguration _config;
        private bool _handlingDoubleClick = false;

        private void pictureBoxPoster_Click(object? sender, EventArgs e)
        {
            if (pictureBoxPoster.Image != null)
            {
                var posterForm = new PosterForm(pictureBoxPoster.Image);
                posterForm.ShowDialog(this);
            }
        }

        public Form1()
        {
            InitializeComponent();
            searchTypeComboBox.Items.Add("Title");
            searchTypeComboBox.Items.Add("Actor");
            searchTypeComboBox.SelectedIndex = 0;
            var toolTip = new ToolTip();
            toolTip.SetToolTip(buttonSearch, "Search for movies by title or actor");
            toolTip.SetToolTip(buttonWhite, "Switch to the Light theme");
            toolTip.SetToolTip(buttonBlack, "Switch to the Dark theme");
            toolTip.SetToolTip(buttonQuit, "GET OUT!!!!");
            pictureBoxPoster.Click += pictureBoxPoster_Click;

            // Load appsettings.json
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            this.Load += Form1_Load;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
        }

        // Button event handlers for background color. I mostly did this because I felt like it
        private void buttonBlack_Click(object? sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.Black;
        }

        private void buttonWhite_Click(object? sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.White;
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            try
            {
                string? apiKey = _config["OMDbApiKey"];
                if (string.IsNullOrEmpty(apiKey))
                {
                    MessageBox.Show("OMDb API key not found in appsettings.json. Please enter the API key");
                    return;
                }

                string url = $"http://www.omdbapi.com/?s=2024&type=movie&apikey={apiKey}";

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetStringAsync(url);
                    var result = JsonConvert.DeserializeObject<OmdbSearchResult>(response);

                    if (result?.Search != null)
                    {
                        // Fetch imdbRating for each movie
                        foreach (var movie in result.Search)
                        {
                            string detailsUrl = $"http://www.omdbapi.com/?i={movie.imdbID}&apikey={apiKey}";
                            var detailsResponse = await httpClient.GetStringAsync(detailsUrl);
                            var details = JsonConvert.DeserializeObject<MovieDetails>(detailsResponse);
                            movie.ImdbRating = details?.imdbRating;
                        }
                        dataGridView1.DataSource = result.Search;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        MessageBox.Show("Either no movies were found or there was an error in API response.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private async void buttonSearch_Click(object? sender, EventArgs e)
        {
            string searchTerm = textBoxSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Please enter a search term.");
                return;
            }

            string? apiKey = _config["OMDbApiKey"];
            string url = "";
            string searchType = searchTypeComboBox.SelectedItem?.ToString() ?? "Title";

            if (searchType == "Title")
            {
                // Search by title
                url = $"http://www.omdbapi.com/?s={Uri.EscapeDataString(searchTerm)}&type=movie&apikey={apiKey}";
            }
            else if (searchType == "Actor")
            {
                // An attempt at an actor search, HEAVILY W.I.P.
                url = $"http://www.omdbapi.com/?s={Uri.EscapeDataString(searchTerm)}&type=movie&apikey={apiKey}";
            }

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<OmdbSearchResult>(response);

                if (result?.Search != null)
                {
                    // If searching by Actor, filter results by actor's name
                    if (searchType == "Actor")
                    {
                        var filteredMovies = new List<Movie>();
                        foreach (var movie in result.Search)
                        {
                            // Fetch details for each movie to check for the actor...
                            string detailsUrl = $"http://www.omdbapi.com/?i={movie.imdbID}&apikey={apiKey}";
                            var detailsResponse = await httpClient.GetStringAsync(detailsUrl);
                            var details = JsonConvert.DeserializeObject<MovieDetails>(detailsResponse);

                            if (details?.Actors != null && details.Actors.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                movie.ImdbRating = details?.imdbRating; // Add the rating to the filtered movie
                                filteredMovies.Add(movie);
                            }
                        }
                        dataGridView1.DataSource = filteredMovies;
                        if (filteredMovies.Count == 0)
                            MessageBox.Show("No movies found for that actor.");
                    }
                    else
                    {
                        // Fetch imdbRating for each movie in title search
                        foreach (var movie in result.Search)
                        {
                            string detailsUrl = $"http://www.omdbapi.com/?i={movie.imdbID}&apikey={apiKey}";
                            var detailsResponse = await httpClient.GetStringAsync(detailsUrl);
                            var details = JsonConvert.DeserializeObject<MovieDetails>(detailsResponse);
                            movie.ImdbRating = details?.imdbRating;
                        }
                        dataGridView1.DataSource = result.Search;
                    }
                }
                else
                {
                    dataGridView1.DataSource = null;
                    MessageBox.Show("No movies found or error in API response.");
                }
            }
        }


        // Double-click handler for showing details and cool poster
        private async void dataGridView1_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (_handlingDoubleClick) return;
            _handlingDoubleClick = true;

            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                var selectedMovie = dataGridView1.Rows[e.RowIndex].DataBoundItem as Movie;
                if (selectedMovie == null || string.IsNullOrEmpty(selectedMovie.imdbID)) return;

                string? apiKey = _config["OMDbApiKey"];
                string url = $"http://www.omdbapi.com/?i={selectedMovie.imdbID}&apikey={apiKey}&plot=full";

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetStringAsync(url);
                    var details = JsonConvert.DeserializeObject<MovieDetails>(response);

                    // Show details in a MessageBox
                    MessageBox.Show(
                        $"Title: {details?.Title ?? "N/A"}\n" +
                        $"Year: {details?.Year ?? "N/A"}\n" +
                        $"Genre: {details?.Genre ?? "N/A"}\n" +
                        $"Actors: {details?.Actors ?? "N/A"}\n" +
                        $"Plot: {details?.Plot ?? "N/A"}\n" +
                        $"IMDB Rating: {details?.imdbRating ?? "N/A"}",
                        "Movie Details"
                    );

                    // Show the cool poster
                    if (!string.IsNullOrEmpty(details?.Poster) && details.Poster != "N/A")
                    {
                        try
                        {
                            var imageStream = await httpClient.GetStreamAsync(details.Poster);
                            pictureBoxPoster.Image = System.Drawing.Image.FromStream(imageStream);
                        }
                        catch
                        {
                            pictureBoxPoster.Image = null;
                        }
                    }
                    else
                    {
                        pictureBoxPoster.Image = null; // Just in case there is none, name one movie that lacks posters...
                    }
                }
            }
            finally
            {
                _handlingDoubleClick = false;
            }
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            this.Close(); // GET OUT!!!!!!!!!!!!!!!!!
        }
    }


    // Classes for deserializing OMDb API results
    public class OmdbSearchResult
    {
        public List<Movie>? Search { get; set; }
        public string? totalResults { get; set; }
        public string? Response { get; set; }
    }

    public class Movie
    {
        public string? Title { get; set; }
        public string? Year { get; set; }
        public string? imdbID { get; set; }
        public string? ImdbRating { get; set; }
    }

    public class MovieDetails
    {
        public string? Title { get; set; }
        public string? Year { get; set; }
        public string? Genre { get; set; }
        public string? Plot { get; set; }
        public string? imdbRating { get; set; }
        public string? Poster { get; set; }
        public string? Actors { get; set; }
    }
}
