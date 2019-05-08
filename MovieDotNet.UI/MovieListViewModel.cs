using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using MovieDotNet.Data;

namespace MovieDotNet.UI
{
    class MovieListViewModel : ViewModelBase
    {
        private int _userId;

        public MovieListViewModel(int userId)
        {
            _userId = userId;
            AddMovieCommand = new RelayCommand(AddMovieAction);
            RemoveMovieCommand = new RelayCommand(RemoveMovieAction);
            SelectMovieCommand = new RelayCommand(SelectMovieAction);
            BackToLoginCommand = new RelayCommand(BackToLoginAction);
            LoadMovies();
        }

        private void LoadMovies()
        {
            Movies = DataManager.Instance.GetMovies();
        }

        #region commands
        public ICommand AddMovieCommand { get; private set; }
        public ICommand RemoveMovieCommand { get; private set; }
        public ICommand SelectMovieCommand { get; private set; }
        public ICommand BackToLoginCommand { get; private set; }

        private void AddMovieAction()
        {
            if (NewMovieName.Trim() == "")
                return;
            DataManager.Instance.AddMovie(NewMovieName);
            NewMovieName = "";
            //MessageBox.Show("Movie added succesfully!");
            LoadMovies();
        }

        private void RemoveMovieAction()
        {
            if (CurMovieIndex >= movies.Count || CurMovieIndex == -1)
                return;
            DataManager.Instance.RemoveMovie(movies[CurMovieIndex].Id);
            LoadMovies();
        }

        private void SelectMovieAction()
        {
            if (CurMovieIndex >= movies.Count || CurMovieIndex == -1)
                return;
            var movie = movies[CurMovieIndex];
            VMLocator.Instance.Main = new MovieDetailsViewModel(_userId, movie);
        }

        private void BackToLoginAction()
        {
            VMLocator.Instance.Main = new LoginViewModel();
        }
        #endregion

        #region properties
        private List<Movie> movies;

        public List<Movie> FilteredMovies => SearchQuery == "" || SearchQuery == null ? Movies : Movies.Where(movie => movie.Name.Contains(SearchQuery)).ToList();

        public string SearchDescription
        {
            get
            {
                var movieCount = Movies.Count;
                var filteredCount = FilteredMovies.Count;
                if (movieCount == filteredCount)
                    return "Search Query";
                else
                    return "Search Query (" + (movieCount - filteredCount) + " hidden)";
            }
        }

        public List<Movie> Movies
        {
            get { return movies; }
            set
            {
                movies = value;
                RaisePropertyChanged(nameof(Movies));
                RaisePropertyChanged(nameof(FilteredMovies));
                RaisePropertyChanged(nameof(SearchDescription));
            }
        }


        private string newMovieName;

        public string NewMovieName
        {
            get { return newMovieName; }
            set
            {
                newMovieName = value;
                RaisePropertyChanged(nameof(NewMovieName));
            }
        }

        private int _curMovieIndex;

        public int CurMovieIndex
        {
            get { return _curMovieIndex; }
            set
            {
                _curMovieIndex = value;
                RaisePropertyChanged(nameof(CurMovieIndex));
            }
        }

        private string _searchQuery = "";

        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                _searchQuery = value;
                RaisePropertyChanged(nameof(SearchQuery));
                RaisePropertyChanged(nameof(FilteredMovies));
                RaisePropertyChanged(nameof(SearchDescription));
            }
        }

        #endregion

    }
}
