namespace UCG.siteTRAXLite.Common.Constants
{
    public class Endpoints
    {
        private static Dictionary<string, CountryEndPointSetting> endpointSetting = null;
        public static Dictionary<string, CountryEndPointSetting> EndpointSettingURL
        {
            get
            {
                if (endpointSetting == null)
                {
                    endpointSetting = new Dictionary<string, CountryEndPointSetting>();

                    // AU
                    var auSettings = new CountryEndPointSetting("AU");
                    auSettings.Add("Live",
                     new EndPointSetting(
                        name: "Live",
                               endpointDPPBaseURL: "https://dpp.sitetrax.com.au/",
                            endpointBMPSignalRURL: "https://sitetrax.com.au/signalr",
                          isDefault: true
                    ));
                    auSettings.Add("Staging",
                     new EndPointSetting(
                        name: "Staging",
                        endpointDPPBaseURL: "https://Staging.dpp.sitetrax.com.au",
                            endpointBMPSignalRURL: "https://Staging.sitetrax.com.au/signalr",
                            isDefault: false
                    ));
                    auSettings.Add("Conf",
                     new EndPointSetting(
                            name: "Conf",
                            endpointDPPBaseURL: "https://conf.dpp.sitetrax.com.au",
                            endpointBMPSignalRURL: "https://conf.sitetrax.com.au/signalr",
                            isDefault: false
                        ));
                    auSettings.Add("QA",
                        new EndPointSetting(
                            name: "QA",
                            endpointDPPBaseURL: "https://qa.dpp.sitetrax.com.au",
                            endpointBMPSignalRURL: "https://qa.sitetrax.com.au/signalr",
                            isDefault: false
                        ));
                    auSettings.Add("QA ST3",
                        new EndPointSetting(
                            name: "QA ST3",
                            endpointDPPBaseURL: "https://dpp.qa.st3.sitetrax.com.au",
                            endpointBMPSignalRURL: "https://qa.st3.sitetrax.com.au/signalr",
                            isDefault: false
                        ));
                    auSettings.Add("Training",
                     new EndPointSetting(
                         name: "Training",
                         endpointDPPBaseURL: "https://training.dpp.sitetrax.com.au",
                         endpointBMPSignalRURL: "https://training.sitetrax.com.au/signalr",
                         isDefault: false
                     ));
                    auSettings.Add("Sandbox",
                     new EndPointSetting(
                            name: "Sandbox",
                            endpointDPPBaseURL: "https://sandbox.dpp.sitetrax.com.au/",
                            endpointBMPSignalRURL: "https://sandbox.sitetrax.com.au/signalr",
                            isDefault: false
                        ));
                    endpointSetting.Add(auSettings.CountryCode, auSettings);

                    // NZ
                    var nzSettings = new CountryEndPointSetting("NZ");
                    nzSettings.Add("Live",
                        new EndPointSetting(
                            name: "Live",
                            endpointDPPBaseURL: "https://dpp.sitetrax.co.nz",
                            endpointBMPSignalRURL: "https://sitetrax.co.nz/signalr",
                            isDefault: true
                        ));
                    nzSettings.Add("Staging",
                     new EndPointSetting(
                            name: "Staging",
                            endpointDPPBaseURL: "https://Staging.dpp.sitetrax.co.nz",
                            endpointBMPSignalRURL: "https://Staging.sitetrax.co.nz/signalr",
                            isDefault: false
                        ));
                    nzSettings.Add("Chorus Staging",
                     new EndPointSetting(
                            name: "Chorus Staging",
                            endpointDPPBaseURL: "https://staging.dpp.chorus.sitetrax.co.nz/",
                            endpointBMPSignalRURL: "https://staging.chorus.sitetrax.co.nz/signalr",
                            isDefault: false
                        ));
                    nzSettings.Add("SCG",
                     new EndPointSetting(
                           name: "SCG",
                           endpointDPPBaseURL: "https://scg.dpp.sitetrax.co.nz/",
                           endpointBMPSignalRURL: "https://scg.sitetrax.co.nz/signalr",
                           isDefault: false
                       ));
                    nzSettings.Add("Conf",
                     new EndPointSetting(
                            name: "Conf",
                            endpointDPPBaseURL: "https://conf.dpp.sitetrax.co.nz",
                            endpointBMPSignalRURL: "https://conf.sitetrax.co.nz/signalr",
                            isDefault: false
                        ));
                    nzSettings.Add("QA",
                        new EndPointSetting(
                            name: "QA",
                            endpointDPPBaseURL: "https://qa.dpp.sitetrax.co.nz",
                            endpointBMPSignalRURL: "https://qa.sitetrax.co.nz/signalr",
                            isDefault: false
                        ));
                    nzSettings.Add("QA ST3",
                        new EndPointSetting(
                            name: "QA ST3",
                            endpointDPPBaseURL: "https://dpp.qa.st3.sitetrax.co.nz",
                            endpointBMPSignalRURL: "https://qa.st3.sitetrax.co.nz/signalr",
                            isDefault: false
                        ));
                    nzSettings.Add("Training",
                     new EndPointSetting(
                         name: "Training",
                         endpointDPPBaseURL: "https://training.dpp.sitetrax.co.nz",
                         endpointBMPSignalRURL: "https://training.sitetrax.co.nz/signalr",
                         isDefault: false
                     ));
                    nzSettings.Add("Sandbox",
                     new EndPointSetting(
                            name: "Sandbox",
                            endpointDPPBaseURL: "https://sandbox.dpp.sitetrax.co.nz/",
                            endpointBMPSignalRURL: "https://sandbox.sitetrax.co.nz/signalr",
                            isDefault: false
                        ));
                    nzSettings.Add("New Dev",
                     new EndPointSetting(
                            name: "New Dev",
                            endpointDPPBaseURL: "https://ci.dpp.sitetrax.co.nz/",
                            endpointBMPSignalRURL: "https://ci.sitetrax.co.nz/signalr",
                            isDefault: false
                        ));
                    nzSettings.Add("New Dev2",
                     new EndPointSetting(
                            name: "New Dev2",
                            endpointDPPBaseURL: "https://ci2.dpp.sitetrax.co.nz/",
                            endpointBMPSignalRURL: "https://ci2.sitetrax.co.nz/signalr",
                            isDefault: false
                        ));
                    nzSettings.Add("Dev",
                     new EndPointSetting(
                            name: "Dev",
                            endpointDPPBaseURL: "https://ci.dpp.chorus.sitetrax.co.nz/",
                            endpointBMPSignalRURL: "https://ci.chorus.sitetrax.co.nz/signalr",
                            isDefault: false
                        ));
                    nzSettings.Add("Dev2",
                     new EndPointSetting(
                            name: "Dev2",
                            endpointDPPBaseURL: "https://ci2.dpp.chorus.sitetrax.co.nz/",
                            endpointBMPSignalRURL: "https://ci2.chorus.sitetrax.co.nz/signalr",
                            isDefault: false
                        ));


                    endpointSetting.Add(nzSettings.CountryCode, nzSettings);
                }

                return endpointSetting;
            }
        }

