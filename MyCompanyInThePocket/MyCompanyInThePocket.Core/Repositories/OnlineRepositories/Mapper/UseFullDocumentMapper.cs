﻿using System;
using System.Collections.Generic;
using MyCompanyInThePocket.Core.Models;

namespace MyCompanyInThePocket.Core.Repositories.OnlineRepositories
{
    internal static class UseFullDocumentMapper
    {
        internal static List<UseFullDocument> MapDocuments(this RootObject documents)
		{

            var dbDocuments = new List<UseFullDocument>();

            if (documents.value != null)
            {
                foreach (var document in documents.value)
                {
                    var dbMeeting = new UseFullDocument();
                    dbMeeting.Created = document.Created;
                    dbMeeting.Modified = document.Modified;
                    dbMeeting.Id = document.Id;
                    dbMeeting.Name = document.URL.Description;
                    dbMeeting.Url = document.URL.Url;

                    dbDocuments.Add(dbMeeting);
                }
            }

			return dbDocuments;
		}

		public class URL
		{
			public string Description { get; set; }
			public string Url { get; set; }
		}

		public class Value
		{

			public int FileSystemObjectType { get; set; }
			public int Id { get; set; }
			public object ServerRedirectedEmbedUri { get; set; }
			public string ServerRedirectedEmbedUrl { get; set; }
			public int ID { get; set; }
			public string ContentTypeId { get; set; }
			public string Modified { get; set; }
			public string Created { get; set; }
			public int AuthorId { get; set; }
			public int EditorId { get; set; }
			public string OData__UIVersionString { get; set; }
			public bool Attachments { get; set; }
			public string GUID { get; set; }
			public URL URL { get; set; }
			public object Comments { get; set; }
		}

		public class RootObject
		{
			public List<Value> value { get; set; }
		}
    }


}
