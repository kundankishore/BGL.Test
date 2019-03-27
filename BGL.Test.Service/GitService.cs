using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using BGL.Test.DataContract;
using BGL.Test.Service.Interfaces;
using Newtonsoft.Json.Linq;

namespace BGL.Test.Service
{
    public class GitService : IGitService
    {
        public UserDetails GetUserDetails(string userName)
        {
            var gitLocation = ConfigurationManager.AppSettings["APILink"];

            var detials = GetUserDetailsFromGit(gitLocation + "users/" + userName);

            var list = GetRepositoryDetailsFromGit(detials.ReposUrl);

            detials.RepoList = list.ToList();

            return detials;
        }

        private UserDetails GetUserDetailsFromGit(string url)
        {
            UserDetails userdetails = new UserDetails();
            try
            {
                var requestUrl = url;

                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                request.KeepAlive = false;
                request.UserAgent = "myTestApp";

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format(
                            "Server error (HTTP {0}: {1}).",
                            response.StatusCode,
                            response.StatusDescription));
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(User));
                    object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                    User jsonResponse = objResponse as User;
                    if (jsonResponse != null)
                    {
                        userdetails.UserName = jsonResponse.Name;
                        userdetails.AvatarUrl = jsonResponse.AvatarUrl;
                        userdetails.Location = jsonResponse.Location;
                        userdetails.ReposUrl = jsonResponse.ReposUrl;
                    }
                }
            }
            catch (Exception e)
            {
                //To do later log Exception in Log files
            }
            return userdetails;
        }

        private IEnumerable<string> GetRepositoryDetailsFromGit(string url)
        {
            IDictionary<string, int> repostats = new Dictionary<string, int>();
            try
            {
                var requestUrl = url;
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                request.KeepAlive = false;
                request.UserAgent = "myTestApp";

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format(
                            "Server error (HTTP {0}: {1}).",
                            response.StatusCode,
                            response.StatusDescription));
                    string responseText = string.Empty;
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        responseText = reader.ReadToEnd();
                    }
                    JArray a = JArray.Parse(responseText);

                    foreach (JObject o in a.Children<JObject>())
                    {
                        string name = string.Empty;
                        int count = 0;
                        foreach (JProperty p in o.Properties())
                        {
                            if (p.Name == "name")
                            {
                                name = Convert.ToString(p.Value);
                            }
                            if (p.Name == "stargazers_count")
                            {
                                count = Convert.ToInt32(p.Value);
                            }
                        }
                        repostats.Add(name, count);
                    }
                }
            }
            catch (Exception e)
            {
                //To do later log Exception in Log files
            }
            var sorteddic = repostats.OrderBy(x => x.Value).Take(5).Select(x => x.Key);

            return sorteddic;
        }

        #region Below are the classes used to parse the user json data for git.

        [DataContract]
        public class User
        {
            [DataMember(Name = "avatar_url")]
            public string AvatarUrl { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "location")]
            public string Location { get; set; }

            [DataMember(Name = "repos_url")]
            public string ReposUrl { get; set; }
        }

        public class Repository
        {
            public string name { get; set; }
        }

        #endregion
    }
}
