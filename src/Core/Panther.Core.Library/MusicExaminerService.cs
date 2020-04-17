using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Panther.Core.Library
{
    // TODO implement using observable pattern
    public class MusicExaminerService : IMusicExaminerService
    {
        public IEnumerable<Task> ExamineAsync(string path, Func<string, Task> asyncFileAction)
        {
            var directoryInfo = new DirectoryInfo(path);

            var files = directoryInfo
                .GetFiles("*", SearchOption.AllDirectories)
                .Where(file => file.Attributes == (FileAttributes.Normal | FileAttributes.ReadOnly));

            foreach (var file in files)
            {
                yield return asyncFileAction(file.FullName);
            }
        }

        public IEnumerable<Task> ExamineAsync(IEnumerable<string> paths, Func<string, Task> asyncFileAction)
            => paths.SelectMany(path => ExamineAsync(path, asyncFileAction));
    }
}