        public static EndPointSetting GetEndpointSetting(string country, string endpointName)
        {
            CountryEndPointSetting countryEndPoint = null;
            if (EndpointSettingURL.ContainsKey(country))
            {
                countryEndPoint = EndpointSettingURL[country];

            }
            return countryEndPoint?.GetEnpoint(endpointName);
        }

        public static CountryEndPointSetting GetEndpointByCountry(string country)
        {
            if (EndpointSettingURL.ContainsKey(country))
            {
                return EndpointSettingURL[country];
            }
            return null;
        }

        public static IEnumerable<string> GetAllCountries()
        {
            return EndpointSettingURL.Values.ToList().Select(s => s.CountryCode);
        }

        public const string ToDoEndpoint = "/todos";
        public const string AuthenEndpoint = "/token?logintype=mobile";
        public const string ForgotPassword = "/api/Account/ForgotPassword";
        public const string SorUploadEndpoint = "/api/sor/sors/uploadFile";
        public const string ValidateEmailCodeEndPoint = "/api/Account/validateemailcode";
        public const string ResetPasswordEndpoint = "/api/Account/ResetPassword";

        // user info
        public const string UserInfoEndpoint = "/api/account/UserInfo";
        public const string UserCompaniesEndpoint = "/api/Users/findusercompaniespaged";
        public const string UserGroupEndpoint = "/api/Users/findusergroupspaged";

