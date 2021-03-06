﻿using FileService.Interfaces;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace FileService.Services
{
    /// <summary>
    /// Implements an <see cref="IFileUtility"/> that reads from and writes contents to a file on disk.
    /// </summary>
    public class DiskFileUtility : IFileUtility
    {
        /// <summary>
        /// Reads the contents of a provided file on disk.
        /// </summary>
        /// <param name="filePathSource">The directory path name of the file on disk.</param>
        /// <returns>The contents of the file.</returns>
        public async Task<string> ReadFromFile(string filePathSource)
        {
            using (StreamReader streamReader = new StreamReader(filePathSource))
            {
                return await streamReader.ReadToEndAsync();
            }
        }

        /// <summary>
        /// Reads contents of a file from a http source 
        /// </summary>
        /// <param name="requestMessage">The Http Request message.</param>
        /// <returns></returns>
        public Task<string> ReadFromDocument(HttpRequestMessage requestMessage)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Writes contents to a provided file on disk.
        /// </summary>
        /// <param name="fileContents">The string content to be written.</param>
        /// <param name="filePathSource">The directory path name of the file on disk.</param>
        /// <returns></returns>
        public async Task WriteToFile(string fileContents, string filePathSource)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePathSource))
            {
                await streamWriter.WriteLineAsync(fileContents);
            }
        }
    }
}
