#if !LOCALTEST

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace System
{
    public struct Char : IComparable, IComparable<char>, IEquatable<char>
    {

        // Note that this array must be ordered, because binary searching is used on it.
        internal static readonly char[] WhiteChars = {
            (char) 0x9, (char) 0xA, (char) 0xB, (char) 0xC, (char) 0xD,
            (char) 0x85, (char) 0x1680, (char) 0x2028, (char) 0x2029,
            (char) 0x20, (char) 0xA0, (char) 0x2000, (char) 0x2001,
            (char) 0x2002, (char) 0x2003, (char) 0x2004, (char) 0x2005,
            (char) 0x2006, (char) 0x2007, (char) 0x2008, (char) 0x2009,
            (char) 0x200A, (char) 0x200B, (char) 0x3000, (char) 0xFEFF };


        internal char m_value;

        public const char MinValue='\0';
        public const char MaxValue = '\xffff';

        public override string ToString()
        {
            return new string(m_value, 1);
        }

        public override bool Equals(object obj)
        {
            return (obj is char && ((char)obj).m_value == this.m_value);
        }

        public override int GetHashCode()
        {
            return (int)this.m_value;
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern static public UnicodeCategory GetUnicodeCategory(char c);

        public static bool IsSurrogatePair(string s, int index)
        {
       //     CheckParameter(s, index);
            return (((index + 1) < s.Length) && IsSurrogatePair(s[index], s[index + 1]));
        }




        public static bool IsSurrogatePair(char highSurrogate, char lowSurrogate)
        {
            return ((((55296 <= highSurrogate) && (highSurrogate <= 56319)) && (56320 <= lowSurrogate)) && (lowSurrogate <= 57343));
        }




        public static bool IsSurrogate(string s, int index)
        {
          
            return IsSurrogate(s[index]);
        }


        public static unsafe bool IsSurrogate(char c)
        {
            return GetUnicodeCategory(c) == UnicodeCategory.Surrogate;
        }

 

 

 


        public static UnicodeCategory GetUnicodeCategory(string str, int index)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            if (index < 0 || index >= str.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return GetUnicodeCategory(str[index]);
        }

        public static bool IsWhiteSpace(char c)
        {
            // TODO: Make this use Array.BinarySearch() when implemented
            for (int i = 0; i < WhiteChars.Length; i++)
            {
                if (WhiteChars[i] == c)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsWhiteSpace(string str, int index)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            if (index < 0 || index >= str.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return IsWhiteSpace(str[index]);
        }

        public static bool IsLetter(char c)
        {
            return GetUnicodeCategory(c) <= UnicodeCategory.OtherLetter;
        }

        public static bool IsLetterOrDigit(char c)
        {
            var gc = GetUnicodeCategory(c);
            if (gc <= UnicodeCategory.OtherLetter) return true;
            if (gc == UnicodeCategory.DecimalDigitNumber) return true;
            if (gc == UnicodeCategory.LetterNumber) return true;
            if (gc == UnicodeCategory.OtherNumber) return true;
            return false;
        }

        public static bool IsLetter(string str, int index)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            if (index < 0 || index >= str.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return IsLetter(str[index]);
        }

        public static bool IsDigit(char c)
        {
            return GetUnicodeCategory(c) == UnicodeCategory.DecimalDigitNumber;
        }

        public static bool IsDigit(string str, int index)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            if (index < 0 || index >= str.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return IsDigit(str[index]);
        }

        public static bool IsLower(char c)
        {
            return GetUnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
        }

        public static bool IsLower(string str, int index)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            if (index < 0 || index >= str.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return IsLower(str[index]);
        }

        public static bool IsUpper(char c)
        {
            return GetUnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
        }

        public static bool IsUpper(string str, int index)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            if (index < 0 || index >= str.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return IsUpper(str[index]);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern public static char ToLowerInvariant(char c);

        public static char ToLower(char c)
        {
            return ToLower(c, CultureInfo.CurrentCulture);
        }

        public static char ToLower(char c, CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }
            if (culture.LCID == 0x7f)
            {
                // Invariant culture
                return ToLowerInvariant(c);
            }
            return '?';
            //return culture.TextInfo.ToUpper(c);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        extern public static char ToUpperInvariant(char c);

        public static char ToUpper(char c)
        {
            return ToUpper(c, CultureInfo.CurrentCulture);
        }

        public static char ToUpper(char c, CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }
            if (culture.LCID == 0x7f)
            {
                // Invariant culture
                return ToUpperInvariant(c);
            }
            return '?';
            //return culture.TextInfo.ToUpper(c);
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            if (!(obj is char))
            {
                throw new ArgumentException();
            }
            return this.CompareTo((char)obj);
        }

        #endregion

        #region IComparable<char> Members

        public int CompareTo(char x)
        {
            return (this.m_value > x) ? 1 : ((this.m_value < x) ? -1 : 0);
        }

        #endregion

        #region IEquatable<char> Members

        public bool Equals(char x)
        {
            return this.m_value == x;
        }

        #endregion

    }
}

#endif
