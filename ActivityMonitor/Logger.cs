using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ActivityMonitor
{
    class Logger : ActivityMonitor.ILogger
    {
        private List<String> _log = new List<string>();

        public void Add(string message)
        {
            // Taken from http://heifner.blogspot.com/2006/12/logging-method-name-in-c.html
            // Start one up so that we don't get the current method but the one that called this one
            StackFrame sf = new StackFrame(1, true);
            System.Reflection.MethodBase mb = sf.GetMethod();
            string methodName = mb != null ? mb.Name : "ERROR DETERMINING CALLING METHOD NAME";
            // note to self: not sure if i need the data captured below this point
            // filename can be null, if unable to determine
            string filename = sf.GetFileName();
            // we only want the filename, not the complete path
            if (filename != null)
                filename = filename.Substring(filename.LastIndexOf('\\') + 1);
            int lineNumber = sf.GetFileLineNumber();

            _log.Add(DateTime.Now.ToString() + "  :  " + filename + "  :  " + methodName + "  :  " + lineNumber + "  :  " + message);
        }

        public void Write()
        {
            WriteToLogFile();
            // Send a email with the (possibly formatted?) contents of _log
        }

        private void WriteToLogFile()
        {
            // StreamWriter _stream = new StreamWriter(DateTime.Now.ToString() + "_log.txt");
            StreamWriter _stream = new StreamWriter("_log.txt");
            _stream.WriteLine("Log file generated at: " + DateTime.Now.ToString());
            foreach (string _logEntry in _log)
            {
                _stream.WriteLine(_logEntry.ToString());
            }
            _stream.Close();
        }
    }
}
