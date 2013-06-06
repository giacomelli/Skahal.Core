#region Usings
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Mime;
using Skahal.Infrastructure.Framework.Net;


#endregion

namespace Skahal.Infrastructure.Mono.Net
{
	/// <summary>
	/// Submits post data to a url.
	/// </summary>
	public class PostSubmitter : IPostSubmitter
	{
		#region Fields	
	    private string m_url = string.Empty;
	    private NameValueCollection m_values = new NameValueCollection();
	    private PostType m_type = PostType.Get;
		#endregion
		
		#region Constructors
	    /// <summary>
	    /// Default constructor.
	    /// </summary>
	    public PostSubmitter()
	    {
			Timeout = 30000;
	    }
	
	    /// <summary>
	    /// Constructor that accepts a url as a parameter
	    /// </summary>
	    /// <param name="url">The url where the post will be submitted to.</param>
	    public PostSubmitter(string url)
	        : this()
	    {
	        m_url = url;
	    }
	
	    /// <summary>
	    /// Constructor allowing the setting of the url and items to post.
	    /// </summary>
	    /// <param name="url">the url for the post.</param>
	    /// <param name="values">The values for the post.</param>
	    public PostSubmitter(string url, NameValueCollection values)
	        : this(url)
	    {
	        m_values = values;
	    }
		#endregion
	
		#region Properties
	    /// <summary>
	    /// Gets or sets the url to submit the post to.
	    /// </summary>
	    public string Url
	    {
	        get
	        {
	            return m_url;
	        }
	        set
	        {
	            m_url = value;
	        }
	    }
	    /// <summary>
	    /// Gets or sets the name value collection of items to post.
	    /// </summary>
	    public NameValueCollection PostItems
	    {
	        get
	        {
	            return m_values;
	        }
	        set
	        {
	            m_values = value;
	        }
	    }
	    /// <summary>
	    /// Gets or sets the type of action to perform against the url.
	    /// </summary>
	    public PostType Type
	    {
	        get
	        {
	            return m_type;
	        }
	        set
	        {
	            m_type = value;
	        }
	    }
	
	    private CookieContainer m_cookieContainer = new CookieContainer();
	    public CookieContainer CookieContainer
	    {
	        get
	        {
	            return m_cookieContainer;
	        }
	
	        set
	        {
	            m_cookieContainer = value;
	        }
	    }
	
	    private WebHeaderCollection m_responseHeaders;
	    public WebHeaderCollection ResponseHeaders
	    {
	        get
	        {
	            return m_responseHeaders;
	        }
	    }
	
	    private string m_responseEncoding = "UTF-8";
	    public string ResponseEncoding
	    {
	        get
	        {
	            return m_responseEncoding;
	        }
	
	        set
	        {
	            m_responseEncoding = value;
	        }
	    }
	
	    private string m_referer;
	    public string Referer
	    {
	        get
	        {
	            return m_referer;
	        }
	
	        set
	        {
	            m_referer = value;
	        }
	    }
	
	    public bool m_readAttachmentFile;
	    public bool ReadAttachmentFile
	    {
	        get
	        {
	            return m_readAttachmentFile;
	        }
	
	        set
	        {
	            m_readAttachmentFile = value;
	        }
	    }

		/// <summary>
		/// Gets or sets the timeout in milliseconds.
		/// </summary>
		/// <value>
		/// The timeout.
		/// </value>
		public int Timeout { get; set; }
		#endregion
			