        // crews api
        public const string CrewFindSchedulingFilterEndpoint = "/api/Dpp/Scheduling/findschedulingfilters";
        public const string CrewFindAllSchedules = "/api/Dpp/SchedulingJob/findallusersschedules";
        public const string CrewFindAllSchedulesWithoutOdata = "/api/Dpp/SchedulingJob/findallusersschedulesnoodata";
        public const string CrewUCGFindAllSchedules = "/api/Dpp/SchedulingJob/ucgfindallusersschedules";
        public const string CrewUCGFindAllSchedulesWithoutOdata = "/api/Dpp/SchedulingJob/ucgfindallusersschedulesnoodata";
        public const string SchedulesOption = "/api/Dpp/SchedulingJob/findallusersschedules_options";
        public const string GetJobTypeSchedules = "/api/Dpp/SchedulingJob/getjobtypeschedules";
        public const string GetAllSuburd = "/api/Dpp/SchedulingJob/getallsuburd";

        public const string FieldPortalEndpoint = "/api/Sites/findallusersschedules";
        public const string UserOnSiteHistoryEndpoint = "api/Dpp/UserOnSiteHistories/Create";

        public const string SiteStatusHistoryCreateEndpoint = "/api/dpp/usersitestatushistory/create";
        public const string SiteStatusHistoryGetEndpoint = "/api/dpp/usersitestatushistory/findbyuserandcompany";
        public const string SiteStatus = "/api/dpp/usersitestatushistory/findallsublistcategory?categoryName={0}";
        public const string ProgramCustomFields = "/api/sites/sitefields";
        public const string JobTypeCustomFields = "/api/sites/jobTypeFields";
        public const string JobLinkActivities = "/api/sites/jobLinkBySite";


        //alerts
        public const string GetAlertsEndpoint = "/api/Hseq/Alerts/getByUser/";
        public const string GetAlertDetailEndpoint = "api/Hseq/Alerts/{0}?$expand=HseqAlertAttachments,HseqAlertViewerActions,HseqAlertRegions,HseqAlertPods,HseqAlertStates,HseqAlertRegions,HseqAlertPods,HseqAlertViewerActions($expand = SubListCategory)";
        public const string PostConfirmReadAlert = "api/UserActionHseq/CreatedUserAlertAction";
        public const string DownloadAlertAttachmentURL = "/HseqAlertAttach/{0}";

        // Question manager
        public const string GetQuestionSetEndpoint = "/api/mobileapi/question/getquestionsets";
        public const string GetQuestionGroupsEndpoint = "/api/mobileapi/question/getquestiongroups";
        public const string CreateOrUpdateQuestionEndpoint = "/api/mobileapi/question/createorupdate";
        public const string GetQuestionsByJobTypeEndpoint = "/api/mobileapi/question/getquestionsbyjobtype";
        public const string GetFirstQuestionByGroupEndpoint = "/api/mobileapi/question/getfirstquestionbygroup";
        public const string GetAssignedQuestionForUserEndpoint = "/api/mobileapi/question/getassignedquestionsforuser";
        public const string GetCurrentQuestionForUserEndpoint = "/api/mobileapi/question/getcurrentquestionforuser";
        public const string QuestionManagerUploadFile = "/api/mobileapi/question/uploadfile";

        // Messages
        public const string GetAllMessageEndpoint = "/api/Dpp/TeamMessages/findallusermessages";
        public const string GetJobMessageEnpoint = "/api/Dpp/JobMessages/JobId";
        public const string CreateJobMessageEnpoint = "/api/Dpp/JobMessages/JobId";

        //Locations
        public const string GetLocationsEndpoint = "/api/Locations/";
        public const string GetLocationDetailsEndpoint = "/api/Locations/{0}";
        public const string CreateNewLocation = "/api/Locations/new/";

