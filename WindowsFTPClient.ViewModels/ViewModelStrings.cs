using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFTPClient.ViewModels
{
    public static class ViewModelStrings
    {
        public static string GetRenameCaption()
        {
            return "Rename";
        }
        public static string GetRenameQuestion()
        {
            return "What would you like to name the file?";
        }
        public static string GetNameSlashError()
        {
            return "Name cannot contain \"/\"";
        }
        public static string GetFileExists()
        {
            return "A file with this name already exists";
        }
        public static string GetCouldNotParseURL()
        {
            return "Could not parse URL, is this a valid URL?";
        }
        public static string GetConfirmDelete()
        {
            return "Are you sure you would like to delete these files?";
        }
        public static string CouldNotDelete(string fullName, string error)
        {
            return $"Could not delete {fullName}{Environment.NewLine}{error}";
        }
    }
}