		#region Methods
	    /// <summary>
	    /// Posts the supplied data to specified url.
	    /// </summary>
	    /// <returns>a string containing the result of the post.</returns>
	    public string Post()
	    {
	        StringBuilder parameters = new StringBuilder();

			if(m_type == PostType.Json)
			{
				for (int i = 0; i < m_values.Count; i++)
				{
					EncodeAndAddItemForJson(ref parameters, m_values.GetKey(i), m_values[i]);
				}

				parameters.Insert(0, "{");
				parameters.Append("}");
			}
			else
			{
		        for (int i = 0; i < m_values.Count; i++)
		        {
		            EncodeAndAddItem(ref parameters, m_values.GetKey(i), m_values[i]);
				}
			}

	        return  PostData(m_url, parameters.ToString());
	    }
	    /// <summary>
	    /// Posts the supplied data to specified url.
	    /// </summary>
	    /// <param name="url">The url to post to.</param>
	    /// <returns>a string containing the result of the post.</returns>
	    public string Post(string url)
	    {
	        m_url = url;
	        return this.Post();
	    }
	    /// <summary>
	    /// Posts the supplied data to specified url.
	    /// </summary>
	    /// <param name="url">The url to post to.</param>
	    /// <param name="values">The values to post.</param>
	    /// <returns>a string containing the result of the post.</returns>
	    public string Post(string url, NameValueCollection values)
	    {
	        m_values = values;
	        return this.Post(url);
	    }
	    /// <summary>
	    /// Posts data to a specified url. Note that this assumes that you have already url encoded the post data.
	    /// </summary>
	    /// <param name="url">the url to post to.</param>
	    /// <param name="postData">The data to post.</param>
	    /// <returns>Returns the result of the post.</returns>
	    private string PostData(string url, string postData)
	    {
	        ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);
	
	        HttpWebRequest request = null;              
	
	        if (m_type == PostType.Post || m_type == PostType.Json)
	        {
	
	            Uri uri = new Uri(url);
	            request = (HttpWebRequest)WebRequest.Create(uri);
	            request.Method = "POST";
				request.ContentType = m_type == PostType.Post ? "application/x-www-form-urlencoded" : "application/json";
	            request.ContentLength = postData.Length;
	            request.Timeout = Timeout;
	            //request.TransferEncoding = "gzip,deflate";            
	            //request.ProtocolVersion = HttpVersion.Version10;
	            //request.AllowAutoRedirect = false;
	            //request.KeepAlive = false;
	            //System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();
	            //request.Headers.Add("Host", "walmart.getnet-tecnologia.com.br");
	            //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
	            //request.Connection = "keep-alive";            
	            //request.Referer = "https://walmart.getnet-tecnologia.com.br/app/home.cfm";
	
				request.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_5) AppleWebKit/537.31 (KHTML, like Gecko) Chrome/26.0.1410.65 Safari/537.31";
	
	            SetCookies(request);
	            SetProxy(request);
	            
	            using (Stream writeStream = request.GetRequestStream())
	            {
	                UTF8Encoding encoding = new UTF8Encoding();
	                byte[] bytes = encoding.GetBytes(postData);
	                writeStream.Write(bytes, 0, bytes.Length);
	            }
	     	}
	        else
	        {
	            Uri uri = new Uri(url + "?" + postData);
	            request = (HttpWebRequest)WebRequest.Create(uri);
	            request.Method = "GET";
				request.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_5) AppleWebKit/537.31 (KHTML, like Gecko) Chrome/26.0.1410.65 Safari/537.31";

