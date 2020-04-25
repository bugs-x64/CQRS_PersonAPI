using System;
using System.IO;

namespace Persons.Service.Extensions
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Читает поток в строку.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ReadToString(this Stream stream)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            var length = stream.Length;
            var data = new byte[length];

            stream.Read(data, 0, (int)length);

            stream.Position = 0;

            return System.Text.Encoding.Default.GetString(data);
        }
    }
}