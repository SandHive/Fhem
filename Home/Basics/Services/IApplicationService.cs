using System;
using System.Windows.Threading;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Basics.Services
{
    public interface IApplicationService
    {
        /// <summary>
        /// Shows an error message.
        /// </summary>
        /// <param name="a_exception">
        /// The exception whose message text should be shown.
        /// </param>
        void ShowErrorMessage( Exception a_exception );

        /// <summary>
        /// Shows an error message.
        /// </summary>
        /// <param name="a_errorText">
        /// The error text that should be shown.
        /// </param>
        void ShowErrorMessage( string a_errorText );

        /// <summary>
        /// Gets the dispatcher for invoking into the UI thread.
        /// </summary>
        Dispatcher UserInterfaceDispatcher { get; }
    }
}
//-----------------------------------------------------------------------------