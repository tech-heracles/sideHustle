using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using Google.Cloud.Firestore;
using FirebaseAdmin.Auth;
using DbCore.IMBUtils.Fiskalizimi.Controls;

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
                AppOptions appOptions = new AppOptions();
                appOptions.ProjectId = "imb-payment";
                GoogleCredential credential = Task.Run(() => GoogleCredential.GetApplicationDefault()).Result;


                appOptions.Credential = credential;
                FirebaseApp.Create(appOptions);
            }

        }
        public async Task<bool> checkIfUserIsVerified(string email)
        {

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
        }
        public async Task<Dictionary<string,object>> getUserDetailsWithUID(string uid)
        {
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
        public async Task<Dictionary<string,object>> getUserDetailsWithEmail(string email)
        {
            try
            {
                Dictionary<string, object> documentDictionary = new Dictionary<string, object>();
                FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
                Query usersRef = firestoreDb.Collection(userDetailsCollection).WhereEqualTo("email", email);
                QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();
                foreach (var snap in snapshot)
                {
                    if (snap.Id == snap.ToDictionary()["uid"].ToString())
                        documentDictionary = snap.ToDictionary();
                }
                return documentDictionary;
            }
            catch(Exception e)
            {
                return new Dictionary<string, object>();
            }

        }
        public async Task<string> createNewOrganization(object organizationDetails,string uid)
        {
            FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
            DocumentReference writeResult = await firestoreDb.Collection(organizationCollection).AddAsync(organizationDetails);
            Google.Cloud.Firestore.WriteResult docRefOrg = await firestoreDb.Collection(organizationCollection).Document(writeResult.Id).UpdateAsync(updateOrganizationMetaData(writeResult.Id, uid));
            
            return writeResult.Id;
        }
        public Task<WriteResult> createNewUser(object userDetails,string uid)
        {
            FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");           
            Task<WriteResult> docRef = firestoreDb.Collection(userDetailsCollection).Document(uid).SetAsync(userDetails);
            return docRef;
        }
        public async Task<bool> updateUserDetails(Dictionary<string, object> userDetails, string uid, string ndermarrja)
        {
            FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
            bool admin = false;
            string loggedInOrg = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar();
            Dictionary<string, object> uDetails = await getUserDetailsWithUID(uid);
            QuerySnapshot userResult = await firestoreDb.Collection(userDetailsCollection).WhereEqualTo("organization", uDetails["organization"].ToString()).GetSnapshotAsync();
            if (userResult.Count > 1)
            {
                if (uDetails.ContainsKey("admin"))
                    if (uDetails["admin"].ToString() == "True")
                        admin = true;
                
            }
            else if (userResult.Count == 1) admin = true;
            if (uDetails.ContainsKey("alphaOrganization") == true)
            {
                if (uDetails["alphaOrganization"].ToString() == loggedInOrg)
                {
                    try
                    {
                        WriteResult docRef = await firestoreDb.Collection(userDetailsCollection).Document(uid).UpdateAsync(userDetails);
                        //if(admin && uDetails.ContainsKey("organization"))
                        //    await firestoreDb.Collection(organizationCollection).Document(uDetails["organization"].ToString()).UpdateAsync(createOrganizationObjectForUpdate(loggedInOrg, ndermarrja));
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    return false;

                }
                else return false;
            }
            else
            {
                try
                {
                    WriteResult docRef = await firestoreDb.Collection(userDetailsCollection).Document(uid).UpdateAsync(userDetails);
                    if(admin && uDetails.ContainsKey("organization"))
                        await firestoreDb.Collection(organizationCollection).Document(uDetails["organization"].ToString()).UpdateAsync(createOrganizationObjectForUpdate(loggedInOrg,ndermarrja));
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
            Dictionary<string, object> uDetails = await getUserDetailsWithUID(uid);
            if (uDetails.Count == 0) return false;
            else return true;
        }
        public async Task<bool> checkIfUserExistsWithAlpha(string shenime)
        {
            Dictionary<string, object> uDetails = await getUserDetailsWithEmail(shenime);
            if (uDetails.Count == 0) return true;
            if (uDetails.ContainsKey("alphaOrganization")) return false;
            else return true;
        }
        public async Task<Dictionary<string, object>> returnUserDetailsFromNotes(string shenime)
        {
            Dictionary<string, object> uDetails = await getUserDetailsWithEmail(shenime);
            if (uDetails.Count == 0) return new Dictionary<string, object>();
            if (uDetails.ContainsKey("alphaOrganization")) return uDetails;
            else return new Dictionary<string, object>();
        }
        public async void updateLogInTime(string uid)
        {
            FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
            await firestoreDb.Collection(userDetailsCollection).Document(uid).UpdateAsync(createUserDetailsObjectForUpdateLogInTime());

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
                organization = orgid,
                admin = true
            };
            
        }
        public object createOrganizationDetailsObject(string alphaOrganization, string ndermarrja)
        {
            return new
            {
                organization= alphaOrganization,
                clientId= "",
                currency= "ALL",
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
                alphaOrganization = alphaOrganization,
                enterprise = ndermarrja
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
        private Dictionary<string, object> createUserDetailsObjectForUpdateLogInTime()

        {
            Dictionary<string, object> update = new Dictionary<string, object>
            {
                { "alphaLastLogedInDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }
            };
            return update;

        }
        public Dictionary<string, object> createOrganizationObjectForUpdate(string alphaOrganization,string ndermarrja)

        {
            Dictionary<string, object> update = new Dictionary<string, object>
            {
                { "alphaOrganization", alphaOrganization },
                {"ndermarrja", ndermarrja}
            };
            return update;

        }
        public Dictionary<string, object> createOrganizatioObjectForUpdateOnlyNdermarrje(string ndermarrja)

        {
            Dictionary<string, object> update = new Dictionary<string, object>
            {
                {"ndermarrja", ndermarrja}
            };
            return update;

        }
        public Dictionary<string, object> updateOrganizationMetaData(string id,string uid)

        {
            Dictionary<string, object> update = new Dictionary<string, object>
            {
                {"metadata", new { 
                    id=id,
                    createdAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    createdBy = uid,
                    updatedAt = "",
                    updatedBy = ""
                } }
            };
            return update;

        }
    }
}
