using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Logging
{
    /// <summary>
    /// Define logging interface.
    /// </summary>
    public interface ILogging
    {
        /// <summary>
        /// Logging message with color.
        /// </summary>
        /// <param name="message">Message to logging.</param>
        /// <param name="color">Logging text's color.</param>
        void WriteMessage(string message, ConsoleColor color);

        /// <summary>
        /// Logging message with default green color.
        /// </summary>
        /// <param name="message">Message to logging.</param>
        void WriteSuccessMessage(string message);

        /// <summary>
        /// Logging message with default red color.
        /// </summary>
        /// <param name="message">Message to logging.</param>
        void WriteErrorMessage(string message);

        /// <summary>
        /// Logging message with default color.
        /// </summary>
        /// <param name="message">Message to logging.</param>
        void WriteMessage(string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        void WriteMessage(Exception ex);
    }
}
