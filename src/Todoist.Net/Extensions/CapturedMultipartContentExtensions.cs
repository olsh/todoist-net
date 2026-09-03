using System;
using System.Collections.Generic;
using System.IO;

using Flurl.Http.Content;

using Todoist.Net.Models;

namespace Todoist.Net.Extensions
{
    internal static class CapturedMultipartContentExtensions
    {
        public static CapturedMultipartContent AddStringParts(this CapturedMultipartContent multipartContent, IDictionary<string, string> stringParams)
        {
            stringParams = stringParams ?? new Dictionary<string, string>();
            foreach (var kvp in stringParams)
            {
                multipartContent.AddString(kvp.Key, kvp.Value);
            }
            return multipartContent;
        }

        public static CapturedMultipartContent AddFileParts(this CapturedMultipartContent multipartContent, string key, params UploadFile[] files)
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
                multipartContent.AddFile(key, file.ContentStream, file.Filename, file.MimeType);
            }
            return multipartContent;
        }
    }
}
