using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using Google.Cloud.Firestore;
using FirebaseAdmin.Auth;
using DbCore.IMBUtils.Security;
using Microsoft.Graph;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using Google.Protobuf.WellKnownTypes;

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
        private static string userDetailsCollection = "userDetails";
        private static string organizationCollection = "organization";
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
                FirebaseApp.Create(appOptions);
            }

        }
        public async Task<bool> checkIfUserIsVerified(string email)
        {
            var imbPayment = System.Web.Hosting.HostingEnvironment.MapPath("~/service_account/imb-payment.json");
            string serviceAccountJson = System.IO.File.ReadAllText(imbPayment);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", imbPayment);
            Dictionary<string, object> documentDictionary = new Dictionary<string, object>();
            FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
            Query usersRef = firestoreDb.Collection(userDetailsCollection).WhereEqualTo("email", email);
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();
            foreach (var snap in snapshot)
            {
                documentDictionary = snap.ToDictionary();
            }
            if (documentDictionary.ContainsKey("verified"))
                return documentDictionary["verified"].ToString() == "True" ? true : false;
            else return false;


        }
        public async Task<Dictionary<string,object>> getUserPassword(string idToken)
        {
            var imbPayment = System.Web.Hosting.HostingEnvironment.MapPath("~/service_account/imb-payment.json");
            string serviceAccountJson = System.IO.File.ReadAllText(imbPayment);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", imbPayment);
            Dictionary<string, object> documentDictionary = new Dictionary<string, object>();
            FirebaseToken firebaseToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
            string uid = firebaseToken.Uid;
            FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
            Query usersRef = firestoreDb.Collection(userDetailsCollection).WhereEqualTo("uid" ,uid);
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();
            foreach(var snap in snapshot) {
                documentDictionary = snap.ToDictionary();
            }
            FirebaseAuth fa = FirebaseAuth.GetAuth(FirebaseApp.DefaultInstance);
            
            Task<string> jwt = fa.CreateCustomTokenAsync(uid);
            return documentDictionary;
            //QueryBuilder qb = QueryBuilder.New("");
            //client = new FirebaseClient(config);
            //var response = client.Get(@userDetailsCollection);
            //string todo = response.Body.ToString();
        }
        public async Task<Dictionary<string,object>> getUserDetailsWithUID(string uid)
        {
            var imbPayment = System.Web.Hosting.HostingEnvironment.MapPath("~/service_account/imb-payment.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", imbPayment);
            Dictionary<string, object> documentDictionary = new Dictionary<string, object>();
            FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
            Query usersRef = firestoreDb.Collection(userDetailsCollection).WhereEqualTo("uid", uid);
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();
            foreach (var snap in snapshot)
            {
                if(snap.Id == uid)
                    documentDictionary = snap.ToDictionary();
            }
            return documentDictionary;
        }
        public async Task<string> createNewOrganization(object organizationDetails)
        {
            var imbPayment = System.Web.Hosting.HostingEnvironment.MapPath("~/service_account/imb-payment.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", imbPayment);
            FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
            DocumentReference writeResult = await firestoreDb.Collection(organizationCollection).AddAsync(organizationDetails);
            return writeResult.Id;
        }
        public Task<WriteResult> createNewUser(object userDetails,string uid)
        {
            var imbPayment = System.Web.Hosting.HostingEnvironment.MapPath("~/service_account/imb-payment.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", imbPayment);
            FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");           
            Task<WriteResult> docRef = firestoreDb.Collection(userDetailsCollection).Document(uid).SetAsync(userDetails);
            return docRef;
        }
        public async Task<bool> updateUserDetails(Dictionary<string, object> userDetails, string uid)
        {
            var imbPayment = System.Web.Hosting.HostingEnvironment.MapPath("~/service_account/imb-payment.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", imbPayment);
            Dictionary<string, object> uDetails = await getUserDetailsWithUID(uid);
            if (uDetails.ContainsKey("alphaOrganization") == true)
            {
                if (!(uDetails["alphaOrganization"].ToString() == clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar()))
                {
                    try
                    {
                        FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
                        WriteResult docRef = await firestoreDb.Collection(userDetailsCollection).Document(uid).UpdateAsync(userDetails);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                }
                else return false;
            }
            else
            {
                try
                {
                    FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
                    WriteResult docRef = await firestoreDb.Collection(userDetailsCollection).Document(uid).UpdateAsync(userDetails);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return true;
        }
        public async Task<bool> checkIfUserExists(string uid)
        {
            string imbPayment = System.Web.Hosting.HostingEnvironment.MapPath("~/service_account/imb-payment.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", imbPayment);
            Dictionary<string, object> uDetails = await getUserDetailsWithUID(uid);
            if (uDetails.Count == 0) return false;
            else return true;
        }

        public object createUserDetailsObject(string uid, string username, string pass,string email,string alphaOrganization,string orgid)
        {
            return new
            {
                username = username,
                uid = uid,
                passwordHash = pass,
                alphaOrganization = alphaOrganization,
                email = email,
                organization = orgid
            };
            
        }
        public object createOrganizationDetailsObject(string alphaOrganization)
        {
            return new
            {
                organization= alphaOrganization,
                clientId= "",
                currency= "LEK",
                docNo= false,
                firstDocNo= 1,
                currentDocNo= 1,
                integration= false,
                bccEmailAddress= "",
                createdAt= DateTime.Now.ToString(),
                updatedAt= "",
                updatedBy= "",
                fiscalization= false,
                einvoice= false,
                isIssuerInVAT= false,
                sellerName= "",
                sellerAddress= "",
                sellerTown= "",
                sellerCountry= "ALB",
                issuerNUIS= "",
                businUnitCode= "",
                operatorCode= "",
                tcrCode= "",
                accountID= "",
                accountName= "",
                secret= "",
                accountID2= "",
                accountName2= "",
                fromEmailAddress= "",
                subject= "",
                defMessage= "",
                templateId= "154b01ec-1f8b-410b-a945-adedc1f5dd0e",
                templateViewer= "akv88ef0v.hbs",
                testFiscalization= false,
                connectionStringName= "",
                organizationServer= "",
                authorization= "",
                username= "",
                nodeUrl= "https://node.alpha.al",
                formatPerImportClient= "",
                formatPerImportShitje= "",
                formatPerImportDalje= "",
                formatPerImportFurnitor= "",
                formatPerImportBlerje= "",
                cashRegisterCode= "",
                alphaOrganization = alphaOrganization
            };


        }
        public Dictionary<string, object> createUserDetailsObjectForUpdate(string alphaOrganization,string hashPassword,string username)

        {
            Dictionary<string, object> update = new Dictionary<string, object>
            {
                { "alphaOrganization", alphaOrganization },
                { "passwordHash", hashPassword },
                { "username",  username},
            };
            return update;

        }
    }
}
