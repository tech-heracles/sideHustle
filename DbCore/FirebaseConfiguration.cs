using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using FireSharp;
using FireSharp.Interfaces;
using FireSharp.Config;
using Microsoft.Graph;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using FirebaseAdmin.Auth;

namespace DbCore
{
    public class FirebaseConfiguration
    {
        //IFirebaseClient client;
        //IFirebaseConfig config = new FirebaseConfig
        //{
        //    AuthSecret = "eowUa51dSv2JVPLNaanbyIt3Kq0g7mJemw7eEUjh",
        //    BasePath = "https://imb-payment.firebaseio.com/"
        //};

        public FirebaseConfiguration(){
            
            if(FirebaseApp.DefaultInstance == null)
            {
                var imbPayment = System.Web.Hosting.HostingEnvironment.MapPath("~/service_account/imb-payment.json");
                string serviceAccountJson = System.IO.File.ReadAllText(imbPayment);
                AppOptions appOptions = new AppOptions();
                appOptions.ProjectId = "imb-payment";
                var credentialsServiceAccount = JsonConvert.DeserializeObject<object>(serviceAccountJson);
                GoogleCredential credential = Task.Run(() => GoogleCredential.FromJson(serviceAccountJson)).Result;
                appOptions.Credential = credential;
                FirebaseApp fap = FirebaseApp.Create(appOptions);
                
            }

        }
        public async Task<Dictionary<string,object>> getUserPassword(string idToken)
        {
            var imbPayment = System.Web.Hosting.HostingEnvironment.MapPath("~/service_account/imb-payment.json");
            string serviceAccountJson = System.IO.File.ReadAllText(imbPayment);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", imbPayment);
            FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
            Dictionary<string, object> documentDictionary = new Dictionary<string, object>();
            FirebaseToken firebaseToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
            string uid = firebaseToken.Uid;
            Query usersRef = firestoreDb.Collection("userDetails").WhereEqualTo("uid" ,uid);
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();
            foreach(var snap in snapshot) {
                documentDictionary = snap.ToDictionary();
            }
            FirebaseAuth fa = FirebaseAuth.GetAuth(FirebaseApp.DefaultInstance);
            
            Task<string> jwt = fa.CreateCustomTokenAsync(uid);
            return documentDictionary;
            //QueryBuilder qb = QueryBuilder.New("");
            //client = new FirebaseClient(config);
            //var response = client.Get(@"userDetails");
            //string todo = response.Body.ToString();
        }
        public async Task<Dictionary<string,object>> getUserDetailsWithUID(string uid)
        {
            var imbPayment = System.Web.Hosting.HostingEnvironment.MapPath("~/service_account/imb-payment.json");
            string serviceAccountJson = System.IO.File.ReadAllText(imbPayment);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", imbPayment);
            FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
            Dictionary<string, object> documentDictionary = new Dictionary<string, object>();
            Query usersRef = firestoreDb.Collection("userDetails").WhereEqualTo("uid", uid);
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();
            foreach (var snap in snapshot)
            {
                documentDictionary = snap.ToDictionary();
            }
            return documentDictionary;
        }
    }
}
