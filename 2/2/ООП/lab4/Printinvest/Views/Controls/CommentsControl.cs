using Printinvest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomControls
{

    public class CommentsControl : Control
    {
        static CommentsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CommentsControl),
                new FrameworkPropertyMetadata(typeof(CommentsControl)));
        }

        // Comments collection
        public static readonly DependencyProperty CommentsProperty =
            DependencyProperty.Register(nameof(Comments), typeof(ObservableCollection<Comment>),
                typeof(CommentsControl), new PropertyMetadata(new ObservableCollection<Comment>()));
        public ObservableCollection<Comment> Comments
        {
            get => (ObservableCollection<Comment>)GetValue(CommentsProperty);
            set => SetValue(CommentsProperty, value);
        }

        // NewCommentText
        public static readonly DependencyProperty NewCommentTextProperty =
            DependencyProperty.Register(nameof(NewCommentText), typeof(string), typeof(CommentsControl),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public string NewCommentText
        {
            get => (string)GetValue(NewCommentTextProperty);
            set => SetValue(NewCommentTextProperty, value);
        }

        // AddComment command
        public static readonly RoutedUICommand AddCommentCommand = new RoutedUICommand(
            "AddComment", "AddComment", typeof(CommentsControl));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            CommandBindings.Add(new CommandBinding(AddCommentCommand, OnAddComment, CanAddComment));


            // Привязываем команду к кнопке
            if (GetTemplateChild("PART_AddButton") is Button btn)
                btn.Command = AddCommentCommand;

            // Привязываем TextBox
            if (GetTemplateChild("PART_TextBox") is TextBox tb)
                tb.SetBinding(TextBox.TextProperty,
                    new Binding(nameof(NewCommentText)) { Source = this, Mode = BindingMode.TwoWay });

            if (GetTemplateChild("PART_ItemsPresenter") is ItemsPresenter ip)
            {
                // nothing
            }
        }

        public static readonly DependencyProperty NewCommentRatingProperty =
    DependencyProperty.Register(nameof(NewCommentRating), typeof(double),
        typeof(CommentsControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double NewCommentRating
        {
            get => (double)GetValue(NewCommentRatingProperty);
            set => SetValue(NewCommentRatingProperty, value);
        }


        public static readonly DependencyProperty CurrentUserProperty =
        DependencyProperty.Register(
            nameof(CurrentUser), typeof(User), typeof(CommentsControl),
            new PropertyMetadata(null, OnCurrentUserChanged));

        public User CurrentUser
        {
            get => (User)GetValue(CurrentUserProperty);
            set => SetValue(CurrentUserProperty, value);
        }

        private static void OnCurrentUserChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // При смене CurrentUser можно, например, очищать поле ввода:
            var ctrl = (CommentsControl)d;
            ctrl.NewCommentText = string.Empty;
        }

        // 2) В CanAddComment проверяем, что есть CurrentUser
        private void CanAddComment(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CurrentUser != null
                           && !string.IsNullOrWhiteSpace(NewCommentText);
        }

        // 3) В OnAddComment используем CurrentUser, а не Environment.UserName
        private void OnAddComment(object sender, ExecutedRoutedEventArgs e)
        {
            Comments.Add(new Comment
            {
                Author = CurrentUser.Name,
                Text = NewCommentText.Trim(),
                Rating = NewCommentRating == -1 ? 0 : NewCommentRating == 6 ? 5 : NewCommentRating,
                Timestamp = DateTime.Now
            });

            NewCommentText = string.Empty;
            NewCommentRating = 0.0;
        }
    }
}