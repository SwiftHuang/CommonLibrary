using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace hwj.CommonLibrary.Object.Email
{
    public class StreamFile
    {
        public StreamFile() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="StreamFile"/> class,default translate into GzipStream .
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="inStream">The in stream.</param>
        public StreamFile(string fileName, Stream inStream)
        {
            new StreamFile(fileName, inStream, true);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamFile"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="inStream">The in stream.</param>
        /// <param name="useGzip">if set to <c>true</c> [use gzip].</param>
        public StreamFile(string fileName, Stream inStream, bool useGzip)
        {
            this.FileName = fileName;
            this.InStream = inStream;
            this.UseGzip = useGzip;
        }

        /// <summary>
        /// Get or Set the value to control either use Gzip to compress the stream or not
        /// 
        /// </summary>
        public bool UseGzip { get; set; }
        public Stream InStream { get; set; }
        public string FileName { get; set; }
    }
}