        //List Categories
        public const string GetListCategories = "api/Dpp/PmpData/listcategories";
        public const string GetListCategoriesByKey = "api/Dpp/PmpData/listcategorybycategory";

        //UMCompany
        public const string GetUMCompany = "api/UMCompanies/findendpoints";
        public const string GetAllUMCompany = "api/UMCompanies/find";

        //WorkFlowStatus
        public const string GetWorkFlowStatus = "api/dpp/WorkflowStatus/findall";
        public const string GetSLATracking = "api/dpp/WorkflowStatus/slaTracking";
        public const string GetOnHoldWorkflowConfigurations = "api/dpp/WorkflowStatus/getonholdworkflowconfigurations";

        //Items
        public const string GetItemsEndpoint = "/api/Items/";

        //Transactions
        public const string GetTransactions = "/api/Transactions/";
        public const string CreateTransaction = "/api/Transactions/create";

        // EformSurveyQa
        public const string EformSurveyQaSection = "/api/EformSurveyQa/List";
        public const string EformSurveyQaAnswer = "/api/EformSurveyQa/CreateOrUpdate";

        // MobileJobServiceGiven
        public const string InsertOrUpdateMobileJobServiceGiven = "/api/dpp/servicegiven/insertorupdate";
        public const string GetMobileJobServiceGivenByJob = "/api/dpp/servicegiven/getbyjob";

        // JobPreStart
        public const string UsersInGroup = "api/Dpp/UserList/usersbyversion";
        public const string UserInGroupkey = "api/Dpp/UserList/usersingroupkey";
        public const string UsersInCompany = "api/Dpp/UserList/usersincompanywithteam";
        public const string GetHazards = "/api/Jps/Categories/Hazards";
        public const string GetSafetyEquipment = "/api/Jps/Categories/Equipment";
        public const string GetHazManualHandling = "/api/Jps/Categories/HazManualHandling";
        public const string GetHazEnvironmental = "/api/Jps/Categories/HazEnvironmental";
        public const string GetHazSite = "/api/Jps/Categories/HazSite";
        public const string GetHazOthers = "/api/Jps/Categories/HazOthers";
        public const string GetEquipment = "/api/Jps/Categories/Equipment";
        public const string GetPersonal = "/api/Jps/Categories/Personal";
        public const string GetElectrical = "/api/Jps/Categories/Electrical";
        public const string GetReasonForVisit = "/api/Jps/Categories/ReasonForVisit";
        public const string GetCrewLeavingSite = "/api/Jps/Categories/CrewLeavingSite";
        public const string GetCrewAtSite = "/api/Jps/Categories/CrewAtSite";
        public const string GetCrewLJobComplete = "/api/Jps/Categories/CrewLJobComplete";
        public const string GetCrewLJobIncomplete = "/api/Jps/Categories/CrewLJobIncomplete";
        public const string GetHighRiskActivities = "/api/Jps/Categories/HighRiskActivities";
        public const string SiteVisitorDeclaration = "/api/Jps/Categories/SiteVisitorDeclaration";
        public const string WorkSiteDeclaration = "/api/Jps/Categories/WorkSiteDeclaration";
        public const string LeavingSiteDeclaration = "/api/Jps/Categories/LeavingSiteDeclaration";
        public const string GetTeamMember = "/api/Jps/TeamMembers";
        public const string GetJPSVersion = "/api/Jps/Version";
        public const string InsertUpdateWorkSite = "/api/Jps/InsertUpdateWorkSite";
        public const string InsertUpdateCrewMember = "/api/Jps/InsertUpdateCrewMember";
        public const string InsertUpdateCrewLeavingSite = "/api/Jps/InsertOrUpdateLeavingOnSite";
        public const string GetJobPreStart = "/api/Jps/Get";
        public const string InsertUpdateHazards = "/api/Jps/InsertOrUpdateHazards";
        public const string InsertUpdateSafetyEquipment = "/api/Jps/InsertOrUpdateSafetyEquipment";
        public const string InsertUpdateSWMS = "/api/Jps/InsertOrUpdateSWMS";
        public const string InsertUpdateVisitorToSite = "/api/Jps/InsertOrUpdateSiteVisitor";
        public const string GetJpsByJob = "/api/Jps/GetLastJpsByJob";
        public const string GetErrorMessageForIncident = "/api/Jps/GetErrorMessageForIncident";
        public const string ListItemsByParentCategory = "/api/Jps/Categories/ListItemsByParentCategory";
        public const string ListItemsByCategoryName = "/api/Jps/Categories/ListItemsByCategoryName";
        public const string CrewMembersLeaving = "/api/Jps/{0}/CrewMembersLeaving";
        public const string HseqPermitsProperties = "/api/Jps/Categories/HseqPermitsProperties";
        public const string HseqPermitsPropertiesDataSource = "/api/Jps/Categories/HseqPermitsPropertiesDataSource";
        public const string InsertOrUpdateHseqPermits = "/api/Jps/InsertOrUpdateHseqPermits";
        public const string JpsGetFolderPath = "api/Jps/GetJpsFolderPath";
        public const string HseqAlertAttachFolderPath = "api/Hseq/Alerts/AttachFolderPath";
        public const string UploadFileJps = "api/Jps/uploadFile";
        public const string DeleteAttachmentJps = "api/Jps/DeleteAttachment?attachmentId={0}";
        public const string InsertUpdateJobPreStart = "/api/dpp/hseqjspversion/InsertOrUpdateJPS";
        public const string DownloadJPSAttachment = "api/Jps/DownloadFile";
        public const string GetContactNumbers = "/api/Jps/Categories/StaticData";
        public const string ValidateJpsSignOn = "/api/Jps/ValidateJpsSignOn";

