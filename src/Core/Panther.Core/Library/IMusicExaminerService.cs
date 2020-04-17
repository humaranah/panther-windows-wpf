using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Panther.Core.Library
{
    public interface IMusicExaminerService
    {
        IEnumerable<Task> ExamineAsync(string path, Func<string, Task> asyncFileAction);
        IEnumerable<Task> ExamineAsync(IEnumerable<string> paths, Func<string, Task> asyncFileAction);
    }
}
