using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Panther.Core.Library
{
    public interface IMusicExaminerService
    {
        IEnumerable<Task> ExamineAsync(string path, Func<string, Task> asyncFileAction, int step = 0, CancellationToken cancellationToken = default);
        IEnumerable<Task> ExamineAsync(IEnumerable<string> paths, Func<string, Task> asyncFileAction, CancellationToken cancellationToken = default);
    }
}
