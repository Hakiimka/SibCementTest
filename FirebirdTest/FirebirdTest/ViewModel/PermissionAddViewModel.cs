using FirebirdTest.Interfaces;
using FirebirdTest.Model;
using FirebirdTest.Repositories;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace FirebirdTest.ViewModel
{
    internal class PermissionAddViewModel : INotifyPropertyChanged, ICloseWindows
    {
        #region
        public Action Close { get; set; }
        private DelegateCommand _closeCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        private UserRepository UserRep = new UserRepository();
        private ViewModelsActionsRepository ActionRep = new ViewModelsActionsRepository();

        private ObservableCollection<USERS> usersCollection;
        private USERS user;

        private TUI_PERMISSIONS permission;
        private PermissionsRepository PermissionRep;


        private ObservableCollection<TUI_VIEW_MODELS_ACTIONS> actionsCollection;
        private TUI_VIEW_MODELS_ACTIONS action;
        private DateTime dateexpire = DateTime.Today;
        private DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(CloseWindow));
        #endregion
        private void CloseWindow()
        {
            Close?.Invoke();
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private int GivePermission()
        {
            try
            {
                if (User == null)
                {
                    MessageBox.Show("Проблема с добавлением\nвозможно какое-то из полей пустое");
                    return 0;
                }
                PermissionRep = new PermissionsRepository();
                permission = new TUI_PERMISSIONS
                {
                    User_Id = User.Id,
                    View_Model_Action = action.View_Model,
                    Date_Expire = dateexpire
                };
                PermissionRep.Insert(permission);
                return 0;
            } catch { MessageBox.Show("Проблема с добавлением\nвозможно какое-то из полей пустое"); return 0; }
        }

        public DateTime DateExpire
        {
            get { return dateexpire; }
            set { dateexpire = value; OnPropertyChanged("DateExpire"); }
        }
        public USERS User
        {
            get { return user; }
            set { user = value; OnPropertyChanged("User"); }
        }
        public TUI_VIEW_MODELS_ACTIONS VMAction
        {
            get { return action; }
            set { action = value; OnPropertyChanged("VMAction"); }
        }
        public ObservableCollection<USERS> UsersCollection
        {
            get { return new ObservableCollection<USERS>(UserRep.GetList()); }
            set { usersCollection = value; OnPropertyChanged("UsersCollection"); }
        }
        public ObservableCollection<TUI_VIEW_MODELS_ACTIONS> ActionsCollection
        {
            get { return new ObservableCollection<TUI_VIEW_MODELS_ACTIONS>(ActionRep.GetList()); }
            set { actionsCollection = value; OnPropertyChanged("ActionsCollection"); }
        }

        public VMcommand CancelButton
        {
            get { return new VMcommand((o) => { Close(); }); }
        }
        public VMcommand GiveButton
        {
            get { return new VMcommand((o) => { GivePermission(); if(User!=null)MessageBox.Show("Права выданы"); Close(); }); }
        }

    }
}
