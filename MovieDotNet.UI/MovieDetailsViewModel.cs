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
    class MovieDetailsViewModel : ViewModelBase
    {
        private Movie _movie;
        private int _userId;

        public MovieDetailsViewModel(int userId, Movie movie)
        {
            _userId = userId;
            _movie = movie;

            BackButtonCommand = new RelayCommand(BackButtonAction);
            RateCommand = new RelayCommand<int>(RateAction);
            CommentCommand = new RelayCommand(CommentAction);
        }

        public bool ShowRatingDialog => !DataManager.Instance.DidUserRateMovie(_userId, _movie.Id);

        public bool ShowCommentDialog => !DataManager.Instance.DidUserCommentMovie(_userId, _movie.Id);

        public ICommand BackButtonCommand { get; private set; }
        public ICommand RateCommand { get; private set; }
        public ICommand CommentCommand { get; private set; }
        private void BackButtonAction()
        {
            VMLocator.Instance.Main = new MovieListViewModel(_userId);
        }

        private void RateAction(int rating)
        {
            if (rating < 1 || rating > 5)
                return;
            DataManager.Instance.RateMovie(_userId, _movie.Id, rating);
            RaisePropertyChanged(nameof(ShowRatingDialog));
        }

        private void CommentAction()
        {
            if (NewCommentBody.Trim() == "")
                return;
            DataManager.Instance.CommentMovie(_userId, _movie.Id, NewCommentBody);
            NewCommentBody = "";
            RaisePropertyChanged(nameof(Comments));
            RaisePropertyChanged(nameof(ShowCommentDialog));
        }       

        public string Name => _movie.Name;

        public List<Comment> Comments => _movie.Comment.ToList();

        private string _newCommentBody;

        public string NewCommentBody
        {
            get { return _newCommentBody; }
            set
            {
                _newCommentBody = value;
                RaisePropertyChanged(nameof(NewCommentBody));
            }
        }

    }
}