        //Document Template
        public const string GetDocumentTemplatesByProgram = "/api/Jps/GetDocumentTemplatesByProgram";
        public const string ExportWordDocument = "/api/Jps/ExportWordDocument";

        // Document filter
        public const string GetDocument = "/api/Dpp/PmpData/dmdocuments";
        public const string GetCategory = "/api/Dpp/PmpData/Categories";
        public const string GetSubCategory = "/api/Dpp/PmpData/subcatgs";
        public const string DocumentDownloadFile = "/api/Dpp/PmpData/dmdocuments/downloadFile/{0}";
        public const string TeamList = "/api/Teams/TeamListActive/{0}";
        public const string GetRegions = "/api/Dpp/PmpData/regions/find";
        public const string GetPods = "/api/Dpp/PmpData/pods/find";

        // Job Note
        public const string GetJobNote = "/api/JobNotes";
        public const string NoteDownloadFile = "api/JobNoteAttachments/download";
        public const string UploadJobNoteAttachments = "/api/JobNoteAttachments/uploadFile";
        public const string CreateJobNote = "/api/JobNotes";
        public const string CreateJobNoteAttachment = "/api/JobNoteAttachments";
        public const string JobNoteAttachmentGetFilePath = "/api/JobNoteAttachments/filePath";
        public const string JobNoteAttachmentDelete = "/api/JobNoteAttachments";

        public const string JobSorReviewDownloadFile = "api/Sites/JobSors/downloadarterfact";
        public const string UploadJobSorReviewAttachments = "api/Sites/JobSors/uploadFile";
        public const string GetSiteNote = "/api/SiteNotes";
        public const string SiteNoteAttachmentGetFilePath = "/api/SiteNotes/filePath";
        public const string SiteNoteDownloadFile = "/api/SiteNotes/download";

        // Mobile Config
        public const string MobileConfigFindbyJobType = "/api/Dpp/MobileSetting/FindByJobType";
        public const string MobileReadOnlyConfig = "/api/Dpp/MobileSetting/GetReadOnly";

        // BMP Signature
        public const string CreateSignature = "/api/dpp/common/signatures/create";
        public const string SignatureFilePath = "/api/dpp/common/signatures/getpath";

