using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbCore.classes.ProcessedOrder;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using DbCore.IMBUtils.Security;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

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
		public FirebaseConfiguration()
		{
			try
			{
				if (FirebaseApp.DefaultInstance == null)
				{
					AppOptions appOptions = new AppOptions();
					appOptions.ProjectId = "imb-payment";
					var imbPayment = System.Web.Hosting.HostingEnvironment.MapPath("~/service_account/imb-payment.json");
					string serviceAccountJson = System.IO.File.ReadAllText(imbPayment);
					var credentialsServiceAccount = JsonConvert.DeserializeObject<object>(serviceAccountJson);
					GoogleCredential credential = Task.Run(() => GoogleCredential.FromJson(serviceAccountJson)).Result;
					appOptions.Credential = credential;
					FirebaseApp.Create(appOptions);
				}
			}
			catch
			{
				Console.WriteLine("Firebase already created!");
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
		public async void addProcessOrder(ProcessOrder processOrder)
		{
			try
			{
				FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
				DocumentReference order_ref = firestoreDb.Collection("organization").Document(processOrder.metadata.organization).Collection("process-orders").Document();
				processOrder.metadata.id = order_ref.Id;
				order_ref.SetAsync(processOrder.toObject());
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error saving process order");
			}

		}
		public async void confirmOrder(string id, string orgId)
		{
			try
			{
				FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
				DocumentReference order_ref = firestoreDb.Collection("organization").Document(orgId).Collection("process-orders").Document(id);
				Dictionary<string, object> order = new Dictionary<string, object>
				{
					{ "status" , ProcessOrderStatus.confirmed.ToString() }
				};
				order_ref.UpdateAsync(order);

			}
			catch (Exception ex)
			{
				Console.WriteLine("Error changin order status");
			}
		}
		public async void returnOrder(string id, string orgId)
		{
			try
			{
				FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
				DocumentReference order_ref = firestoreDb.Collection("organization").Document(orgId).Collection("process-orders").Document(id);
				Dictionary<string, object> order = new Dictionary<string, object>
				{
					{ "status" , ProcessOrderStatus.returned.ToString() }
				};
				order_ref.UpdateAsync(order);

			}
			catch (Exception ex)
			{
				Console.WriteLine("Error changin order status");
			}
		}
		public async Task<Dictionary<string, object>> getUserPassword(string idToken)
		{
			Dictionary<string, object> documentDictionary = new Dictionary<string, object>();
			FirebaseToken firebaseToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
			string uid = firebaseToken.Uid;
			FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
			Query usersRef = firestoreDb.Collection(userDetailsCollection).WhereEqualTo("uid", uid);
			QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();
			foreach (var snap in snapshot)
			{
				documentDictionary = snap.ToDictionary();
			}
			FirebaseAuth fa = FirebaseAuth.GetAuth(FirebaseApp.DefaultInstance);

			Task<string> jwt = fa.CreateCustomTokenAsync(uid);
			return documentDictionary;
		}
		public async Task<Dictionary<string, object>> getUserDetailsWithUID(string uid)
		{
			Dictionary<string, object> documentDictionary = new Dictionary<string, object>();
			FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
			Query usersRef = firestoreDb.Collection(userDetailsCollection).WhereEqualTo("uid", uid);
			QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();
			foreach (var snap in snapshot)
			{
				if (snap.Id == uid)
					documentDictionary = snap.ToDictionary();
			}
			return documentDictionary;
		}
		public async Task<Dictionary<string, dynamic>> getUserDetailsWithUIDDynamic(string uid)
		{
			Dictionary<string, object> documentDictionary = new Dictionary<string, object>();
			FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
			DocumentReference usersRef = firestoreDb.Collection(userDetailsCollection).Document(uid);
			return (await usersRef.GetSnapshotAsync()).ToDictionary();
		}
		public async Task<Dictionary<string, object>> getUserDetailsWithEmail(string email)
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
			catch (Exception e)
			{
				return new Dictionary<string, object>();
			}

		}
		public async Task<string> createNewOrganization(object organizationDetails, string uid)
		{
			FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
			DocumentReference writeResult = await firestoreDb.Collection(organizationCollection).AddAsync(organizationDetails);
			Google.Cloud.Firestore.WriteResult docRefOrg = await firestoreDb.Collection(organizationCollection).Document(writeResult.Id).UpdateAsync(updateOrganizationMetaData(writeResult.Id, uid));

			return writeResult.Id;
		}
		public Task<WriteResult> createNewUser(object userDetails, string uid)
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
					if (admin && uDetails.ContainsKey("organization"))
						await firestoreDb.Collection(organizationCollection).Document(uDetails["organization"].ToString()).UpdateAsync(createOrganizationObjectForUpdate(loggedInOrg, ndermarrja));
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
		//public async Task<void> add

		public async void updateLogInTime(string uid)
		{
			FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
			await firestoreDb.Collection(userDetailsCollection).Document(uid).UpdateAsync(createUserDetailsObjectForUpdateLogInTime());

		}
		public async void changeOrganization(string uid, string alphaOrganization, string enterprise, int idPerdoruesi)
		{
			FirestoreDb firestoreDb = FirestoreDb.Create("imb-payment");
			DocumentReference user_ref = firestoreDb.Collection(userDetailsCollection).Document(uid);
			Dictionary<string, object> user_data = (await user_ref.GetSnapshotAsync()).ToDictionary();
			object org_object = createOrganizationDetailsObject(alphaOrganization, enterprise);
			string username = (string)user_data["username"];

			string pass = PasswordHelper.HashLogin(username, clsFunksione.generateRandomPassword());
			clsPerdorues.krijoPerdoruesMeGmail((string)user_data["email"], username, username, pass, idPerdoruesi);
			string org_id = "";
			bool status = false;
			ICollection<object> organizations = new List<object>();
			if (user_data.ContainsKey("previousOrganizations"))
			{
				organizations = user_data["previousOrganizations"] as System.Collections.Generic.ICollection<object>;
				object[] org_array = organizations.ToArray();
				for (int i = 0; i < org_array.Length; i++)
				{
					DocumentReference current_org_ref = firestoreDb.Collection(organizationCollection).Document(org_array[i].ToString());
					var current_organization = (await current_org_ref.GetSnapshotAsync()).ToDictionary();
					if (current_organization.ContainsKey("alphaOrganization"))
					{
						if ((string)current_organization["alphaOrganization"] == alphaOrganization)
						{
							org_id = current_org_ref.Id;
							status = true;
							break;
						}
					}
				};
			}
			organizations.Add((string)user_data["organization"]);
			if (!status) org_id = await createNewOrganization(org_object, uid);

			//List<string> organizations = user_data.ContainsKey("previousOrganizations") ? new List<string>(.TryGetValue("") : new List<string>();
			//organizations.Add((string)user_data["organization"]);
			Dictionary<string, object> update_dictionary = new Dictionary<string, object>
			{
				{ "alphaOrganization", alphaOrganization },
				{ "organization", org_id},
				{ "passwordHash", pass},
				{"previousOrganizations", organizations.ToArray() }
			};
			await user_ref.UpdateAsync(update_dictionary);
		}
		public object createUserDetailsObject(string uid, string username, string pass, string email, string alphaOrganization, string orgid)
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
				organization = alphaOrganization,
				clientId = "",
				currency = "ALL",
				docNo = false,
				firstDocNo = 1,
				currentDocNo = 1,
				integration = false,
				bccEmailAddress = "",
				createdAt = DateTime.Now.ToString(),
				updatedAt = "",
				updatedBy = "",
				fiscalization = false,
				einvoice = false,
				isIssuerInVAT = false,
				sellerName = "",
				sellerAddress = "",
				sellerTown = "",
				sellerCountry = "ALB",
				issuerNUIS = "",
				businUnitCode = "",
				operatorCode = "",
				tcrCode = "",
				accountID = "",
				accountName = "",
				secret = "",
				accountID2 = "",
				accountName2 = "",
				fromEmailAddress = "",
				subject = "",
				defMessage = "",
				templateId = "154b01ec-1f8b-410b-a945-adedc1f5dd0e",
				templateViewer = "akv88ef0v.hbs",
				testFiscalization = false,
				connectionStringName = "",
				organizationServer = "",
				authorization = "",
				username = "",
				nodeUrl = "https://node.alpha.al",
				formatPerImportClient = "",
				formatPerImportShitje = "",
				formatPerImportDalje = "",
				formatPerImportFurnitor = "",
				formatPerImportBlerje = "",
				cashRegisterCode = "",
				alphaOrganization = alphaOrganization,
				enterprise = ndermarrja
			};


		}
		public Dictionary<string, object> createUserDetailsObjectForUpdate(string alphaOrganization, string hashPassword, string username)

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
		public Dictionary<string, object> createOrganizationObjectForUpdate(string alphaOrganization, string ndermarrja)

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
		public Dictionary<string, object> updateOrganizationMetaData(string id, string uid)

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
