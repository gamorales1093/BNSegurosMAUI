using System;
using BNSegurosMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;

namespace BNSegurosMAUI.Helpers
{
    public class SessionInfo
    {
        #region Variables
        public static SessionInfo instance = null;
        public Notice NoticeModel { get; set; }
        public Insurance InsuranceModel { get; set; }
        public bool FromInsuranceDetail { get; set; }
        public bool FromFAQPage { get; set; }
        #endregion

        #region Contructor
        public SessionInfo()
        {
        }
        #endregion

        #region Instance
        public static SessionInfo GetInstance()
        {
            if (SessionInfo.instance == null)
                SessionInfo.instance = new SessionInfo();

            return SessionInfo.instance;
        }
        #endregion
    }

    public static class ResourceHelper
    {

        public static object FindResource(this VisualElement o, string key)
        {
            while (o != null)
            {
                if (o.Resources.TryGetValue(key, out var r1)) return r1;
                if (o is Page) break;
                if (o is IElement e) o = e.Parent as VisualElement;
            }
            if (Application.Current.Resources.TryGetValue(key, out var r2)) return r2;
            return null;
        }
    }

    /// Represents to add expander items to scroll view.
    /// </summary>
#if NET8_0_OR_GREATER && (IOS || MACCATALYST)
    internal class ExpanderScrollView : ScrollView, ICrossPlatformLayout
#else
    internal class ExpanderScrollView : ScrollView
#endif  
    {
        public ExpanderScrollView()
        {

        }

        #region Public Methods

#if NET8_0_OR_GREATER && (IOS || MACCATALYST)
        /// <summary>
        /// Override this method due to .net8 blank issue.
        /// </summary>
        /// <param name="bounds">Bounds for expander.</param>
        /// <returns>Size of scroll view.</returns>
        Size ICrossPlatformLayout.CrossPlatformArrange(Microsoft.Maui.Graphics.Rect bounds)
        {
            // 855697 - Checked, from where expander bounds changes and override this method.
            // from ScrollViewHandler ICrossPlatformLayout.CrossPlatformArrange native bounds used instead of frame.
            if (this is IScrollView scrollView)
            {
                bounds.X = 0;
                bounds.Y = 0;
                return scrollView.ArrangeContentUnbounded(bounds);
            }

            return bounds.Size;
        }
#endif

        #endregion
    }

}