	            SetCookies(request);
	            SetProxy(request);
	        }
	        string result = string.Empty;
	
	                    
	
	        try
	        {
	            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
	            {
	                m_responseHeaders = response.Headers;
	                using (Stream responseStream = response.GetResponseStream())
	                {
	                    if (ReadAttachmentFile)
	                    {
	                        MemoryStream memStream = new MemoryStream();
	                        long size = StreamCopy(response.GetResponseStream(), memStream);
	
	                        // Next, retrieve the suggested filename from the Content-Disposition header
	                        // and write the blob contents out to the file.
	                        ContentDisposition disp = new ContentDisposition(response.Headers["Content-Disposition"]);
	                        string suggestedFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, disp.FileName);
	
	
	                        try
	                        {
	                            using (FileStream fs = File.OpenWrite(suggestedFilename))
	                            {
	                                fs.Write(memStream.ToArray(), 0, (int)size);
	                            }
	                        }
	                        catch (IOException ex)
	                        {
	                            throw new ApplicationException("Unable to write blob data out!", ex);
	                        }
	
	                    }
	                    else
	                    {
	                        using (StreamReader readStream = new StreamReader(responseStream, Encoding.GetEncoding(ResponseEncoding)))
	                        {
	                            result = readStream.ReadToEnd();
	                        }
	                    }
	                }
	            }
	        }
	        catch (WebException wEx)
	        {
				var response = wEx.Response;

				if(response == null)
				{
					throw;
				}
				else
				{
		            using (Stream errorResponseStream = response.GetResponseStream())
		            {
		
		                using (StreamReader errorReadStream = new StreamReader(errorResponseStream, Encoding.GetEncoding(ResponseEncoding)))
		                {
		                    result = errorReadStream.ReadToEnd();
		                    throw new Exception(result);
		                }
		
		            }
				}
	        }
	
	        return result;
	    }
	
	    protected static long StreamCopy(Stream input, Stream output)
	    {
	        byte[] buffer = new byte[64 * 1024];
	        long length = 0L;
	
	        for (; ; )
	        {
	            int read = input.Read(buffer, 0, buffer.Length);
	            if (read > 0)
	            {
	                length += (long)read;
	                output.Write(buffer, 0, read);
	            }
	            else
	                break;
	        }
	
	        return length;
	    }
	
	
	    private void SetCookies(HttpWebRequest request)
	    {
	        if (m_cookieContainer != null)
	        {
	            request.CookieContainer = m_cookieContainer;
	        }
	    }
	
	    private void SetProxy(HttpWebRequest request)
	    {
	//        if (false)
	//        {
	//            string userName = ConfigurationManager.AppSettings["InternetUserName"];
	//            string password = ConfigurationManager.AppSettings["InternetPassword"];
	//            string domain = ConfigurationManager.AppSettings["InternetDomain"];
	//            string proxy = ConfigurationManager.AppSettings["InternetProxy"];
	//
	//            if (String.IsNullOrEmpty(userName))
	//            {
	//                request.Proxy = new WebProxy(proxy);
	//            }
	//            else
	//            {
	//                NetworkCredential credential = new NetworkCredential(userName, password, domain);
	//                request.Credentials = credential;
	//                request.Proxy = new WebProxy(proxy, true, new string[0], credential);
	//            }
	//        }
	//        else
	//        {
	            request.Proxy = null;
	        //}
	    }
	
	    /// <summary>
	    /// Encodes an item and ads it to the string.
	    /// </summary>
	    /// <param name="baseRequest">The previously encoded data.</param>
	    /// <param name="dataItem">The data to encode.</param>
	    /// <returns>A string containing the old data and the previously encoded data.</returns>
	    private void EncodeAndAddItem(ref StringBuilder baseRequest, string key, string dataItem)
	    {
	        if (baseRequest == null)
	        {
	            baseRequest = new StringBuilder();
	        }
	        if (baseRequest.Length != 0)
	        {
	            baseRequest.Append("&");
	        }
	        baseRequest.Append(key);
	        baseRequest.Append("=");
			baseRequest.Append(dataItem);
	    }

		private void EncodeAndAddItemForJson(ref StringBuilder baseRequest, string key, string dataItem)
		{
			if (baseRequest == null)
			{
				baseRequest = new StringBuilder();
			}
			if (baseRequest.Length != 0)
			{
				baseRequest.Append(", ");
			}
			baseRequest.AppendFormat("\"{0}\":\"{1}\"", key, dataItem);
		}
	
	    public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
	    {
	        return true;
	    }
		#endregion	
	}
	
	public class MyPolicy : ICertificatePolicy
	{
	    public bool CheckValidationResult(ServicePoint srvPoint,
	      X509Certificate certificate, WebRequest request,
	      int certificateProblem)
	    {
	        //Return True to force the certificate to be accepted.
	        return true;
	    }
	}
}