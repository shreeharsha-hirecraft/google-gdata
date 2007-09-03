/* Copyright (c) 2006 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/
#region Using directives

#define USE_TRACING

using System;
using System.Net;
using System.IO;
using System.Xml;
using Google.GData.Client;

#endregion

//////////////////////////////////////////////////////////////////////
// <summary>contains Service, the base interface that 
//   allows to query a service for different feeds
//  </summary>
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>base Service interface definition
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public interface IService 
    {
        /// <summary>get/set for credentials to the service calls. Gets passed through to GDatarequest</summary> 
        GDataCredentials Credentials 
        {
            get;
            set;
        }
        /// <summary>get/set for the GDataRequestFactory object to use</summary> 
        IGDataRequestFactory RequestFactory
        {
            get;
            set;
        }
        /// <summary>the minimal Get OpenSearchRssDescription function</summary> 
        Stream QueryOpenSearchRssDescription(Uri serviceUri);

        /// <summary>the minimal query implementation</summary> 
        AtomFeed Query(FeedQuery feedQuery);
        /// <summary>simple update for atom resources</summary> 
        AtomEntry Update(AtomEntry entry);
        /// <summary>simple insert for atom entries, based on a feed</summary> 
        AtomEntry Insert(AtomFeed feed, AtomEntry entry);
        /// <summary>delete an entry</summary> 
        void Delete(AtomEntry entry);
        /// <summary>batch operation, posting of a set of entries</summary>
        AtomFeed Batch(AtomFeed feed, Uri batchUri); 
        /// <summary>simple update for media resources</summary> 
        AtomEntry Update(Uri uriTarget, Stream input, string contentType);
        /// <summary>simple insert for media resources</summary> 
//        AtomEntry Insert(Uri uriTarget, Stream input, string contentType);
    }


    //////////////////////////////////////////////////////////////////////
    /// <summary>the one that creates GDatarequests on the service
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public interface IGDataRequestFactory
    {
        /// <summary>creation method for GDatarequests</summary> 
        IGDataRequest CreateRequest(GDataRequestType type, Uri uriTarget); 
        /// <summary>set wether or not to use gzip for new requests</summary>
        bool    UseGZip
        {
            get;
            set;
        }
    }
    //////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////
    /// <summary>enum to describe the different operations on the GDataRequest
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public enum GDataRequestType
    {
        /// <summary>The request is used for query</summary>
        Query,                       
        /// <summary>The request is used for an insert</summary>
        Insert,                        
        /// <summary>The request is used for an update</summary>
        Update,                    
        /// <summary>The request is used for a delete</summary>
        Delete, 
        /// <summary>This request is used for a batch operation</summary>
        Batch
    }
    /////////////////////////////////////////////////////////////////////////////

    

    //////////////////////////////////////////////////////////////////////
    /// <summary>Thin layer to abstract the request/response
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public interface IGDataRequest 
    {
        /// <summary>get/set for credentials to the service calls. Get's passed through to GDatarequest</summary> 
        GDataCredentials Credentials 
        {
            get;
            set;
        }
        /// <summary>set wether or not to use gzip for this request</summary>
        bool UseGZip
        {
            get;
            set;
        }
        /// <summary>get's the request stream to write into</summary> 
        Stream GetRequestStream();
        /// <summary>Executes the request</summary> 
        void   Execute();
        /// <summary>get's the response stream to read from</summary> 
        Stream GetResponseStream();
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Thin layer to create an action on an item/response
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public interface IBaseWalkerAction
    {
        /// <summary>the only relevant method here</summary> 
        bool Go(AtomBase atom);
    }


    //////////////////////////////////////////////////////////////////////
    /// <summary>if an extension element is created and wants to persist itself,
    /// it needs to implement this interface
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public interface IExtensionElement
    {
        /// <summary>the only relevant method here</summary> 
        void Save(XmlWriter writer);
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>if an extension element want's to use the new parsing method
    /// it needs to implement this interface
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public interface IExtensionElementFactory
    {
        /// <summary>the only relevant method here</summary> 
        string XmlName { get;}
        string XmlNameSpace { get;}
        string XmlPrefix { get;}
        IExtensionElement CreateInstance(XmlNode node, AtomFeedParser parser); 
    }






} 
/////////////////////////////////////////////////////////////////////////////
