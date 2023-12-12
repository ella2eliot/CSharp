using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SYS.Utilities.Dialog
{
    public class DialogHelper
    {
        /// <summary>
        /// Winform OK dialog
        /// </summary>
        /// <param name="message"></param>
        public static void ShowOKDialog(string message)
        {
            MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Winform Confirm dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cpation"></param>
        /// <returns></returns>
        public static DialogResult ShowYesNoDialog(string message, string cpation)
        {
            return MessageBox.Show(message, cpation, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        /// <summary>
        /// Winform Error dialog
        /// </summary>
        /// <param name="message"></param>
        public static void ShowErrorDialog (string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
