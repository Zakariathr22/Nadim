using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.Views.Controls
{
    class NewButton : Button
    {
        public NewButton()

        {
            this.DefaultStyleKey = typeof(Button);
            this.ProtectedCursor = InputSystemCursor.Create(InputSystemCursorShape.Hand);
        }

        public void ChangeCursor(InputCursor cursor)

        {
            this.ProtectedCursor = cursor;
        }

    }
}
