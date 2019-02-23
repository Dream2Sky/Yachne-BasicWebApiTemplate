using System;
using System.Collections.Generic;
using System.Text;

namespace Yachne.Terminal
{
    public interface ITerminalProvider
    {
        string ExecuteCommand(string command, params string[] args);
    }
}
