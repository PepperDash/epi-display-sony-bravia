﻿using System;
using System.Linq;
using PepperDash.Core;

namespace SonyBraviaEpi
{
    public static class Rs232ParsingUtils
    {
        private const byte Header = 0x70;

        public static bool ParsePowerResponse(this byte[] response, out bool power)
        {
            // TODO [ ] actually add in parsing
            Debug.Console(DebugLevels.DebugLevel, "ParsePowerResponse response: {0}", response.ToReadableString());

            if (response[2] == 0x00)
            {
                power = response[3] == 0x01;
                return true;
            }

            power = false;
            return false;
        }

        public static bool ParseInputResponse(this byte[] response, out string input)
        {
            // TODO [ ] actually add in parsing
            Debug.Console(DebugLevels.DebugLevel, "ParseInputResponse response: {0}", response.ToReadableString());

            //if (response[2] == 0x02)
            //{
            //      input = "";
            //      return true;
            //}

            input = "";
            return false;
        }

        public static bool IsComplete(this byte[] message)
        {
            var returnDataSize = message[2];
            var totalDataSize = returnDataSize + 3;
            return message.Length == totalDataSize;
        }

        public static bool ContainsHeader(this byte[] bytes)
        {
            return bytes.Any(IsHeader());
        }

        public static int NumberOfHeaders(this byte[] bytes)
        {
            return bytes.Count(IsHeader());
        }

        public static int FirstHeaderIndex(this byte[] bytes)
        {
            return bytes.ToList().IndexOf(Header);
        }

        private static byte[] GetFirstMessageWithMultipleHeaders(this byte[] bytes)
        {
            // any less than 3-bytes, we don't have a complete message
            if (bytes.Length < 3) return bytes;

            var secondHeaderIndex = bytes.ToList().FindIndex(1, IsHeader().ToPredicate());            

            // ex. 0x70,0x00,0x70 (valid ACK response) - skip to byte[3]
            if ((bytes[0] + bytes[1] == bytes[2]) && (bytes[2] == Header)) secondHeaderIndex++;

            if (secondHeaderIndex <= 0) secondHeaderIndex = bytes.Length;

            return bytes.Take(secondHeaderIndex).ToArray();
        }

        public static byte[] GetFirstMessage(this byte[] bytes)
        {
            return (bytes.NumberOfHeaders() <= 1) ? bytes : bytes.GetFirstMessageWithMultipleHeaders();
        }

        public static byte[] CleanToFirstHeader(this byte[] bytes)
        {
            var firstHeaderIndex = bytes.FirstHeaderIndex();
            return bytes.Skip(firstHeaderIndex).ToArray();
        }

        public static byte[] CleanOutFirstMessage(this byte[] bytes)
        {
            // any less than 3-bytes, we don't have a complete message
            if (bytes.Length < 3) return bytes;

            var secondHeaderIndex = bytes.ToList().FindIndex(1, IsHeader().ToPredicate());
            
            // ex. 0x70,0x00,0x70 (valid ACK response) - skip to byte[3]
            if ((bytes[0] + bytes[1] == bytes[2]) && (bytes[2] == Header)) secondHeaderIndex++;

            if (secondHeaderIndex <= 0) secondHeaderIndex = bytes.Length;

            return bytes.Skip(secondHeaderIndex).ToArray();
        }

        public static string ToReadableString(this byte[] bytes)
        {
            return BitConverter.ToString(bytes);
        }

        private static Func<byte, bool> IsHeader()
        {
            const byte header = Header;
            return t => t == header;
        }

        private static Predicate<T> ToPredicate<T>(this Func<T, bool> func)
        {
            return new Predicate<T>(func);
        }
    }
}