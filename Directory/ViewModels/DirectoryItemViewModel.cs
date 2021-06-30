
using System.Linq;
using System.Collections.ObjectModel;
using System;
using System.Windows.Input;

namespace WpfTreeView
{
    public class DirectoryItemViewModel : BaseViewModel
    {
        public DirectoryItemType Type { get; set; }
        public string FullPath { get; set; }
        public string Name {  get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectorySctructure.GetFileFolderName(this.FullPath); } }

        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }
        public bool CanExpand {  get { return this.Type != DirectoryItemType.File; } }

        public ICommand ExpandCommand { get; set; }

        public DirectoryItemViewModel(string fullPath, DirectoryItemType type )
        {
            this.ExpandCommand = new RelayCommand(Expand);

            this.FullPath = fullPath;
            this.Type = type;

            this.ClearChildren();
        }

        public bool IsExpanded
        {
            get
            {
              return  this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                if (value == true)
                    Expand();
                else
                    this.ClearChildren();
            }
        }

        private void ClearChildren()
        {
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }

        private void Expand()
        {
            if (this.Type == DirectoryItemType.File)
                return;

            this.Children = new ObservableCollection<DirectoryItemViewModel>(
                DirectorySctructure.GetDirectoryContent(this.FullPath)
                .Select(content => new DirectoryItemViewModel(content.FullPath, content.Type))
                );
        }
    }
}
