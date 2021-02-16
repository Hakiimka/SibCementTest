using FirebirdTest.Model;
using FirebirdTest.Repositories;
using FirebirdTest.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace FirebirdTest
{
    public class UsersViewModel : INotifyPropertyChanged
    {
        #region
        public event PropertyChangedEventHandler PropertyChanged;
        public static USERS user_temp;
        private bool ButtonFlag=true;

        private UserRepository UserRep = new UserRepository();
        private ObservableCollection<USERS> collection;
        private USERS user;

        private PermissionsRepository permissionsRepository = new PermissionsRepository();
        private ObservableCollection<TUI_PERMISSIONS> PermCollection;
        private TUI_PERMISSIONS permission;
        #endregion  // perem

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private int Delete()
        {
            if (User == null)
            {
                MessageBox.Show("Проблема с удалением\nвозможно вы не выбрали пользователя");
                return 0;
            }
            else
            {
               
               UserRep.Delete(User);
               // MessageBox.Show(UsersCollection[0].Middle_Name);
               //UsersCollection.Remove(User);
                return 0;
            }
        }
        private void AddUser()
        {
            AddWindow addwindow = new AddWindow();
            addwindow.Show();
        }
        private int Edit()
        {
            if (User == null)
            {
                MessageBox.Show("Проблема с изменением\nвозможно вы не выбрали пользователя");
                return 0;
            }
            else
            {
                EditWindow editwindow = new EditWindow();
                editwindow.Show();
                return 0;
            }
        }

        private void AddPermission()
        {
            AddPermissionWindow permissionwindow = new AddPermissionWindow();
            permissionwindow.Show();
        }
        private void DeletePermission()
        {
            permissionsRepository.Delete(permission);
        }
        private void refresh()
        {
            collection = new ObservableCollection<USERS>(UserRep.GetList());
            PermCollection = new ObservableCollection<TUI_PERMISSIONS>(permissionsRepository.GetList());
        }
        public bool IsButtonEnabled
        {
            get { return ButtonFlag; }
            set { ButtonFlag = value; OnPropertyChanged("IsButtonEnabled"); }
        }
        public USERS User
        {
            get { user_temp = user; return user; }
            set { user_temp = user; user = value; OnPropertyChanged("User"); }
        }
        public TUI_PERMISSIONS Permission
        {
            get { return permission; }
            set { permission = value; OnPropertyChanged("Permission"); }
        }

        public ObservableCollection<USERS> UsersCollection
        {
            get { return new ObservableCollection<USERS>(UserRep.GetList()); }
            set { collection = value; OnPropertyChanged("collection"); }
        }

        public ObservableCollection<TUI_PERMISSIONS> PermissionCollection
        {
            get { return new ObservableCollection<TUI_PERMISSIONS>(permissionsRepository.GetList()); }
            set { PermCollection = value; OnPropertyChanged("PermissionCollection"); }
        }
        public VMcommand DeleteButton
        {
            get { return new VMcommand((o) => { Delete(); refresh(); }); }
        }
        public VMcommand AddButton
        {
            get { return new VMcommand((o) => { AddUser(); refresh(); }); }
        }
        public VMcommand EditButton
        {
            get { return new VMcommand((o) => { Edit(); }); }
        }
        public VMcommand AddPermissionButton
        {
            get { return new VMcommand((o) => { AddPermission(); refresh(); }); }
        }
        public VMcommand DeletePermissionButton
        {
            get { return new VMcommand((o) => { DeletePermission(); refresh(); }); }
        }



    }
}
