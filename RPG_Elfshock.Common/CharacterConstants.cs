﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Elfshock.Common
{
    public static class CharacterConstants
    {
        public static readonly string[] characterClassesNames = { "Mage", "Warrior", "Archer" };

        public static readonly char[] pcEntitySymbol = { '*', '#', '@' };

        public static string symbolInvalidMsg = "This symbol is invalid!";

        public static string classInvalidMsg = "This class name is invalid!";
    }
}
