using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Basics.Services
{
    public class ApplicationService : IApplicationService
    {
        //---------------------------------------------------------------------
        #region Fields

        private Dispatcher  m_dispatcher = Application.Current.Dispatcher;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region IApplicationService Members

        public void ShowErrorMessage( string a_errorText )
        {
            //-- Show the error information
            MessageBox.Show( a_errorText, "Error", MessageBoxButton.OK, MessageBoxImage.Error );
        }

        [DebuggerStepThrough]
        public void ShowErrorMessage( Exception a_exception )
        {
            this.ShowErrorMessage( a_exception.Message );
        }

        public Dispatcher UserInterfaceDispatcher
        {
            get { return m_dispatcher; } }

        //-- IApplicationService Members
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------