        // WIEform
        public const string EFormList = "api/dpp/question/eforms/eformsbyjobtype";
        public const string EFormQuestionList = "api/dpp/question/eforms/readeform";
        public const string EFormUpdateForm = "api/dpp/question/eforms/createorupdateeform";
        public const string EFormGetFolder = "api/dpp/question/eforms/GetEFormsFolderPath";
        public const string UploadFileEform = "api/dpp/question/eforms/uploadFile";
        public const string DeleteAttachmentEform = "api/dpp/question/eforms/DeleteAttachment";
        public const string DownloadFileEform = "api/dpp/question/eforms/DownloadFile";
        public const string ThumbnailsPath = "api/common/thumbnails/getpath";
        public const string DownloadQuestionImage = "api/dpp/question/eforms/DownloadQuestionImages";
        public const string EFormAutoFilled = "api/dpp/question/eforms/eformsautofilled";
        public const string EFormPreviousResponse = "api/dpp/question/eforms/previousresponse";
        public const string ExportEformPDF = "api/dpp/question/eforms/ExportEFormPdf";
        public const string ExportEFormCascadingPdf = "api/dpp/question/eforms/ExportEFormCascadingPdf";
        public const string CheckEformAutomation = "api/dpp/question/eforms/checkeformautomation";

        // review Result
        public const string PermitEformReviewResult = "/api/dpp/permiteformreview/results";
        public const string PermitAttachmentResult = "/api/dpp/permiteformreview/getreviewfilecommentpermit";
        public const string EFormAttachmentResult = "/api/dpp/permiteformreview/getreviewfilecommenteform";
        public const string DownloadAttachmentReview = "/api/dpp/permiteformreview/downloadreviewfile";

        // Field App Eform Setting
        public const string FieldAppEformList = "api/Dpp/MobileSetting/questionfieldappsetting/search";

        // Notification setting
        public const string GetAllNotificationEndpoint = "/api/Dpp/UserNotification/usernotificationpaged";
        public const string GetCountNotificationEndpoint = "/api/Dpp/UserNotification/numbernotification";
        public const string ReadNotification = "/api/Dpp/UserNotification/readnotification";

        // Token Firebase
        public const string PostUserDevice = "/api/Dpp/userdevice/posttoken";
        //JobActivityTypes
        public const string JobActivityTypes = "/api/Jobs/jobActivityTypes?jobK=";
        public const string SaveJobActivityTypes = "/api/Jobs/saveJobActivityTypes";

        //SnJSiteTag
        public const string SiteTags = "/api/SiteTags";
        // JobSor
        public const string JobSors = "/api/Sites/JobSors/view";
        public const string GetJobSors = "/api/Sites/JobSors/getjobsorwithquantity";
        public const string JobSorsQuantities = "/api/Sites/JobSors/createorupdatedpquantity";
        public const string SaveJobSorsQuantities = "/api/Sites/JobSors/createorupdatejobsorsdpquantity";
        public const string ReviewArtefactAttachmentDelete = "/api/Sites/JobSors/deletearterfact";
        //
        public const string InsertOrUpdateMobileSubmitRegister = "/api/mobilesubmitjob/createorupdate";
        public const string GetJobSubmitStatus = "/api/mobilesubmitjob/getjobsubmitstatus";
        public const string InsertOrUpdateMobileTempFixRegister = "/api/mobiletempfixsubmitjob/createorupdate";
        public const string GetJobTempFixStatus = "/api/mobiletempfixsubmitjob/getjobtempfixstatus";
        public const string CheckJobSubmitAutomation = "/api/mobilesubmitjob/checkjobsubmitautomation";

        // ucg
        public const string GetAllJobPrestartByJob = "api/dpp/hseqjspversion/GetPageJpsByJob";
        public const string ExportJobPreStartPDF = "api/dpp/hseqjspversion/exportJobPreStartPDF";
        public const string GetAllUserOnSiteHistory = "api/Dpp/UserOnSiteHistories/all";
        public const string DownloadJPSAttachmentFile = "api/dpp/hseqjspversion/downloadfilezip";

        // Job Site Attachment
        public const string GetAttachments = "api/site/attachments/findbysitepaged";
        public const string DownLoadAttachments = "api/site/attachments/downloadfile";

