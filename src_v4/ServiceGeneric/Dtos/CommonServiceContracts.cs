﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Northwind.Data.Dtos
{
    #region Get Request/Response
  
    public abstract partial class GetRequest
    {
        protected GetRequest()
        {
            RCache = 0;
            Include = string.Empty;
            Select = string.Empty;
        }

        public int RCache { get; set; } //Used by LLBLGen V4 template only
        public string Include { get; set; }
        public string Select { get; set; }
    }

    public abstract partial class GetRequest<TDto, TResponse> : GetRequest, IReturn<TResponse>
        where TResponse : GetResponse<TDto>
    {
        protected GetRequest(): base()
        {
        }
    }

    public abstract partial class GetCollectionRequest
    {
        protected GetCollectionRequest()
        {
            RCache = 0;
            PageNumber = 0;
            PageSize = 0;
            Limit = 0;
            Include = string.Empty;
            Sort = string.Empty;
            Select = string.Empty;
            Filter = string.Empty;
            Relations = string.Empty;
        }
        
        public int RCache { get; set; } //Used by LLBLGen V4 template only
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Limit { get; set; }
        public string Include { get; set; }
        public string Sort { get; set; }
        public string Select { get; set; }
        public string Filter { get; set; }
        public string Relations { get; set; }
    }

    public abstract partial class GetCollectionRequest<TDto, TResponse> : GetCollectionRequest, IReturn<TResponse>
        where TResponse: GetCollectionResponse<TDto>
    {
        protected GetCollectionRequest()
            : base()
        {
        }
    }

    public abstract partial class GetResponse<TDto>
    {
        protected GetResponse()
            : this(default(TDto))
        {
        }

        protected GetResponse(TDto dto)
        {
            Result = dto;
        }

        public TDto Result { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
    
    public abstract partial class GetCollectionResponse<TDto>
    {
        protected GetCollectionResponse()
        {
        }

        protected GetCollectionResponse(IEnumerable<TDto> collection, int pageNumber, int pageSize, int totalItemCount): this()
        {            
            var pagedList = new StaticPagedList<TDto>(collection, pageNumber, pageSize, totalItemCount);
            Result = pagedList.Subset;
            Paging = new PagingDetails(pagedList);
        }

        public List<TDto> Result { get; set; }

        public PagingDetails Paging { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
  
    #endregion

    #region DataTable Response
  
    public partial class DataTableResponse
    {
        public DataTableResponse()
        {
            sEcho = string.Empty;
            iTotalRecords = 0;
            iTotalDisplayRecords = 0;
            aaData = new List< string[] >();    
        }

        public string sEcho { get; set; }
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public List< string[] > aaData { get; set; } 
    } 
  
    #endregion

    #region Simple Request/Response
  
    public class SimpleRequest<TResponse> : GetRequest<TResponse, SimpleResponse<TResponse>>
    {
    }

    public class SimpleResponse<TDto> : GetResponse<TDto>
    {
    }
  
    #endregion

    #region Miscellaneous
  
    public partial class EntityMetaDetailsResponse
    {
        public Link[] Fields { get; set; }
        public Link[] Includes { get; set; }
        public Link[] Relations { get; set; }
    }
  
    public partial class Link
    {
        public Link()
        {
            Properties = new Dictionary<string, string>();
        }

        public string Id { get; set; }
        public string Type { get; set; }
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Properties { get; set; }

        public static Link Create(string id = null, string type = null, string rel = null, string href = null, string description = null, IDictionary<string, string> properties = null)
        {
            return new Link() { Id = id, Type = type, Rel = rel, Href = href, Description = description, 
                Properties = properties == null ? new Dictionary<string, string>() : new Dictionary<string, string>(properties)};
        }
    }
  
    #endregion
}
