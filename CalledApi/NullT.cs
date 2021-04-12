﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CalledApi
{
    public static class NullT
    {
        public static bool IsNull<T>(this T subject)
        {
            return ReferenceEquals(subject, null);
        }
    }
}
