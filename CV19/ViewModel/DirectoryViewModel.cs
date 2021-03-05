using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CV19.ViewModel
{
    internal class DirectoryViewModel : Base.ViewModel

    {
        public readonly DirectoryInfo _DirectoryInfo;

        public IEnumerable<DirectoryViewModel> SubDirectories
        {
            get
            {
                try
                {
                    return _DirectoryInfo
                        .EnumerateDirectories()
                        .Select(directory => new DirectoryViewModel(directory.FullName));
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }

                return Enumerable.Empty<DirectoryViewModel>();
            }
        }

        public IEnumerable<FileViewModel> Files
        {
            get
            {
                try
                {
                    var files = _DirectoryInfo
                         .EnumerateFiles()
                        .Select(file => new FileViewModel(file.FullName));
                    return files;
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }

                return Enumerable.Empty<FileViewModel>();
            }
        }

        public IEnumerable<object> DirectoryFiles
        {
            get
            {
                try
                {
                    return SubDirectories.Cast<object>().Concat(Files);
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }
                return Enumerable.Empty<object>();
            }
        }

        public string Name => _DirectoryInfo.Name;

        public string Path => _DirectoryInfo.FullName;

        public DateTime CreatedTime => _DirectoryInfo.CreationTime;

        public DirectoryViewModel(string Path) => _DirectoryInfo = new DirectoryInfo(Path);
    }

    internal class FileViewModel : Base.ViewModel
    {
        public readonly FileInfo _FileInfo;

        public string Name => _FileInfo.Name;
        public string Path => _FileInfo.FullName;
        public DateTime CreatedTime => _FileInfo.CreationTime;

        public FileViewModel(string Path) => _FileInfo = new FileInfo(Path);
    }
}