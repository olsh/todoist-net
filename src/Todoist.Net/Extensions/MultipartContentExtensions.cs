using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

using Todoist.Net.Models;

namespace Todoist.Net.Extensions
{
    internal static class MultipartFormDataContentExtensions
    {
        public static MultipartFormDataContent AddStringParts(this MultipartFormDataContent multipartContent, IDictionary<string, string> stringParams)
        {
            stringParams = stringParams ?? new Dictionary<string, string>();
            foreach (var kvp in stringParams)
            {
                multipartContent.Add(new StringContent(kvp.Value), $"\"{kvp.Key}\"");
            }
            return multipartContent;
        }

        public static MultipartFormDataContent AddFileParts(this MultipartFormDataContent multipartContent, string key, params UploadFile[] files)
        {
            files = files ?? Array.Empty<UploadFile>();
            foreach (var file in files)
            {
                var contentStream = file.ContentStream;
                if (contentStream.CanSeek)
                {
                    // The same file may be sent more than once, e.g. when a request is retried,
                    // so the stream is rewound instead of being read from wherever it was left.
                    contentStream.Seek(0, SeekOrigin.Begin);
                }

                var content = new NonDisposingStreamContent(contentStream);
                if (file.MimeType != null && MediaTypeHeaderValue.TryParse(file.MimeType, out var mediaType))
                {
                    content.Headers.ContentType = mediaType;
                }
                multipartContent.Add(content, key, file.Filename);
            }
            return multipartContent;
        }


        /// <summary>
        /// A <see cref="StreamContent" /> which leaves the underlying stream open once disposed.
        /// </summary>
        /// <remarks>
        /// The stream belongs to the <see cref="UploadFile" /> owned by the caller, so it has to outlive
        /// both the content and the request the content is sent with.
        /// </remarks>
        private sealed class NonDisposingStreamContent : StreamContent
        {
            public NonDisposingStreamContent(Stream content)
                : base(content)
            {
            }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(false);
            }
        }
    }
}
