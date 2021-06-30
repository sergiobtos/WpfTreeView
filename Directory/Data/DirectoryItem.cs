

namespace WpfTreeView
{

    /// <summary>
    /// Information about a directory item such as a drive, a file, or a folder
    /// </summary>

    public class DirectoryItem
    {
        public DirectoryItemType Type { get; set; }

        public string FullPath { get; set; }

        public string Name { get { 
                return this.Type==DirectoryItemType.Drive ? 
                    this.FullPath : 
                    DirectorySctructure.GetFileFolderName(this.FullPath); } 
        }
    }
}
