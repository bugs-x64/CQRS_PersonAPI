using System;
using System.IO;
using System.Text;

namespace Persons.Service.Extensions
{
    /// <summary>
    /// Расширения для <see cref="Stream"/>.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Читает поток в строку.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ReadToString(this Stream stream, Encoding encoding)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            var length = stream.Length;
            var data = new byte[length];

            stream.Read(data, 0, (int)length);

            stream.Position = 0;

            return encoding.GetString(data);
        }
    }
}