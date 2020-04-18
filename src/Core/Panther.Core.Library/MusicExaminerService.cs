using Panther.Core.Library.Notification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Panther.Core.Library
{
    // TODO implement using observable pattern
    public class MusicExaminerService : IMusicExaminerService, IObservable<ProgressNotification>
    {
        private readonly List<IObserver<ProgressNotification>> _observers;

        public MusicExaminerService()
        {
            _observers = new List<IObserver<ProgressNotification>>();
        }

        public IEnumerable<Task> ExamineAsync(string path, Func<string, Task> asyncFileAction, int step = 0, CancellationToken cancellationToken = default)
        {
            var directoryInfo = new DirectoryInfo(path);

            var files = directoryInfo
                .GetFiles("*", SearchOption.AllDirectories)
                .Where(file => file.Attributes == (FileAttributes.Normal | FileAttributes.ReadOnly));

            var progress = new ProgressNotification
            {
                StepNumber = step,
                StepProcessed = 0,
                StepTotal = files.Count()
            };

            foreach (var file in files)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    yield break;
                }

                progress.StepProcessed++;
                yield return ExecuteAndNotify(path, asyncFileAction, progress);
            }
        }

        public IEnumerable<Task> ExamineAsync(IEnumerable<string> paths, Func<string, Task> asyncFileAction, CancellationToken cancellationToken = default)
        {
            int step = 0;
            foreach (var path in paths)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    yield break;
                }

                yield return Task.WhenAll(ExamineAsync(path, asyncFileAction, ++step, cancellationToken));
            }
        }

        public IDisposable Subscribe(IObserver<ProgressNotification> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new LoadStateUnsubscriber(_observers, observer);
        }

        private async Task ExecuteAndNotify(string fileName, Func<string, Task> asyncFileActioin, ProgressNotification progress)
        {
            Exception exception = null;

            try
            {
                await asyncFileActioin(fileName);
            }
            catch (Exception ex)
            {
                exception = ex;
            }


            foreach (var observer in _observers)
            {
                if (exception != null)
                {
                    observer.OnError(exception);
                    continue;
                }

                if (progress.StepProcessed == progress.StepTotal)
                {
                    observer.OnCompleted();
                    continue;
                }

                if (!(observer is IPeriodicObserver<ProgressNotification> progressObserver))
                {
                    observer.OnNext(progress);
                    continue;
                }

                if (progress.StepProcessed % progressObserver.Period == 0)
                {
                    observer.OnNext(progress);
                }
            }
        }
    }
}
