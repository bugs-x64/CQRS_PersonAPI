using System;
using System.IO;

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
        public static string ReadToString(this Stream stream)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            var length = stream.Length;
            var data = new byte[length];

            stream.Read(data, 0, (int)length);

            stream.Position = 0;

            //todo по-хорошему надо перегрузку с выбором кодировки.
            return System.Text.Encoding.Default.GetString(data);
        }
    }
}