        public const string GetSiteTags = "/api/dpp/sitetags/all";
        public const string GetAllSiteTags = "/api/dpp/sitetags/allSiteTags";
        public const string GetSiteJobTags = "/api/dpp/sitetags/sJTags";
        public const string UpdateSnJSTags = "/api/dpp/sitetags/updatesnjsitetags";
        public const string UpdateSnJJTags = "/api/dpp/sitetags/updatesnjjobtags";

        // Api Version
        public const string GetApiVersion = "api/dpp/apiversion/values";

        // WRM
        public const string GetActivityDetailById = "api/external/wrm/get-activity-details-by-id";

        // ICMS
        public const string GetJobMemo = "api/dpp/icms/get-job-memo";
        public const string GetJobInstruction = "api/dpp/icms/get-job-instruction";
        public const string GetJobLineCard = "api/external/copper/get-line-card";

        //
        public const string GetJobPreLoad = "api/dpp/preloaddata/all";

        // File extension
        public const string FileExtension = "api/dpp/filestorage/extention";

        // Features config
        public const string FeaturesConfig = "api/pmp/featuremanagement/loadfeatures";

        //Materials Config
        public const string GetLatestMaterialsConfig = "api/dpp/materialsconfig/latest";

        //Unleashed Api
        public const string GetUnleashedConfig = "api/dpp/materialsconfig/unleashedapi";

        //Unleashed
        public const string UnleashedBaseUrl = "https://api.unleashedsoftware.com/";
        public const string GetProductsEndpoint = "Products";
        public const string GetWarehousesEndpoint = "Warehouses";
        public const string GetStockOnHandEndpoint = "StockOnHand";
        public const string GetWarehouseTransfersEndpoint = "WarehouseStockTransfers";
        public const string PostWarehouseTransfersEndpoint = "WarehouseStockTransfers";
        public const string GetTaxesEndpoint = "Taxes";
        public const string GetCustomersEndpoint = "Customers";
        public const string GetSalesOrdersEndpoint = "SalesOrders";
        public const string GetSerialNumbersEndpoint = "SerialNumbers";
        public const string StockAdjustmentEndpoint = "StockAdjustments";
        public const string SendMailOrderEndpoint = "api/dpp/material/sendmail";
        public const string SendMailReceiveEndpoint = "api/dpp/material/sendmailreceive";
        public const string UpdateReceiveStatusEndpoint = "api/dpp/material/updatereceivestatus";
        public const string UnleashMaterial = "api/dpp/material/saveunleashed";
        public const string GetUnleashMaterial = "api/dpp/material/unleashmaterialtransfer/all";
        public const string GetUnleashedStockTakenSchedule = "api/dpp/unleashedstocktakenschedule/getustschedule";
        public const string GetGetPermissionByName = "api/dpp/unleashedstocktakenschedule/getpermissionbyname";
        public const string CreateOrUpdateStockTakeProductSerial = "api/dpp/unleashedstocktakeproductserial/createorupdate";
        public const string CreateOrUpdateStockTakeProductSerialBatch = "api/dpp/unleashedstocktakeproductserial/createorupdatebatch";
        public const string GetWarehouseWithUser = "api/dpp/unleashedmaterialwarehousewithuser/getmaterialwarehousewithuser";
        public const string CreateOrUpdateUnleashedStockAdjustment = "api/dpp/unleashedstockadjustment/createorupdate";

