using System;
namespace ActivityMonitor
{
    public interface ILogger
    {
        void Add(string message);
        void Write();
    }
}
