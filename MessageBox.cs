﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static odl.SDL2.SDL;

namespace peridot
{
    public class MessageBox
    {
        public odl.Window Parent;
        public string Title;
        public string Message;
        public int IconType;
        public List<string> Buttons;

        public MessageBox(odl.Window Parent, string Title, string Message, int IconType, List<string> Buttons)
        {
            this.Parent = Parent;
            this.Title = Title;
            this.Message = Message;
            this.IconType = IconType;
            this.Buttons = Buttons;
        }

        public unsafe int Show()
        {
            SDL_MessageBoxData boxdata = new SDL_MessageBoxData();
            if (this.IconType == 1) boxdata.flags = SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION;
            else if (this.IconType == 2) boxdata.flags = SDL_MessageBoxFlags.SDL_MESSAGEBOX_WARNING;
            else if (this.IconType == 3) boxdata.flags = SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR;
            SDL_MessageBoxButtonData[] buttons = new SDL_MessageBoxButtonData[Buttons.Count];
            for (int i = 0; i < Buttons.Count; i++)
            {
                buttons[i] = new SDL_MessageBoxButtonData();
                buttons[i].text = StrToPtr(Buttons[i]);
                buttons[i].buttonid = 0;
                if (i == 0) buttons[i].flags = SDL_MessageBoxButtonFlags.SDL_MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT;
                else if (i == Buttons.Count - 1) buttons[i].flags = SDL_MessageBoxButtonFlags.SDL_MESSAGEBOX_BUTTON_ESCAPEKEY_DEFAULT;
            }
            boxdata.message = this.Message;
            boxdata.numbuttons = Buttons.Count;
            boxdata.title = this.Title;
            boxdata.window = this.Parent == null ? IntPtr.Zero : this.Parent.SDL_Window;
            int result = -1;
            fixed (SDL_MessageBoxButtonData* buttonptr = &buttons[0])
            {
                boxdata.buttons = (IntPtr) buttonptr;
            }
            SDL_ShowMessageBox(ref boxdata, out result);
            return result;
        }
    }

    public class StandardBox : MessageBox
    {
        public StandardBox(odl.Window Parent, string Message)
            : base(Parent, Parent == null ? "peridot" : Parent.Text, Message, 0, new List<string>() { "OK" })
        {

        }
    }

    public class InfoBox : MessageBox
    {
        public InfoBox(odl.Window Parent, string Message)
            : base(Parent, "Info", Message, 1, new List<string>() { "OK" })
        {

        }
    }

    public class WarningBox : MessageBox
    {
        public WarningBox(odl.Window Parent, string Message)
            : base(Parent, "Warning", Message, 2, new List<string>() { "OK" })
        {

        }
    }

    public class ErrorBox : MessageBox
    {
        public ErrorBox(odl.Window Parent, string Message)
            : base(Parent, "Error", Message, 3, new List<string>() { "OK" })
        {

        }
    }
}