        public const string GetProductGroup = "api/dpp/material/getproductgroup";
        public const string GetAttributeSet = "api/dpp/material/getattributeset";
        public const string GetUnleashProduct = "api/dpp/material/getunleashproduct";
        public const string FindListCustomersByJobtype = "api/dpp/material/findlistcustomersbyjobtype";
        public const string FindListCustomersByUserId = "api/dpp/material/findlistcustomersbyuserid";
        public const string GetUnleashConsumptionProductsTemplate = "api/dpp/material/getunleashconsumptionproducttemplate";
        public const string GetUnleashProductByCode = "api/dpp/material/getunleashproductbycode";
        public const string GetMatrixConfigByJobK = "api/incidenteform/getmatrixconfiguration";
        public const string GetIncidentEform = "api/incidenteform/getincidenteform";
        public const string GetDpCodesConfig = "api/incidenteform/getdpcodebybill";
        public const string GetEformDpCode = "api/incidenteform/geteformdpcode";
        public const string CreateOrUpdateDpEform = "api/incidenteform/createorupdatedpeform";
        public const string DeleteDpEformByIncidentEform = "api/incidenteform/deletedpeform";
        public const string IncidentEFormUpdateForm = "api/incidenteform/createorupdate";
        public const string GetUnleashProductSparePart = "api/dpp/material/getunleashproductsparepart";
        public const string GetServiceAreaByCompanyFK = "api/dpp/material/getprogramserviceareabyDP";
        public const string GetJobTypeByCrew = "api/dpp/material/getjobtypebycrew";
        public const string GetJobTypeByName = "api/dpp/material/getjobtypebyname";
        public const string GetUCGCode = "api/incidenteform/getucgcodeconfig";
        public const string GetListCodeForNDR = "api/incidenteform/getcodesforndrconfig";
        public const string GetCustomerName = "api/incidenteform/GetCustomerName";

        // MobileSettings
        public const string MobileSettingsEndpoint = "api/dpp/mobileappsettings/all";
        public const string GoogleAPIEndpoint = "https://maps.googleapis.com/maps/";
        public const string LocationByAddressEndpoint = "api/geocode/json";
        public const string CreateOrUpdateSaleOrderEndpoint = "api/dpp/unleashedsaleorder/savesaleorder";
        public const string GetSaleOrderST3Endpoint = "api/dpp/unleashedsaleorder/getsaleorder";

        // MobileLineTest
        public const string InsertOrUpdateMobileLineTest = "api/dpp/mobilelinetest/insertorupdate";
        public const string GetLineProductIdByJob = "api/dpp/mobilelinetest/getproductid";
        public const string GetLineTestsByProductId = "api/external/linetest/tests";
        public const string GetMobileLineTestByJob = "api/dpp/mobilelinetest/getmobilelinetestbyjob";

        //ICMS Copper
        public const string GetMemoCopper = "api/external/copper/get-memo";
    }

    public class EndPointSetting
    {
        public string Name { get; set; }
        public string EndpointDPPBaseURL { get; set; }
        public string EndpointBMPSignalRURL { get; set; }
        public string EndpointUnleashedUrl { get; set; }
        public string EndpointGoogleAPI { get; set; }
        public bool IsDefault { get; set; }
        public EndPointSetting() { }
        public EndPointSetting(string name, string endpointDPPBaseURL, string endpointBMPSignalRURL, bool isDefault = false)
        {
            Name = name;
            EndpointDPPBaseURL = endpointDPPBaseURL;
            EndpointBMPSignalRURL = endpointBMPSignalRURL;
            EndpointUnleashedUrl = Endpoints.UnleashedBaseUrl;
            EndpointGoogleAPI = Endpoints.GoogleAPIEndpoint;
            IsDefault = isDefault;
        }
    }

    public class CountryEndPointSetting
    {
        public string CountryCode { get; set; }
        private Dictionary<string, EndPointSetting> EndpointSettingURL { get; set; }

        public CountryEndPointSetting(string countryCode)
        {
            CountryCode = countryCode;
            EndpointSettingURL = new Dictionary<string, EndPointSetting>();
        }

        public void Add(string endpointName, EndPointSetting endPointSetting)
        {
            if (EndpointSettingURL.ContainsKey(endpointName))
            {
                EndpointSettingURL[endpointName] = endPointSetting;
            }
            else
            {
                EndpointSettingURL.Add(endpointName, endPointSetting);
            }
        }

        public EndPointSetting GetEnpoint(string endpointName)
        {
            if (EndpointSettingURL.ContainsKey(endpointName))
            {
                return EndpointSettingURL[endpointName];
            }
            return EndpointSettingURL.Values.Where(s => s.IsDefault).FirstOrDefault();
        }

        public List<EndPointSetting> GetAllEnpoints()
        {
            return EndpointSettingURL.Values.ToList();
        }
    }
}
