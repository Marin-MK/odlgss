﻿using System;
using System.Collections.Generic;
using RubyDotNET;

namespace odlgss
{
    public class Color : RubyObject
    {
        public ODL.Color ColorObject;
        public static IntPtr ClassPointer;
        public static Dictionary<IntPtr, Color> Colors = new Dictionary<IntPtr, Color>();

        public static Class CreateClass()
        {
            Class c = new Class("Color");
            ClassPointer = c.Pointer;
            c.DefineClassMethod("new", New);
            c.DefineMethod("initialize", initialize);
            c.DefineMethod("red", redget);
            c.DefineMethod("red=", redset);
            c.DefineMethod("green", greenget);
            c.DefineMethod("green=", greenset);
            c.DefineMethod("blue", blueget);
            c.DefineMethod("blue=", blueset);
            c.DefineMethod("alpha", alphaget);
            c.DefineMethod("alpha=", alphaset);
            return c;
        }

        private Color()
        {
            this.Pointer = Internal.rb_funcallv(ClassPointer, Internal.rb_intern("allocate"), 0);
            Colors[Pointer] = this;
        }

        public static Color New(byte Red, byte Green, byte Blue, byte Alpha = 255)
        {
            IntPtr ptr = Internal.rb_funcallv(ClassPointer, Internal.rb_intern("new"), 4,
                Internal.LONG2NUM(Red),
                Internal.LONG2NUM(Green),
                Internal.LONG2NUM(Blue),
                Internal.LONG2NUM(Alpha)
            );
            return Colors[ptr];
        }
        public void Initialize(byte Red, byte Green, byte Blue, byte Alpha = 255)
        {
            ColorObject = new ODL.Color(Red, Green, Blue, Alpha);
            this.Red = Red;
            this.Green = Green;
            this.Blue = Blue;
            this.Alpha = Alpha;
        }

        public byte Red
        {
            get
            {
                return (byte) Internal.NUM2LONG(Internal.rb_funcallv(Pointer, Internal.rb_intern("red"), 0));
            }
            set
            {
                Internal.rb_funcallv(Pointer, Internal.rb_intern("red="), 1, Internal.LONG2NUM(value));
            }
        }
        public byte Green
        {
            get
            {
                return (byte) Internal.NUM2LONG(Internal.rb_funcallv(Pointer, Internal.rb_intern("green"), 0));
            }
            set
            {
                Internal.rb_funcallv(Pointer, Internal.rb_intern("green="), 1, Internal.LONG2NUM(value));
            }
        }
        public byte Blue
        {
            get
            {
                return (byte) Internal.NUM2LONG(Internal.rb_funcallv(Pointer, Internal.rb_intern("blue"), 0));
            }
            set
            {
                Internal.rb_funcallv(Pointer, Internal.rb_intern("blue="), 1, Internal.LONG2NUM(value));
            }
        }
        public byte Alpha
        {
            get
            {
                return (byte) Internal.NUM2LONG(Internal.rb_funcallv(Pointer, Internal.rb_intern("alpha"), 0));
            }
            set
            {
                Internal.rb_funcallv(Pointer, Internal.rb_intern("alpha="), 1, Internal.LONG2NUM(value));
            }
        }

        static IntPtr New(IntPtr _self, IntPtr _args)
        {
            Color c = new Color();
            RubyArray Args = new RubyArray(_args);
            IntPtr[] newargs = new IntPtr[Args.Length];
            for (int i = 0; i < Args.Length; i++) newargs[i] = Args[i].Pointer;
            Internal.rb_funcallv(c.Pointer, Internal.rb_intern("initialize"), Args.Length, newargs);
            return c.Pointer;
        }
        static IntPtr initialize(IntPtr _self, IntPtr _args)
        {
            RubyArray Args = new RubyArray(_args);

            Color c = Colors[_self];
            if (Args.Length == 3 || Args.Length == 4)
            {
                int r = (int) Internal.NUM2LONG(Internal.rb_funcallv(Args[0].Pointer, Internal.rb_intern("to_i"), 0));
                int g = (int) Internal.NUM2LONG(Internal.rb_funcallv(Args[1].Pointer, Internal.rb_intern("to_i"), 0));
                int b = (int) Internal.NUM2LONG(Internal.rb_funcallv(Args[2].Pointer, Internal.rb_intern("to_i"), 0));
                int a = 255;
                if (Args.Length == 4) a = (int) Internal.NUM2LONG(Internal.rb_funcallv(Args[3].Pointer, Internal.rb_intern("to_i"), 0));
                c.Initialize((byte) r, (byte) g, (byte) b, (byte) a);
            }
            else
            {
                ScanArgs(3, Args);
            }

            return _self;
        }

        static IntPtr redget(IntPtr _self, IntPtr _args)
        {
            return Internal.rb_ivar_get(_self, Internal.rb_intern("@red"));
        }
        static IntPtr redset(IntPtr _self, IntPtr _args)
        {
            RubyArray Args = new RubyArray(_args);
            ScanArgs(1, Args);
            Colors[_self].ColorObject.Red = (byte) Internal.NUM2LONG(Args[0].Pointer);
            return Internal.rb_ivar_set(_self, Internal.rb_intern("@red"), Args[0].Pointer);
        }

        static IntPtr greenget(IntPtr _self, IntPtr _args)
        {
            return Internal.rb_ivar_get(_self, Internal.rb_intern("@green"));
        }
        static IntPtr greenset(IntPtr _self, IntPtr _args)
        {
            RubyArray Args = new RubyArray(_args);
            ScanArgs(1, Args);
            Colors[_self].ColorObject.Green = (byte) Internal.NUM2LONG(Args[0].Pointer);
            return Internal.rb_ivar_set(_self, Internal.rb_intern("@green"), Args[0].Pointer);
        }

        static IntPtr blueget(IntPtr _self, IntPtr _args)
        {
            return Internal.rb_ivar_get(_self, Internal.rb_intern("@blue"));
        }
        static IntPtr blueset(IntPtr _self, IntPtr _args)
        {
            RubyArray Args = new RubyArray(_args);
            ScanArgs(1, Args);
            Colors[_self].ColorObject.Blue = (byte) Internal.NUM2LONG(Args[0].Pointer);
            return Internal.rb_ivar_set(_self, Internal.rb_intern("@blue"), Args[0].Pointer);
        }

        static IntPtr alphaget(IntPtr _self, IntPtr _args)
        {
            return Internal.rb_ivar_get(_self, Internal.rb_intern("@alpha"));
        }
        static IntPtr alphaset(IntPtr _self, IntPtr _args)
        {
            RubyArray Args = new RubyArray(_args);
            ScanArgs(1, Args);
            Colors[_self].ColorObject.Alpha = (byte) Internal.NUM2LONG(Args[0].Pointer);
            return Internal.rb_ivar_set(_self, Internal.rb_intern("@alpha"), Args[0].Pointer);
        }
    }
}
