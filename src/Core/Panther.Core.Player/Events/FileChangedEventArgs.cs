using System;

namespace Panther.Core.Player.Events
{
    public class FileChangedEventArgs : EventArgs
    {
        public FileChangedEventArgs(string currentPath, string previousPath)
        {
            CurrentPath = currentPath;
            PreviousPath = previousPath;
        }

        public string CurrentPath { get; set; }
        public string PreviousPath { get; set; }
    }
}
